using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeDuration;
    public float shakeIntensity;
    public float decreaseFactor;

    private void Update()
    {
        if (shakeDuration > 0f)
        {
            this.transform.localPosition = Random.insideUnitSphere * shakeIntensity;
            shakeDuration -= Time.deltaTime * decreaseFactor;
        } else
        {
            shakeDuration = 0f;
            this.transform.localPosition = Vector3.zero;
        }

        // if (Input.GetKeyDown(KeyCode.M)) { shakeDuration = .2f; };
    }

    public void Shake(float duration)
    {
        shakeDuration = duration;
        print($"Shake() called with duration of {duration}");
    }
}
