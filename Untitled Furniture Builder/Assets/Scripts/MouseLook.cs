using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 1f;

    public Transform playerBody;

    float xRotation = 0f;
    public static bool canMove;

    //public GameObject pauseUI;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        //pauseUI.SetActive(false);
        canMove = true;
    }


    void Update()
    {
       
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        if (canMove)
        {
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }

           // DetectUIandHandleCursor();

        



    }
    /*
    void DetectUIandHandleCursor()
    {
        if (PauseMenu.GameIsPaused)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else if(PauseMenu.GameIsPaused == false)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

        }
    }
    */

}
