using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTrigger : MonoBehaviour
{
    List<GameObject> _children;

    public void checkChildren()
    {
        this._children = new List<GameObject>();


        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            //if the InnerButtonAddListener is not present continue the loop but don't add
            if (gameObject.transform.GetChild(i).GetComponentInChildren<TriggerCheck>() == null)
                continue;
            //  if (_nextUI.gameObject.transform.GetChild(i).GetComponentInChildren<InnerButtonAddListener>() != null)
            _children.Add(gameObject.transform.GetChild(i).gameObject);

        }

        foreach ( var child in _children)
        {

            if (child.GetComponent<TriggerCheck>().triggerIsTaken)
            {
                Debug.Log(child.name + child.GetComponent<TriggerCheck>().triggerIsTaken);
            }
            else
            {
                Debug.Log(child.name + child.GetComponent<TriggerCheck>().triggerIsTaken);
            }

        }
    }
}
