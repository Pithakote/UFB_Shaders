using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckTrigger : MonoBehaviour
{
    List<GameObject> _children;

    [SerializeField]
    GameObject levelCompleteUI;

    public int numChildren, numTakenPoints;
    bool _isComplete;
    public bool IsComplete { get { return _isComplete; } }
    private void Start()
    {
        numTakenPoints = 0;
        numChildren = this.gameObject.transform.childCount;
    }
    

    public bool checkChildren()
    {
        

        //this._children = new List<GameObject>();
        if (numTakenPoints >= numChildren)
        {
            // players build has all of the required parts!!!
            Debug.Log("YES");
            //levelCompleteUI.SetActive(true);          
            //_isComplete = true;
            return true;
        }
        else
        {
            Debug.Log("NO");
            Debug.Log(numTakenPoints + "  " + numChildren);
            return false;

        }

        #region old unused code
        //for (int i = 0; i < this.gameObject.transform.childCount; i++)
        //{
        //    //if the InnerButtonAddListener is not present continue the loop but don't add
        //    if (gameObject.transform.GetChild(i).GetComponentInChildren<TriggerCheck>() == null)
        //        continue;
        //    //  if (_nextUI.gameObject.transform.GetChild(i).GetComponentInChildren<InnerButtonAddListener>() != null)
        //    _children.Add(gameObject.transform.GetChild(i).gameObject);
        //
        //}




        //foreach ( var child in _children)
        //{
        //
        //    if (child.GetComponent<TriggerCheck>().screwTriggerIsTaken)
        //    {
        //        
        //        if (child.GetComponent<TriggerCheck>().legScrewTaken)
        //        {
        //            return;
        //        }
        //        child.GetComponent<TriggerCheck>().legScrewTaken = true;
        //        //if (child.GetComponent<TriggerCheck>().screwTriggerIsTaken && !child.GetComponent<TriggerCheck>().legTriggerIsTaken)
        //        //{
        //        //    
        //        //
        //        //    
        //        //}
        //
        //    }
        //    else
        //        return;  
        //    
        //}

        #endregion

    }

    private void OnValidate()
    {
        checkChildren();
    }


}
