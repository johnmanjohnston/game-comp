using UnityEngine;

namespace GameComp.Core {
    public class GameManager : MonoBehaviour
    {
        public static bool PlayerInRangeOfInteractable;
        private static GameObject interactableIcon;

        private void Awake() {
            // configure and assign variables
            PlayerInRangeOfInteractable = false;
            interactableIcon = GameObject.Find("Interactable Icon");

            ConfigureInteractableIconVisibility(false);
        }

        public static void ConfigureInteractableIconVisibility(bool visible) {
            interactableIcon.SetActive(visible);
        }
    }
}
