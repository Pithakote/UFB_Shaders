using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraController : MonoBehaviour
{

    private float camPos;

    float xMin, xMax, xPos;

    private void Start()
    {
        camPos = (Mathf.Clamp(transform.position.x, -1.3f, 14f));
        xPos = gameObject.transform.position.x;

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


        //gameObject.transform.position = Mathf.Clamp(gameObject.transform.position.x)
        //cameraTransform = Mathf.Clamp(cameraTransform.x, -1.3f, 14f);
        // -1.3 to 14
    }
}
