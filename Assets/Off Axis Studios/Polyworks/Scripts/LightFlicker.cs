using UnityEngine;
using System.Collections.Generic;

public class LightFlicker : MonoBehaviour
{
    private float minIntensity;
    private float maxIntensity;
    private new Light light;

    [Tooltip("Multiplier to set the minimum light intensity based on the maximum.")]
    public float intensityFactor = 0.5f;

    [Tooltip("Amount to smooth out the flicker.")]
    [Range(1, 50)]
    public int smoothing = 20;

    Queue<float> smoothQueue;
    float lastSum = 0;

    public void Reset()
    {
        smoothQueue.Clear();
        lastSum = 0;
    }

    private void Awake()
    {
        light = transform.GetComponent<Light>();

        maxIntensity = light.intensity;
        minIntensity = maxIntensity * intensityFactor;
    }

    void Start()
    {
        smoothQueue = new Queue<float>(smoothing);

        if (light == null)
        {
            return;
        }
    }

    void Update()
    {
        if (light == null)
            return;

        while (smoothQueue.Count >= smoothing)
        {
            lastSum -= smoothQueue.Dequeue();
        }

        float newVal = Random.Range(minIntensity, maxIntensity);
        smoothQueue.Enqueue(newVal);
        lastSum += newVal;

        light.intensity = lastSum / (float)smoothQueue.Count;
    }

}