using GameComp.PlayerConfigs;
using UnityEngine;

namespace GameComp.Mechanics {
public class Flier : MonoBehaviour
{
    [SerializeField] private float flyAmount;

    private void Start() {
        this.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D col) {
        // TODO: create a function in the PlayerMovement class DEDICATED
        // to forcing the player up, if the player is in a flier
        if (col.CompareTag("Player")) {
            PlayerMovement mv = col.GetComponent<PlayerMovement>();
            mv.BoostPlayer(flyAmount, transform.up);
        }
    }
}
}
