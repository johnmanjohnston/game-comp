using UnityEngine;
using TMPro;
using GameComp.PlayerConfigs;

public class DebugMenu : MonoBehaviour
{
    private TextMeshProUGUI text;
    private PlayerMovement mv;

    private void Start() {
        text = this.GetComponent<TextMeshProUGUI>();
        mv = FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>();
    }

    private void Update() {
        text.text = $"Velocity: {mv.rb.velocity}\n Horizontal: {mv.horizontal}\n Previous Direction: {mv.previousDirection}";
    }
}
