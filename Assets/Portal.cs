using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Color portalA;
    [SerializeField] private Color portalB;

    [SerializeField] private SpriteRenderer portalSprite;

    public void Init() {
        portalSprite = this.GetComponentInChildren<SpriteRenderer>();
    }

    public bool isPrimaryPortal;

    public void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            Debug.Log("PORATDSFLAJSDF;L");
        }
    }

    public void SetColor() {
        if (isPrimaryPortal) portalSprite.color = portalA;
        else portalSprite.color = portalB;
    }
}
