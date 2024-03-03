using System.Collections.Generic;
using UnityEngine;
using GameComp.Mechanics;
using GameComp.Core;
using GameComp.Utilities;
using System.Collections;

namespace GameComp.PlayerConfigs {
public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask portalable;
    [SerializeField] private GameObject portalPrefab;
    [SerializeField] private Transform firepoint;

    [SerializeField] private Portal primaryPortal;
    [SerializeField] private Portal secondaryPortal;

    private PlayerMovement movement;

    [SerializeField] private GameObject playerSpriteObject;
    private SpriteRenderer gfx;

    [SerializeField] private bool IGNORE_PLAYER_GFX_SCALE_FACTOR;

    [SerializeField] private List<InventoryItem> inventoryItems;

    public List<StringIntPair> PLAYER_GFX_SCALE_FACTOR_LOOKUP_TABLE;

        private CameraShake cameraShake;

    private void Start() {
        movement = this.GetComponent<PlayerMovement>();
        gfx = playerSpriteObject.GetComponent<SpriteRenderer>();

        cameraShake = FindObjectOfType<CameraShake>();

        if (!IGNORE_PLAYER_GFX_SCALE_FACTOR) {
            /*
            int targetPlayerSpriteScaleFactor = Convert.ToInt16(gfx.sprite.ToString()[5].ToString());
            Vector3 targetPlayerSpriteScaleVector = new(targetPlayerSpriteScaleFactor, targetPlayerSpriteScaleFactor, 1f); 
            playerSpriteObject.transform.localScale = targetPlayerSpriteScaleVector;
            print($"Player GFX name is {gfx.sprite.ToString()}, the scale factor is {targetPlayerSpriteScaleFactor}");
            */

                                        // 21 is the length of" (UnityEngine.Sprite)", including the space
            string spriteName = gfx.sprite.ToString().Substring(0, gfx.sprite.ToString().Length - 21);
            int? scaleFactor = StaticUtilities.GetValOfStringIntPair(PLAYER_GFX_SCALE_FACTOR_LOOKUP_TABLE, spriteName);
            if (scaleFactor == null) { Debug.LogError($"SCALE FACTOR IS NULL. SPRITE NAME: {spriteName}"); }
            else {
                Vector3 scaleVector = new((int)scaleFactor, (int)scaleFactor, 1f);
                playerSpriteObject.transform.localScale = scaleVector;

                print($"Configured scale vector. Scale factor is {scaleFactor}, and the sprite name is {spriteName}");
            }
        } else {
            Debug.LogWarning("IGNORING PLAYER GFX SCALING");
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ShootPortal(true);
        }
        
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            ShootPortal(false);
        }
    }

    private void ShootPortal(bool isPrimary) {
        // send raycast to shoot portal
        movement.animator.SetTrigger("portalShootAnim");
        StartCoroutine(ShootPortalWithDelay(isPrimary));
        StartCoroutine(ResetPortalAnimationTrigger());
    }

    public void TeleportPlayer(bool enteredPrimaryPortal) {
        if (enteredPrimaryPortal) {
            if (secondaryPortal != null) {
                transform.position = secondaryPortal.transform.position;
                secondaryPortal.canTeleport = false;
            }
        }

        else {
            if (primaryPortal != null) {
                transform.position = primaryPortal.transform.position;
                primaryPortal.canTeleport = false;
            }
        }
    }

    public void TryAddItemToInventory(InventoryItem item) {
        if (inventoryItems.Count < GameManager.inventorySizeLimit) {
            Sprite gfx = item.gameObject.GetComponentInChildren<SpriteRenderer>().sprite;
            GameManager.DisplayItemInInventorySlot(gfx, this.inventoryItems.Count);

            this.inventoryItems.Add(item);
        }
    }

        [SerializeField] private float portalShootDelay;
    private IEnumerator ResetPortalAnimationTrigger()
    {
        yield return new WaitForSeconds(portalShootDelay);
        movement.animator.ResetTrigger("portalShootAnim");
    }

    private IEnumerator ShootPortalWithDelay(bool isPrimary)
    {
            yield return new WaitForSeconds(portalShootDelay);


            RaycastHit2D hit = Physics2D.Raycast(firepoint.position, firepoint.right, 100f, portalable);

            cameraShake.Shake(.2f);

            if (hit.collider)
            {
                // assign hit point, create portal, configure portal position
                Vector3 hitPoint = hit.point;
                GameObject portalInstance = Instantiate(portalPrefab);
                portalInstance.transform.position = hitPoint;

                // configure portal gameobject's Portal class variables
                Portal portal = portalInstance.GetComponent<Portal>();
                portal.isPrimaryPortal = isPrimary;
                portal.Init();
                portal.SetColor();
                portal.transform.localScale = new Vector3(-movement.previousDirection, 1, 1); // make child objects of the portal (could be lights/particles, etc.) face the player

                if (isPrimary)
                {
                    primaryPortal = portal;

                    GameObject existingPortalGameObject = GameObject.FindGameObjectWithTag("PortalPrimary");
                    if (existingPortalGameObject != null)
                        existingPortalGameObject.SetActive(false);

                    portal.gameObject.tag = "PortalPrimary";
                }

                else
                {
                    secondaryPortal = portal;

                    GameObject existingPortalGameObject = GameObject.FindGameObjectWithTag("PortalSecondary");

                    if (existingPortalGameObject != null)
                        existingPortalGameObject.SetActive(false);

                    portal.gameObject.tag = "PortalSecondary";
                }
            }
        }
}
}