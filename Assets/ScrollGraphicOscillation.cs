using UnityEngine;

public class ScrollGraphicOscillation : MonoBehaviour
{
    public float amplitude;
    public float frequency;

    public void Update()
    {
        float sinewave = Mathf.Sin(Time.time * frequency) * amplitude;
        this.transform.localPosition = new Vector3(0f, sinewave, 0f);
    }
}