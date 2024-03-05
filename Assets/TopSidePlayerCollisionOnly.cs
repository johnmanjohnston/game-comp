using UnityEngine;

public class TopSidePlayerCollisionOnly : MonoBehaviour
{
    private GameObject player;
    private BoxCollider2D col;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        col = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        col.enabled = player.transform.position.y >= this.transform.position.y;
    }
}
