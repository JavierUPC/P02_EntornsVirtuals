using UnityEngine;

public class DimensionLightingChanger : MonoBehaviour
{
    // Referencia al script DimensionChange
    public DimensionChange dimensionChange;

    // Parámetros para el estado "No Dystopian"
    public float normalIntensityMultiplier = 1f;
    public Color normalFogColor = new Color(0.5f, 0.5f, 0.5f); // Color hexadecimal 808080
    public float normalFogDensity = 0.003f;
    public Material normalSkybox; // Skybox para el estado normal
    public float normalSunIntensity = 1f; // Intensidad de la luz del sol para el estado normal

    // Parámetros para el estado "Dystopian"
    public float dystopianIntensityMultiplier = 0.5f; // Ejemplo de valor, puedes cambiarlo
    public Color dystopianFogColor = new Color(0.2f, 0.2f, 0.2f); // Color más oscuro, ajustable
    public float dystopianFogDensity = 0.01f; // Mayor densidad, ajustable
    public Material dystopianSkybox; // Skybox para el estado Dystopian
    public float dystopianSunIntensity = 0.3f; // Intensidad de la luz del sol para el estado Dystopian

    // Referencia a la luz del sol
    public Light sunLight;

    private void Update()
    {
        if (dimensionChange != null)
        {
            if (dimensionChange.Dystopian())
            {
                SetDystopianLighting();
            }
            else
            {
                SetNormalLighting();
            }
        }
        else
        {
            Debug.LogWarning("DimensionChange script is not assigned.");
        }
    }

    private void SetNormalLighting()
    {
        RenderSettings.ambientIntensity = normalIntensityMultiplier;
        RenderSettings.fogColor = normalFogColor;
        RenderSettings.fogDensity = normalFogDensity;
        RenderSettings.skybox = normalSkybox;
        if (sunLight != null)
        {
            sunLight.intensity = normalSunIntensity;
        }
        DynamicGI.UpdateEnvironment(); // Actualiza la iluminación global
    }

    private void SetDystopianLighting()
    {
        RenderSettings.ambientIntensity = dystopianIntensityMultiplier;
        RenderSettings.fogColor = dystopianFogColor;
        RenderSettings.fogDensity = dystopianFogDensity;
        RenderSettings.skybox = dystopianSkybox;
        if (sunLight != null)
        {
            sunLight.intensity = dystopianSunIntensity;
        }
        DynamicGI.UpdateEnvironment(); // Actualiza la iluminación global
    }
}
