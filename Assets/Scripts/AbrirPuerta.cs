using UnityEngine;

public class AbrirPuerta : MonoBehaviour
{
    public GameObject objectButton1, objectButton2, objectButton3, objectButton4, objectmuro;
    public Material VerdeCubo;
    private bool button1Pressed = false, button2Pressed = false, button3Pressed = false, button4Pressed = false;

    private void Update()
    {
        if (button1Pressed && button2Pressed && button3Pressed && button4Pressed)
        {
            gameObject.SetActive(false);
        }
    }

    public void BotonTocado(GameObject button)
    {
        if (button == objectButton1)
        {
            button1Pressed = true;
            CambiarColorHijos(objectButton1, VerdeCubo);
            CambiarColorHijosMuro(objectmuro, new int[] { 2, 3, 7, 8, 9 }, VerdeCubo);
        }
        else if (button == objectButton2)
        {
            button2Pressed = true;
            CambiarColorHijos(objectButton2, VerdeCubo);
            CambiarColorHijosMuro(objectmuro, new int[] { 4, 5, 6, 10, 11 }, VerdeCubo);
        }
        else if (button == objectButton3)
        {
            button3Pressed = true;
            CambiarColorHijos(objectButton3, VerdeCubo);
            CambiarColorHijosMuro(objectmuro, new int[] { 1, 13, 15, 17, 18 }, VerdeCubo);
        }
        else if (button == objectButton4)
        {
            button4Pressed = true;
            CambiarColorHijos(objectButton4, VerdeCubo);
            CambiarColorHijosMuro(objectmuro, new int[] { 0, 12, 14, 16, 19 }, VerdeCubo);
        }
    }

    private void CambiarColorHijos(GameObject button, Material nuevoMaterial)
    {
        foreach (Transform hijo in button.transform)
        {
            Renderer renderer = hijo.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.material = nuevoMaterial;
            }
        }
    }

    private void CambiarColorHijosMuro(GameObject targetObject, int[] indices, Material nuevoMaterial)
    {
        foreach (int indice in indices)
        {
            if (indice >= 0 && indice < targetObject.transform.childCount)
            {
                Transform hijo = targetObject.transform.GetChild(indice);
                Renderer renderer = hijo.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material = nuevoMaterial;
                }
            }
        }
    }
}