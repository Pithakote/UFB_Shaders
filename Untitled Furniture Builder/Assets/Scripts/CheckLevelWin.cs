using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLevelWin : MonoBehaviour
{
    [SerializeField] GameObject checkButton;
    [SerializeField] int numChildrenRequired;
    GameObject enteredObject;
    
    bool isWin;
    private void Start()
    {
        checkButton.SetActive(false);
    }

    private void Update()
    {
        Debug.Log("numSnapped: " + snap.numSnapped);
    }

    private void OnTriggerEnter(Collider other)
    {
        enteredObject = other.gameObject;

        var meshFilter = other.GetComponent<MeshFilter>();
        var mesh = meshFilter.sharedMesh;
        Debug.Log(mesh.name);

        if (enteredObject.GetComponent<snap>().snap_to_id == -1)
        {
            Debug.Log(other.gameObject.transform.childCount);

            if (enteredObject.transform.childCount >= snap.numSnapped && snap.numSnapped == numChildrenRequired)
            {
                checkButton.SetActive(true);
            }
            else
                Debug.Log("not enough children in object");
        }
        else
            return;
        Debug.Log(other.gameObject.transform.childCount);
       // if (other.gameObject.transform.childCount >= 8)
       // {
       //     Debug.Log(other);
       //     //checkButton.SetActive(true);
       // }
    }

   
   

}
