using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snapToPos : MonoBehaviour
{
    bool snapped = false;
    GameObject snapparent; // the gameobject this transform will be snapped to

    [SerializeField] private Vector3 offset; // the offset of this object's position from the parent
    [SerializeField] float yPosOffset;

    void updateTransParent(bool snapped)
    {
        if (snapped == false)
            return;

        offset = new Vector3(offset.x, offset.y, yPosOffset);

        transform.parent = snapparent.transform;
        transform.localPosition = snapparent.transform.localPosition + offset;
        transform.up = snapparent.transform.up;

        Rigidbody rigidbody = this.gameObject.GetComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.FreezeAll;

        gameObject.GetComponent<MeshCollider>().enabled = false;       
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

    // do a check so each point only has 1 leg attached

}
