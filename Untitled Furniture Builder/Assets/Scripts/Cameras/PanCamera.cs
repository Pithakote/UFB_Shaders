﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanCamera : MonoBehaviour
{
    private CursorLockMode _wantedMode;

   
   
    [SerializeField]
    private float panSpeed;
    private Quaternion camRotation;

    void Update()
    {       
        // when A or D key is pressed - rotate the camera around the objToOrbit's transform (the item of furniture in that level)
        if (Input.GetKey(KeyCode.A))
        {           
            transform.Rotate(0.0f, -panSpeed, 0.0f, Space.Self);       
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0.0f, panSpeed, 0.0f, Space.Self);
        }
        // when W or S is pressed - rotate the camera about itself, which gives a panning up/down effect
        else if (Input.GetKey(KeyCode.W))
        {
            transform.Rotate(-panSpeed, 0.0f, 0.0f, Space.Self);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            transform.Rotate(panSpeed, 0.0f, 0.0f, Space.Self);
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
