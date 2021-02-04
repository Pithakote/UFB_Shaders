using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snapToPos : MonoBehaviour
{
    bool snapped = false;
    GameObject snapparent; // the gameobject this transform will be snapped to
    public Vector3 offset; // the offset of this object's position from the parent
    public Quaternion rotOffset;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (snapped == true)
        {            
            //transform.position = snapparent.transform.position + offset;

            // transform.rotation = Quaternion.Euler(-90f, 0, 0);
            //transform.rotation = snapparent.transform.rotation;

            transform.parent = snapparent.transform;
            transform.position = snapparent.transform.position + offset;
            //transform.rotation = snapparent.transform.rotation;



            Rigidbody rigidbody = this.gameObject.GetComponent<Rigidbody>();
            Destroy(rigidbody);

            

            //this.gameObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
        }

    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "parent")
        {
            snapped = true;
            snapparent = col.gameObject;
            //gameObject.transform.SetParent(snapparent.transform);
            
            //offset = transform.position - snapparent.transform.position; //store relation to parent
            //offset = new Vector3(-0.1115f, -0.3285973f, -0.773f);
        }
    }
}
