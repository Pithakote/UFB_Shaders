using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{  
    private CursorLockMode _wantedMode;

    [SerializeField]
    private GameObject objToOrbit;
    [SerializeField]
    private float orbitSpeed;
    [SerializeField]
    private float panSpeed;


    void Update()
    {

        // when A or D key is pressed - rotate the camera around the objToOrbit's transform (the item of furniture in that level)
        if(Input.GetKey(KeyCode.A))
        {
            gameObject.transform.RotateAround(objToOrbit.transform.position, Vector3.up, orbitSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            gameObject.transform.RotateAround(objToOrbit.transform.position, Vector3.down, orbitSpeed * Time.deltaTime);
        }
        // when W or S is pressed - rotate the camera about itself, which gives a panning up/down effect
        else if (Input.GetKey(KeyCode.W))
        {
            gameObject.transform.Rotate(-panSpeed, 0.0f, 0.0f, Space.Self);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            gameObject.transform.Rotate(panSpeed, 0.0f, 0.0f, Space.Self);
        }
        // call SetCursorState every frame to check if the player has pressed 'Space'
        SetCursorState();
    }
    private void SetCursorState()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_wantedMode == CursorLockMode.Locked)
                Cursor.lockState = _wantedMode = CursorLockMode.None;
            else
                _wantedMode = CursorLockMode.Locked;
        }
        // Apply cursor state
        Cursor.lockState = _wantedMode;
        // Hide cursor when locking
        Cursor.visible = (CursorLockMode.Locked != _wantedMode);
    }
}
