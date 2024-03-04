using UnityEngine;

public class ParalaxObject : MonoBehaviour
{
    public float paralaxFactor;
    private Vector3 startPos;

    private Camera cam;

    public Vector3 offset;

    private void Start()
    {
        startPos = transform.position;
        cam = Camera.main;
    }

    private void Update()
    {
        transform.position = (startPos + (cam.transform.position * paralaxFactor)) + offset;
    }
}