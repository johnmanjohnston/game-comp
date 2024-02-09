using UnityEngine;
using TMPro;
using GameComp.PlayerConfigs;
using GameComp.Core;

public class DebugMenu : MonoBehaviour
{
    private TextMeshProUGUI text;
    private PlayerMovement mv;

    private void Start() {
        text = this.GetComponent<TextMeshProUGUI>();
        mv = FindObjectOfType<PlayerMovement>().GetComponent<PlayerMovement>();
    }

    private void Update() {
        text.text = $"Velocity: {mv.rb.velocity}\nHorizontal: {mv.horizontal}\nPrevious Direction: {mv.previousDirection}\nPlayer in range of interactable (static): {GameManager.PlayerInRangeOfInteractable}";
    }
}
