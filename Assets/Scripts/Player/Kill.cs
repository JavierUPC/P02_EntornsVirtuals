using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Kill : MonoBehaviour
{
    public GameObject Canvas;
    private Animator canvasAnimator;
    private string currentSceneName;


    private void Start()
    {
        canvasAnimator = GetComponent<Animator>();
        currentSceneName = SceneManager.GetActiveScene().name;
    }
    public void Respawn()
    {
        SceneManager.LoadScene(currentSceneName);
        //canvasAnimator.SetTrigger("GameOver");
        //StartCoroutine(WaitSeconds());
    }

    //IEnumerator WaitSeconds()
    //{
    //    yield return new WaitForSeconds(2f);
    //    SceneManager.LoadScene(currentSceneName);
    //}
}
