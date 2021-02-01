using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabAndDrop : MonoBehaviour
{


  
 
    //Define the camera that we will use
    //Don't forget to set this to the camera in your game
    [SerializeField]
    Camera screenCamera;

    [SerializeField]
    float rotSpeed;
     
    //The desired distance from the camera to the object
    [SerializeField]
    float zDistance = 1.0f;
    private void OnMouseDrag()
    {

        var mousePos = Input.mousePosition;
    
        // Set the position of the transform to a position defined by the mouse
        // which is zDistance units away from the screenCamera
        this.transform.position = screenCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, zDistance));
        zDistance += Input.GetAxis("Mouse ScrollWheel") * 2;
    }
    private void OnMouseUpAsButton()
    {
        Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
        Vector3 vel = rigidbody.velocity;
        vel = new Vector3(0, 0, 0);
        rigidbody.velocity = vel;
        rigidbody.angularVelocity = vel;
    }
    void Update()
    {  

        // rotation
        if (Input.GetKey(KeyCode.R))
        {
            
            Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();

            float mouseDX = Input.GetAxis("Mouse X");
            float mouseDY = Input.GetAxis("Mouse Y");
            rigidbody.constraints = RigidbodyConstraints.FreezePosition;

            gameObject.transform.Rotate(new Vector3(mouseDX, mouseDY, 0) * Time.deltaTime * rotSpeed);

            Cursor.visible = false;


        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            
            Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
            Cursor.visible = true;
            rigidbody.constraints = RigidbodyConstraints.None;

        }

    }
    
}
