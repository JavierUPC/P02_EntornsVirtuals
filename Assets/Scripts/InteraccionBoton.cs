using UnityEngine;

public class InteraccionBoton : MonoBehaviour
{
    public AbrirPuerta abrirPuertaScript;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            abrirPuertaScript.BotonTocado(this.gameObject);
            Debug.Log("Contacto");
        }
    }
}
