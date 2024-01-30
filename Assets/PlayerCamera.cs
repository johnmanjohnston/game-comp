using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 target = new(player.position.x + offset.x, player.position.y + offset.y, offset.z);
        transform.position = Vector3.Lerp(transform.position, target, 1f/30f);
    }
}