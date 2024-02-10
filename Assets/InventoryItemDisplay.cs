using UnityEngine;
using UnityEngine.UI;

public class InventoryItemDisplay : MonoBehaviour {

    private Image img;
    private Color activeCol;
    private Color deactivatedCol;

    private void Start() {
        img = this.GetComponent<Image>();

        activeCol = new Color(1f, 1f, 1f, 1f);
        deactivatedCol = new Color(1f, 1f, 1f, 0f);
    }

    public void SetGFX(Sprite gfx) {
        this.img.sprite = gfx;
        this.img.color = activeCol;
    }

    public void RemoveGFX() {
        this.img.sprite = null;
        this.img.color = deactivatedCol;
    }
}