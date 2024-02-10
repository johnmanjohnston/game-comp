using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    private Image img;
    private float alpha; // ranges from 0-1
    [SerializeField] private float killAmount; // at what alpha should we destroy this gameObject?
    [SerializeField] private float multiplier;

    private void Awake() {
        img = this.GetComponent<Image>();
        alpha = 1f;

        img.color = new Color(0f, 0f, 0f, alpha);
    }

    private void Update() {
        alpha -= Time.deltaTime * multiplier;
        img.color = new Color(0f, 0f, 0f, alpha);

        if (alpha <= killAmount) {
            Destroy(this.gameObject);
        }
    }
}