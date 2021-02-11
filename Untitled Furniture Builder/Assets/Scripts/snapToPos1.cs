using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snapToPos1 : MonoBehaviour
{
    bool snapped = false;
    GameObject snapparent; // the gameobject this transform will be snapped to

    [SerializeField] private Vector3 offset; // the offset of this object's position from the parent
    [SerializeField] float yPosOffset;
    [SerializeField] bool setXAxis;

    

    private void Start()
    {
        
    }

    void updateTransParent(bool snapped)
    {
        if (snapped == false)
            return;
        offset = new Vector3(offset.x, yPosOffset, offset.z);

        //snapPosition.transform.parent = snapparent.transform;
        //snapPosition.transform.up = snapparent.transform.up;
        //
        //snapPosition.transform.localPosition = new Vector3(0, snapparent.transform.localPosition.y, 0);
        ////snapPosition.transform.localRotation = snapparent.transform.localRotation;
        //
        //// offset = transform.localPosition - snapparent.transform.position;
        //transform.parent = snapPosition.transform;
        //transform.up = snapPosition.transform.up;
        //   transform.localPosition = new Vector3(0, offset.y, 0);
        //// transform.localPosition = Vector3.zero;
        ////  transform.localRotation = snapPosition.transform.localRotation;


        transform.parent = snapparent.transform;
        transform.localPosition = snapparent.transform.localPosition + offset;
        transform.up = snapparent.transform.up;
        //transform.localRotation = snapparent.transform.localRotation;





        Rigidbody rigidbody = this.gameObject.GetComponent<Rigidbody>();
        Destroy(rigidbody);
        if (rigidbody == null)
            return;
        //rigidbody.constraints = RigidbodyConstraints.FreezeAll;

        //rigidbody.isKinematic = true;
        gameObject.GetComponent<MeshCollider>().enabled = false;

        if (setXAxis)
            transform.right = snapparent.transform.right;
        else return;
    }
    void OnTriggerEnter(Collider col)
    {                  


        if (col.tag == "parent")
        {                     
                // if col.getcomponent<drag> = true, then do it.
                snapped = true;
                snapparent = col.gameObject;
                updateTransParent(snapped);                       
        }
        
       
       

    }

}
    // if leg already attached
    // do nothing
    // if no leg attached
    // attach leg

    // do a check so each point only has 1 leg attached


