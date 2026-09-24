using UnityEngine;

// adds small random shake to a controller, to test how selection techniques handle tremor
public class TremorSimulator : MonoBehaviour
{
    public bool tremorEnabled = false;
    public Transform target;
    public float rotationAmount = 3f;

    void LateUpdate()
    {
        if (!tremorEnabled)
            return;

        float noiseX = (Mathf.PerlinNoise(Time.time * 10f, 0f) - 0.5f) * rotationAmount;
        float noiseY = (Mathf.PerlinNoise(0f, Time.time * 10f) - 0.5f) * rotationAmount;

        target.Rotate(noiseX, noiseY, 0f, Space.Self);
    }
}
