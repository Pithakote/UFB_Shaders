using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snapToPos : MonoBehaviour
{
    bool snapped = false;
    GameObject snapparent; // the gameobject this transform will be snapped to

    [SerializeField] private Vector3 offset; // the offset of this object's position from the parent
    [SerializeField] float yPosOffset;
    [SerializeField] bool setXAxis;

    [SerializeField] GameObject snapPosition;

    GameObject triggerPoint;

    private void Start()
    {
        
    }

    void updateTransParent(bool snapped)
    {
        if (snapped == false)
            return;
        offset = new Vector3(offset.x, yPosOffset, offset.z);
        
        snapPosition.transform.parent = snapparent.transform;
        snapPosition.transform.up = snapparent.transform.up;
        
        snapPosition.transform.localPosition = new Vector3(0, snapparent.transform.localPosition.y, 0);
        
        transform.parent = snapPosition.transform;
        transform.up = snapPosition.transform.up;
        transform.localPosition = new Vector3(0, offset.y, 0);  


        Rigidbody rigidbody = this.gameObject.GetComponent<Rigidbody>();
        Destroy(rigidbody);
       

        if (setXAxis)
            transform.right = snapparent.transform.right;
        else return;
    }
    void OnTriggerEnter(Collider col)
    {

        if (triggerPoint.GetComponent<TriggerCheck>() != null )
        {
            snapped = true;
                    snapparent = col.gameObject;
                  updateTransParent(snapped);
        }

        //triggerPoint = col.gameObject;

        //if (triggerPoint.GetComponent<TriggerCheck>() != null && !triggerPoint.GetComponent<TriggerCheck>().screwTriggerIsTaken)
        //{
        //    triggerPoint.GetComponent<TriggerCheck>().screwTriggerIsTaken = true;
        //    triggerPoint.gameObject.transform.parent.GetComponent<CheckTrigger>().numTakenPoints++;
        //
        //        snapped = true;
        //        snapparent = col.gameObject;
        //        updateTransParent(snapped);
        //       
        //}





    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other == null && other.gameObject != triggerPoint)
    //        return;
    //    triggerPoint = other.gameObject;
    //    triggerPoint.GetComponent<TriggerCheck>().triggerIsTaken = false;      
    //}

}
   


