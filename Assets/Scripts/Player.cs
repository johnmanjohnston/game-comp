using UnityEngine;
using GameComp.Mechanics;

namespace GameComp.PlayerConfigs {
public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask portalable;
    [SerializeField] private GameObject portalPrefab;
    [SerializeField] private Transform firepoint;

    [SerializeField] private Portal primaryPortal;
    [SerializeField] private Portal secondaryPortal;

    private PlayerMovement movement;

    private void Start() {
        movement = this.GetComponent<PlayerMovement>();
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
        RaycastHit2D hit = Physics2D.Raycast(firepoint.position, firepoint.right, 100f, portalable);

        if (hit.collider) {
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

            if (isPrimary) {
                primaryPortal = portal;

                GameObject existingPortalGameObject = GameObject.FindGameObjectWithTag("PortalPrimary");
                if (existingPortalGameObject != null)
                    existingPortalGameObject.SetActive(false);

                portal.gameObject.tag = "PortalPrimary";
            } 
            
            else {
                secondaryPortal = portal;

                GameObject existingPortalGameObject = GameObject.FindGameObjectWithTag("PortalSecondary");
                
                if (existingPortalGameObject != null)
                    existingPortalGameObject.SetActive(false);

                portal.gameObject.tag = "PortalSecondary";
            }
        }
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
}
}