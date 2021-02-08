using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drag : MonoBehaviour
{
    
    //The desired distance from the camera to the object
    [SerializeField]
    float zDistance = 1.0f;
    Vector3 dist;
    float posX;
    float PosY;
    bool canRotate;

    [SerializeField] Transform objRotateAround;
   

    void OnMouseDown()
    {
        Rigidbody rigidbody = this.gameObject.GetComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.None;
        dist = Camera.main.WorldToScreenPoint(transform.position);
        posX = Input.mousePosition.x - dist.x;
        PosY = Input.mousePosition.y - dist.y;
    }
    void OnMouseDrag()
    {
        if (!canRotate)
        {
            Vector3 curPos = new Vector3(Input.mousePosition.x - posX, Input.mousePosition.y - PosY, zDistance);
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);
            transform.position = worldPos;
            zDistance += Input.GetAxis("Mouse ScrollWheel") * 2;
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);

            Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();
            Cursor.visible = true;
            rigidbody.constraints = RigidbodyConstraints.None;

        }
        else if (canRotate)
        {
            Rigidbody rigidbody = gameObject.GetComponent<Rigidbody>();

            float mouseDX = Input.GetAxis("Mouse X");
            float mouseDY = Input.GetAxis("Mouse Y");
            rigidbody.constraints = RigidbodyConstraints.FreezePosition;

            //gameObject.transform.Rotate(new Vector3(-mouseDX, 0, mouseDY) * Time.deltaTime * 300);
            if (objRotateAround == null)
                objRotateAround = this.gameObject.transform;
            gameObject.transform.RotateAround(objRotateAround.position, new Vector3(mouseDX, 0, mouseDY), Time.deltaTime * 200);
            Cursor.visible = false;
        }      
        
    }  

    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            canRotate = true;
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            canRotate = false;
        }

        if (Input.GetMouseButtonDown(1))
        {
            print("RMB clicked");
            //pickedUp.transform.position = goalPosition;
            Rigidbody rigidbody = this.gameObject.GetComponent<Rigidbody>();
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
       
    }

    


}
