using System.Collections.Generic;
using UnityEngine;

// smooths a controller's rotation using a moving average, to soften tremor on the ray
public class RaySmoother : MonoBehaviour
{
    public Transform source;
    public int sampleCount = 5;

    Queue<Quaternion> recentRotations = new Queue<Quaternion>();

    void Update()
    {
        transform.position = source.position;

        recentRotations.Enqueue(source.rotation);
        if (recentRotations.Count > sampleCount)
            recentRotations.Dequeue();

        transform.rotation = AverageRotation();
    }

    Quaternion AverageRotation()
    {
        Vector3 forwardSum = Vector3.zero;
        Vector3 upSum = Vector3.zero;

        foreach (Quaternion rot in recentRotations)
        {
            forwardSum += rot * Vector3.forward;
            upSum += rot * Vector3.up;
        }

        return Quaternion.LookRotation(forwardSum / recentRotations.Count, upSum / recentRotations.Count);
    }
}
