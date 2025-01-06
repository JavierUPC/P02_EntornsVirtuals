using UnityEngine;

public class AbrirPuerta : MonoBehaviour
{
    public GameObject objectButton1, objectButton2, objectButton3, objectButton4;
    private bool button1Pressed = false, button2Pressed = false, button3Pressed = false, button4Pressed = false;

    private void Update()
    {
        if (button1Pressed && button2Pressed && button3Pressed && button4Pressed)
        {
            gameObject.SetActive(false);
            Debug.Log("puertafalse");
        }
    }

    public void BotonTocado(GameObject button)
    {
        if (button == objectButton1)
        {
            button1Pressed = true;
            Debug.Log("ob1");
        }
        else if (button == objectButton2)
        {
            button2Pressed = true;
            Debug.Log("ob2");
        }
        else if (button == objectButton3)
        {
            button3Pressed = true;
            Debug.Log("ob3");
        }
        else if (button == objectButton4)
        {
            button4Pressed = true;
            Debug.Log("ob4");
        }
    }
}
