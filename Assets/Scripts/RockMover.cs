using UnityEngine;

public class RockMover : MonoBehaviour
{
    public GameObject objectToMove1, objectToMove2, objectToMove3;
    public Vector3 direction1, direction2, direction3;
    [SerializeField] private float speed = 5f;

    public void MoverRoca()
    {
        Debug.Log("Entra");
        objectToMove1.GetComponent<Rigidbody>().velocity = direction1 * speed;
        objectToMove2.GetComponent<Rigidbody>().velocity = direction2 * speed;
        objectToMove3.GetComponent<Rigidbody>().velocity = direction3 * speed;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            MoverRoca();
        }
    }
}
