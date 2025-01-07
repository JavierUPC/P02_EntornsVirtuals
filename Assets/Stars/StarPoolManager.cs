using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarPoolManager : MonoBehaviour
{
    public GameObject starPrefab; // Prefab de la estrella
    public Material[] starMaterials; // Array con los cuatro materiales
    public Transform starCreationPoint; // Punto inicial de creación
    public int starsPerColor = 5; // Número de estrellas por color
    public float starLifeTime = 5f; // Duración de la animación (en segundos)
    public float visibleTime = 1f; // Tiempo visible por estrella
    public int maxVisibleStars = 6; // Número máximo de estrellas visibles

    private Queue<GameObject> starPool = new Queue<GameObject>();
    private List<GameObject> activeStars = new List<GameObject>();

    void Start()
    {
        // Crear las estrellas iniciales
        for (int i = 0; i < starMaterials.Length; i++)
        {
            for (int j = 0; j < starsPerColor; j++)
            {
                GameObject newStar = Instantiate(starPrefab, starCreationPoint.position, Quaternion.identity);
                newStar.GetComponent<MeshRenderer>().material = starMaterials[i];
                newStar.SetActive(false); // Hacer la estrella invisible
                starPool.Enqueue(newStar);
            }
        }
    }

    public void TriggerStars(Vector3 triggerPosition)
    {
        // Mover el GameObject a la posición del trigger
        Debug.Log("Entered triggerStars");
        transform.position = triggerPosition;
        StartCoroutine(StarAnimation());
    }

    private IEnumerator StarAnimation()
    {
        float elapsedTime = 0f;
        while (elapsedTime < starLifeTime)
        {
            if (activeStars.Count < maxVisibleStars && starPool.Count > 0)
            {
                GameObject star = starPool.Dequeue();
                activeStars.Add(star);
                star.SetActive(true);

                // Mover la estrella desde la posición actual del StarCreation
                StartCoroutine(MoveStar(star, transform.position));
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Desactivar las estrellas restantes
        foreach (GameObject star in activeStars)
        {
            star.SetActive(false);
            star.transform.position = starCreationPoint.position;
            starPool.Enqueue(star);
        }
        activeStars.Clear();
    }

    private IEnumerator MoveStar(GameObject star, Vector3 startPosition)
    {
        Vector3 endPosition = startPosition + new Vector3(Random.Range(-10f, 10f), 20f, Random.Range(-10f, 10f));

        float time = 0f;
        while (time < visibleTime)
        {
            star.transform.position = Vector3.Lerp(startPosition, endPosition, time / visibleTime);
            time += Time.deltaTime;
            yield return null;
        }

        // Volver la estrella a la posición inicial y desactivarla
        star.SetActive(false);
        star.transform.position = starCreationPoint.position;
        activeStars.Remove(star);
        starPool.Enqueue(star);
    }
}
