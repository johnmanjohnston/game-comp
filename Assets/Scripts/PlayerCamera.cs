using UnityEngine;

namespace GameComp.PlayerConfigs {
public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float smoothTime = 0.3f;

    private Transform player;
    private Vector3 velocity = Vector3.zero;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        Vector3 targetPosition = player.transform.position + offset;

        targetPosition = new Vector3(
            Mathf.RoundToInt(targetPosition.x),
            Mathf.RoundToInt(targetPosition.y),
            Mathf.RoundToInt(targetPosition.z)
        );

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
}