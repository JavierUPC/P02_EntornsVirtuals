using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionChange : MonoBehaviour
{
    public float zOffset;
    private bool dystopian;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("C"))
        {
            ChangeDimension();
        }
    }

    private void ChangeDimension()
    {
        //Animación personaje
        //Animación cámara
    }

    public void ChangeMap()
    {
        if (!dystopian)
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + zOffset);
        else
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - zOffset);
    }
}
