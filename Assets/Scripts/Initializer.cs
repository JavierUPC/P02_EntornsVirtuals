using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [SerializeField] private GameObject objectToMove;
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float delay = 0.5f; //x si volem delay

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        if (objectToMove != null)
        {
            rb = objectToMove.GetComponent<Rigidbody>();

            //x afegir rb si no té
            if (rb == null)
            {
                rb = objectToMove.AddComponent<Rigidbody>();
            }
        }

        Invoke(nameof(InitializeObject), delay);
    }

    private void InitializeObject()
    {
        if (rb != null && target != null)
        {
            //calcul vector dir normalizado
            Vector3 direction = (target.position - objectToMove.transform.position).normalized;

            rb.velocity = direction * speed;
        }
    }
}
