using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DimensionLightingChanger : MonoBehaviour
{
    [Header("References")]
    public DimensionChange dimensionChangeScript; // Referencia al script DimensionChange

    [Header("Lighting Settings")]
    public Material dystopianSkybox;              // Material del Skybox para la dimensi�n dist�pica
    public Material normalSkybox;                 // Material del Skybox para la dimensi�n normal
    public float dystopianIntensityMultiplier = 0.5f; // Intensidad de luz en la dimensi�n dist�pica
    public float normalIntensityMultiplier = 1.0f;    // Intensidad de luz en la dimensi�n normal
    public float dystopianFogDensity = 0.1f;      // Densidad de niebla en la dimensi�n dist�pica
    public float normalFogDensity = 0.02f;        // Densidad de niebla en la dimensi�n normal

    void Update()
    {
        // Comprueba si el script y el m�todo existen
        if (dimensionChangeScript != null)
        {
            bool isDystopian = dimensionChangeScript.distopian;

            // Cambia el Skybox
            RenderSettings.skybox = isDystopian ? dystopianSkybox : normalSkybox;

            // Cambia la intensidad de la luz
            RenderSettings.sun.intensity = isDystopian ? dystopianIntensityMultiplier : normalIntensityMultiplier;

            // Cambia la densidad de la niebla
            RenderSettings.fogDensity = isDystopian ? dystopianFogDensity : normalFogDensity;
        }
    }
}
