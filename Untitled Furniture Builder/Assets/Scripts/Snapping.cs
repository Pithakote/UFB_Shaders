using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snapping : MonoBehaviour
{
    [SerializeField]
    GameObject parent, point1, point2, point3, point4;

    //The desired distance from the camera to the object
    [SerializeField]
    float zDistance = 1.0f;
    Vector3 dist;
    float posX;
    float PosY;

    bool canRotate;
    bool snapped = false;
    Vector3 offset;
    //void OnMouseDown()
    //{
    //    dist = Camera.main.WorldToScreenPoint(transform.position);
    //    posX = Input.mousePosition.x - dist.x;
    //    PosY = Input.mousePosition.y - dist.y;
    //}
    //void OnMouseDrag()
    //{
    //    if (!canRotate)
    //    {
    //        Vector3 curPos = new Vector3(Input.mousePosition.x - posX, Input.mousePosition.y - PosY, zDistance);
    //        Vector3 worldPos = Camera.main.ScreenToWorldPoint(curPos);
    //        transform.position = worldPos;
    //        zDistance += Input.GetAxis("Mouse ScrollWheel") * 2;
    //        this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
    //        this.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
    //    }
    //    else
    //        return;
    //    
    //}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "parent")
        {
            snapped = true;
            parent = other.gameObject;
            offset = transform.position - parent.transform.position;
        }
    }

    private void Update()
    {
        if(snapped)
        {
            transform.position = parent.transform.position + offset;
        }
       
        
        
        
    }


}
