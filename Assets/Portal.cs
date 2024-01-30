using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Color portalA;
    [SerializeField] private Color portalB;

    [SerializeField] private SpriteRenderer portalSprite;

    public bool isA;

    public void Init() {
        portalSprite = GetComponentInChildren<SpriteRenderer>();
    }

    public void RefreshColor() {
        if (isA) {
            portalSprite.color = portalA;
        } else {
            portalSprite.color = portalB;
        }
    }
}
