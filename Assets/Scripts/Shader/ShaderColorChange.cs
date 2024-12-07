using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderColorChange : MonoBehaviour
{
    private MeshRenderer meshRenderer;
    private Material material;
    private float r, g, b;
    private Color blue, red, emissionBlue, emissionRed;
    private bool timer;
    private float timed;
    private float lastValue, lerpTime;
    public float stayDuration, transitionTime;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        material = meshRenderer.material;
        blue = new Color(0f / 255f, 221f / 255f, 255f / 255f);
        red = new Color(130f / 255f, 0f / 255f, 0f / 255f);
        emissionBlue = new Color(0f / 255f, 166f / 255f, 191f / 255f);
        emissionRed = new Color(230f / 255f, 0f / 255f, 0f / 255f);
        timer = true;
        timed = 0;
        lastValue = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log($"lastValue: {lastValue}, timer: {timer}, lerpTime: {lerpTime}");

        if (!timer && lastValue == 1)
            ColorToBlue();

        if((!timer && lastValue == 0))
            ColorToRed();

        if (timer)
        {
            timed += Time.unscaledDeltaTime;
            if (timed > stayDuration)
            {
                timer = false;
                timed = 0;
            }
        }
    }

    //CAMBIAR TAMBIÉN EMISIÓN

    private void ColorToBlue()
    {
        Debug.Log("Entered Blue");
        lerpTime += Time.unscaledDeltaTime / transitionTime;
        material.SetColor("_ColorBase", Color.Lerp(red, blue, lerpTime));
        material.SetColor("_Emission", Color.Lerp(emissionRed, emissionBlue, lerpTime));
        //material.SetFloat("_TurnSpeed", Mathf.Lerp(-0.05f, 0.05f, lerpTime));
        if (lerpTime >= 1)
        {
            timer = true;
            lastValue = 0;
            lerpTime = 0;
        }
    }

    private void ColorToRed()
    {
        Debug.Log("Entered Blue");
        lerpTime += Time.unscaledDeltaTime / transitionTime;
        material.SetColor("_ColorBase", Color.Lerp(blue, red, lerpTime));
        material.SetColor("_Emission", Color.Lerp(emissionBlue, emissionRed, lerpTime));
        //material.SetFloat("_TurnSpeed", Mathf.Lerp(0.05f, -0.05f, lerpTime));
        if (lerpTime >= 1)
        {
            timer = true;
            lastValue = 1;
            lerpTime = 0;
        }
    }
}
