using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuCameraController : MonoBehaviour
{

    private float camPos;

    Vector3 xMin, xMax;

    private void Start()
    {
        camPos = (Mathf.Clamp(transform.position.x, -1.3f, 14f));
        xMin = new Vector3(-1.3f, 0.45f, -9.656f);
        xMax = new Vector3(14f, 0.45f, -9.656f);

    }
    private void Update()
    {

        if (Input.GetKey(KeyCode.A))
        {
            gameObject.transform.position -= new Vector3(2, 0, 0) * Time.deltaTime;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            gameObject.transform.position += new Vector3(2, 0, 0) * Time.deltaTime;

        }

        //gameObject.transform.position = Vector3.Lerp(transform.position, xMax, Time.deltaTime * 0.1f);

       
        
        
        if (transform.position.x <= -1)
            transform.DOMove(xMax, 40f, false);
        else if (transform.position.x >= 13.9)
            transform.DOMove(xMin, 40f, false);

        //gameObject.transform.position = Mathf.Clamp(gameObject.transform.position.x)
        //cameraTransform = Mathf.Clamp(cameraTransform.x, -1.3f, 14f);
        // -1.3 to 14
    }
}
