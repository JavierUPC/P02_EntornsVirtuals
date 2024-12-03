using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogIntensityController : MonoBehaviour
{
    public int intensityLevel = 1; // 1: Max, 2: Medium, 3: Min

    private ParticleSystem particleSystem;
    private ParticleSystem.ColorOverLifetimeModule colorOverLifetime;

    public Gradient maxFog; // Preset 1: Max fog
    public Gradient medFog; // Preset 2: Medium fog
    public Gradient minFog; // Preset 3: Min fog

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        if (particleSystem == null)
        {
            Debug.LogError("No ParticleSystem attached to the GameObject!");
            return;
        }

        colorOverLifetime = particleSystem.colorOverLifetime;

        // Apply the initial intensity level
        UpdateFogIntensity();
    }

    void Update()
    {
        // Check for input or condition to dynamically change intensity level
        // Example: intensityLevel = someValue;
        UpdateFogIntensity();
    }

    private void UpdateFogIntensity()
    {
        if (colorOverLifetime.enabled)
        {
            switch (intensityLevel)
            {
                case 1:
                    colorOverLifetime.color = new ParticleSystem.MinMaxGradient(maxFog);
                    break;
                case 2:
                    colorOverLifetime.color = new ParticleSystem.MinMaxGradient(medFog);
                    break;
                case 3:
                    colorOverLifetime.color = new ParticleSystem.MinMaxGradient(minFog);
                    break;
                default:
                    Debug.LogWarning("Invalid intensity level! Use values 1, 2, or 3.");
                    break;
            }
        }
        else
        {
            Debug.LogWarning("Color Over Lifetime is disabled in the ParticleSystem.");
        }
    }
}

