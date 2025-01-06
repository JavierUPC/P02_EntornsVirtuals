using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchLight : MonoBehaviour
{
    public Light torchLight; // Luz que queremos simular
    public Color color1 = Color.red;
    public Color color2 = Color.yellow;
    public float minIntensity = 1.5f;
    public float maxIntensity = 2.5f;
    public float minRange = 5f;
    public float maxRange = 8f;
    public float flickerSpeed = 0.1f;

    private float flickerTimer = 0f;

    void Start()
    {
        if (torchLight == null)
        {
            torchLight = GetComponent<Light>();
        }
    }

    void Update()
    {
        flickerTimer += Time.deltaTime;

        if (flickerTimer >= flickerSpeed)
        {
            // Cambiar intensidad
            torchLight.intensity = Random.Range(minIntensity, maxIntensity);

            // Cambiar rango
            torchLight.range = Random.Range(minRange, maxRange);

            // Cambiar color entre dos colores
            torchLight.color = Color.Lerp(color1, color2, Random.value);

            flickerTimer = 0f;
        }
    }
}
