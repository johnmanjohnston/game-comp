using System;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    private Image img;
    [SerializeField] private float alpha;
    [SerializeField] private float killAmount; // at what alpha should we destroy this gameObject?
    [SerializeField] private float multiplier;

    private void Awake() {
        img = this.GetComponent<Image>();
        UpdateColor();
    }

    private void Update() {
        alpha -= Time.deltaTime * multiplier;
        UpdateColor();

        if (alpha <= killAmount) {
            Destroy(this.gameObject);
        }
    }

    private void UpdateColor() {
        img.color = new Color(0f, 0f, 0f, Math.Min(alpha, 1f));
    }
}