using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunRotation : MonoBehaviour
{
    // Duración completa del día en segundos
    public float dayDuration = 120f; // Por defecto, 120 segundos (2 minutos)

    // Tiempo transcurrido dentro del día
    private float timeElapsed = 0f;

    void Update()
    {
        // Incrementar el tiempo transcurrido
        timeElapsed += Time.deltaTime;

        // Calcular el progreso del día como un porcentaje (0 a 1)
        float dayProgress = timeElapsed / dayDuration;

        // Limitar el progreso para que no supere el 100% (1)
        if (dayProgress > 1f)
        {
            dayProgress = 0f; // Reinicia el día
            timeElapsed = 0f;
        }

        // Calcular el ángulo de rotación en X según el progreso del día
        float rotationX = Mathf.Lerp(0f, 180f, dayProgress);

        // Aplicar la rotación al objeto de la luz direccional
        transform.rotation = Quaternion.Euler(rotationX, 0f, 0f);
    }
}
