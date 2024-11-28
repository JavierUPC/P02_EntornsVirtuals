using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialation : MonoBehaviour
{
    [SerializeField] private GameObject objectToMove;
    [SerializeField] private Vector3 velocity;
    [SerializeField] private float delay = 0.5f; //Temps en segons (li he posat delay pq és el q tarda en inicialitzar)

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        if (objectToMove != null)
        {
            rb = objectToMove.GetComponent<Rigidbody>();

            //per afegir rigid body si no en té
            if (rb == null)
            {
                rb = objectToMove.AddComponent<Rigidbody>();
            }

        }

        Invoke(nameof(InitializeObject), delay);

    }

    // Update is called once per frame
    void Update()
    {

        
    }

    private void InitializeObject()
    {
        if (rb != null)
        {
            rb.velocity = velocity; //per donar-li velocitat
        }
    }
}
