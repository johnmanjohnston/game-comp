using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private LayerMask portalable;
    [SerializeField] private GameObject portalPrefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            // shoot portal right
            ShootPortal(new Vector2(1, 0));
        } 
        
        else if (Input.GetKeyDown(KeyCode.Q)) {
            // shoot portal left
            ShootPortal(new Vector2(-1, 0));
        }
    }

    private void ShootPortal(Vector2 dir) {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 100f, portalable);
        if (hit.collider) {
            Vector3 hitPoint = hit.point;
            GameObject portalInstance = GameObject.Instantiate(portalPrefab);
            portalInstance.transform.position = hitPoint;

            Portal portal = portalInstance.GetComponent<Portal>();
           // portal.Init();
           // portal.isA = true;
           // portal.RefreshColor();
        }
    }
}