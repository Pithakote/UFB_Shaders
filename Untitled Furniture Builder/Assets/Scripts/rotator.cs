using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotator : MonoBehaviour
{
	
	public float speed = 100f;
	Vector3 zStart = new Vector3(0,0,1);

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(zStart * Time.deltaTime * speed);
    }
}
