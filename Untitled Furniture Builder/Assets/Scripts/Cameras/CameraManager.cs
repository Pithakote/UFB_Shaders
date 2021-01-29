using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    private GameObject stoolCamera;
    [SerializeField]
    private GameObject instructionsCamera;
    [SerializeField]
    private GameObject kitchenCamera;
    private void Start()
    {
        instructionsCamera.SetActive(true);
        stoolCamera.SetActive(false);
        kitchenCamera.SetActive(false);
    }

   
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            instructionsCamera.SetActive(true);
            kitchenCamera.SetActive(false);
            stoolCamera.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            stoolCamera.SetActive(true);
            instructionsCamera.SetActive(false);
            kitchenCamera.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            kitchenCamera.SetActive(true);
            stoolCamera.SetActive(false);
            instructionsCamera.SetActive(false);

        }

       
    }
}
