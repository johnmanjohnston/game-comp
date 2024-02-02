using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask portalable;
    [SerializeField] private GameObject portalPrefab;
    [SerializeField] private Transform firepoint;

    [SerializeField] private Portal primaryPortal;
    [SerializeField] private Portal secondaryPortal;

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
        RaycastHit2D hit = Physics2D.Raycast(firepoint.position, firepoint.right, 100f, portalable);
        if (hit.collider) {
            Vector3 hitPoint = hit.point;
            GameObject portalInstance = Instantiate(portalPrefab);
            portalInstance.transform.position = hitPoint;

            Portal portal = portalInstance.GetComponent<Portal>();
            portal.isPrimaryPortal = isPrimary;
            portal.Init();
            portal.SetColor();


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
}