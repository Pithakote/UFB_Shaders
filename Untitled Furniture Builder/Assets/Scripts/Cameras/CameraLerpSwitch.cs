using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraLerpSwitch : MonoBehaviour
{
    public Camera mainCamera;

    public Transform[] camPos;   
    public Transform currentPos;
    public float transitionSpeed;
    public static CameraLerpSwitch instance;
    public TMPro.TMP_Text currentPosText;
    Animator _animator; 


    private void Start()
    {
        currentPos = camPos[3];
        //_animator = mainCamera.GetComponent<Animator>();
        //_animator.SetBool("shakeCam", false);
    }

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        // check for 1,2,3,4 keyboard input and switch camera position accordingly
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentPos = camPos[0];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentPos = camPos[1];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentPos = camPos[2];
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentPos = camPos[3];            
        }   

        if (currentPos == camPos[0])
            currentPosText.text = "Instructions";
        else if(currentPos == camPos[1])
            currentPosText.text = "Build";
        else if (currentPos == camPos[2])
            currentPosText.text = "Kitchen";
        else if (currentPos == camPos[3])
            currentPosText.text = "Top-Down";
    }
    private void LateUpdate()   // late update prevents camera stutter and is generally recommended for camera movement
    {
        // Linear interpolate between the current pos and the next desired pos
        gameObject.transform.position = Vector3.Lerp(transform.position, currentPos.position, Time.deltaTime * transitionSpeed);

        if (currentPos == null)
            return;

        // Linear interpolation between the angles of the current pos and the desired pos
        Vector3 currentAngle = new Vector3(
            Mathf.LerpAngle(transform.rotation.eulerAngles.x, currentPos.transform.rotation.eulerAngles.x, Time.deltaTime * transitionSpeed),
            Mathf.LerpAngle(transform.rotation.eulerAngles.y, currentPos.transform.rotation.eulerAngles.y, Time.deltaTime * transitionSpeed),
            Mathf.LerpAngle(transform.rotation.eulerAngles.z, currentPos.transform.rotation.eulerAngles.z, Time.deltaTime * transitionSpeed)
            );

        transform.eulerAngles = currentAngle;
        /*
        if (Input.GetKeyDown(KeyCode.Alpha1) && currentPos == camPos[0])
        {
            _animator.SetBool("shakeCam", true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && currentPos == camPos[1])
        {
            _animator.SetBool("shakeCam", true);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && currentPos == camPos[2])
        {
            _animator.SetBool("shakeCam", true);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && currentPos == camPos[3])
        {
            _animator.SetBool("shakeCam", true);

        }*/
    }
}
