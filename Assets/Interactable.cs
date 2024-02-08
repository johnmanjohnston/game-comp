using GameComp.Core;
using UnityEngine;

namespace GameComp.Interactables {
    [RequireComponent(typeof(Collider2D))]
    public class Interactable : MonoBehaviour
    {

        private void Start() {
            GetComponent<Collider2D>().isTrigger = true;
        }

        // allow the function to be overriden to provide custom functionality
        protected virtual void OnInteract() {
            print("OnInteract() called");
        }

        private bool playerInRange;
        private void Update() {
            // if player is in range, and clicks the F key, perform the interactable function
            if (playerInRange) {
                if (Input.GetKeyDown(KeyCode.F)) {
                    this.OnInteract();
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D col) {
            if (col.gameObject.CompareTag("Player")) {
                // player is within range
                GameManager.ConfigureInteractableIconVisibility(true);
                playerInRange = true;
            }
        }

        private void OnTriggerExit2D(Collider2D col) {
            if (col.gameObject.CompareTag("Player")) {
                // player is out of range
                GameManager.ConfigureInteractableIconVisibility(false);
                playerInRange = false;
            }
        }
    }
}
