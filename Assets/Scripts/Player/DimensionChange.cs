using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DimensionChange : MonoBehaviour
{
    public float xOffset, camAnimSpeed;
    private bool dystopian;
    public Animator playerAnimator, canvasAnimator;
    public Transform camera;
    private bool IsChangingWorld;
    // Start is called before the first frame update
    void Start()
    {
        dystopian = false;   
    }

    // Update is called once per frame
    void Update()
    {
        IsChangingWorld = (Input.GetKey(KeyCode.E));
        if (IsChangingWorld)
            ChangeDimension();
    }

    public void ChangeDimension()
    {
        playerAnimator.SetBool("IsChangingWorld", true);;
    }

    //public void CameraRotation()
    //{
    //    camera.GetComponentInChildren<UlisesCameraController>().enabled = false;
    //    Debug.Log("Rota");
    //    camera.transform.Rotate(0, 0, 180f, Space.Self);
    //}

    public void AnimateCanvas()
    {
        canvasAnimator.SetTrigger("ChangeWorld");
    }

    public void ChangeMap()
    {
        if (!dystopian)
        {
            transform.position = new Vector3(transform.position.x + xOffset, transform.position.y, transform.position.z);
            dystopian = true;
        }
        else
        {
            transform.position = new Vector3(transform.position.x - xOffset, transform.position.y, transform.position.z);
            dystopian = false;
        }
    }

    public bool Dystopian()
    {
        return dystopian;
    }
}
