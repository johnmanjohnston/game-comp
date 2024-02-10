using UnityEngine;
using UnityEngine.UI;

namespace GameComp.Core {
    public class GameManager : MonoBehaviour
    {
        public static bool PlayerInRangeOfInteractable;
        private static GameObject interactableIcon;

        public static int inventorySizeLimit = 4;
        private static GameObject[] inventorySlotDisplays = new GameObject[inventorySizeLimit];

        private void Awake() {
            // configure and assign variables
            PlayerInRangeOfInteractable = false;
            interactableIcon = GameObject.Find("Interactable Icon");

            ConfigureInteractableIconVisibility(false);

           for (int i = 0; i < inventorySizeLimit; i++) {
                print($"Assigning InventorySlot{i}");
                inventorySlotDisplays[i] = GameObject.Find($"InventorySlot{i}");
            }
        }

        public static void ConfigureInteractableIconVisibility(bool visible) {
            interactableIcon.SetActive(visible);
        }

        // indexes start from 0
        public static void DisplayItemInInventorySlot(Sprite gfx, int index) {
            inventorySlotDisplays[index].GetComponentInChildren<InventoryItemDisplay>().SetGFX(gfx);
        }
    }
}
