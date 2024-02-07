using UnityEngine;
using TMPro;

public class DebugMenu : MonoBehaviour
{
    private TextMeshProUGUI text;
    private PlayerMovement mv;

    private void Start() {
        text = this.GetComponent<TextMeshProUGUI>();
        mv = FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>();
    }

    private void Update() {
        text.text = $"Velocity: {mv.rb.velocity}\n Horizontal: {mv.horizontal}";
    }
}
