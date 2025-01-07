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
    public float flickerSpeed = 0.1f; // Velocidad de los cambios
    public float smoothSpeed = 5f; // Velocidad de suavizado

    private float targetIntensity;
    private float targetRange;
    private Color targetColor;

    void Start()
    {
        if (torchLight == null)
        {
            torchLight = GetComponent<Light>();
        }

        // Inicializar valores objetivos
        targetIntensity = Random.Range(minIntensity, maxIntensity);
        targetRange = Random.Range(minRange, maxRange);
        targetColor = Color.Lerp(color1, color2, Random.value);
    }

    void Update()
    {
        // Actualizar suavemente los valores actuales hacia los objetivos
        torchLight.intensity = Mathf.Lerp(torchLight.intensity, targetIntensity, smoothSpeed * Time.deltaTime);
        torchLight.range = Mathf.Lerp(torchLight.range, targetRange, smoothSpeed * Time.deltaTime);
        torchLight.color = Color.Lerp(torchLight.color, targetColor, smoothSpeed * Time.deltaTime);

        // Cambiar objetivos a intervalos de tiempo
        if (Time.time % flickerSpeed < Time.deltaTime)
        {
            targetIntensity = Random.Range(minIntensity, maxIntensity);
            targetRange = Random.Range(minRange, maxRange);
            targetColor = Color.Lerp(color1, color2, Random.value);
        }
    }
}
