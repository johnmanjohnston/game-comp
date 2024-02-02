using Unity.VisualScripting;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Color portalA;
    [SerializeField] private Color portalB;

    [SerializeField] private SpriteRenderer portalSprite;
    public bool canTeleport = true;

    public void Init() {
        portalSprite = this.GetComponentInChildren<SpriteRenderer>();
    }

    public bool isPrimaryPortal;

    public void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player") && canTeleport) {
            Debug.Log("PORATDSFLAJSDF;L");

            col.GetComponent<Player>().TeleportPlayer(isPrimaryPortal);
            col.GetComponent<PlayerMovement>().AddForceOnEnteringPortal();
        }
    }

    public void OnTriggerExit2D(Collider2D col) {
        if (col.GetComponent<Player>()) {
            canTeleport = true;
        }
    }

    public void SetColor() {
        if (isPrimaryPortal) portalSprite.color = portalA;
        else portalSprite.color = portalB;
    }
}
