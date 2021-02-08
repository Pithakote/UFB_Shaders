using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class CameraLerpSwitch : MonoBehaviour
{
    public Camera mainCamera;
    public Transform camTransform;

    [SerializeField]
    Transform nextPos;
    public Transform[] camPos;
    public Transform currentPos;
    public TMPro.TMP_Text currentPosText;
    public float transitionSpeed;

    [Header("Camera-Shake Tween Settings")]
    [SerializeField]
    float strength;
    [SerializeField]
    float duration;
    [SerializeField]
    int vibrato;
    [SerializeField]
    float randomness;

    public delegate void onKeyPressed();
    public event onKeyPressed keyPressedEvent;
        

    private void Start()
    {
        currentPos = camPos[3];
        keyPressedEvent += getKeysPressed;
    }

    private void OnDisable()
    {
        keyPressedEvent -= getKeysPressed;
    }

    void doCamShake()
    {
        transform.DOShakePosition(duration, new Vector3(0, 0, strength), vibrato, randomness);
    }

    void getKeysPressed()
    {               
        // check for 1,2,3,4 keyboard input and switch camera position accordingly
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            nextPos = camPos[0];
            currentPosText.text = "Instructions";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            nextPos = camPos[1];
            currentPosText.text = "Build";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            nextPos = camPos[2];
            currentPosText.text = "Kitchen";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            nextPos = camPos[3];
            currentPosText.text = "Top-Down";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            nextPos = camPos[4];
            currentPosText.text = "Radio";
        }
        if (currentPos == nextPos && currentPos != null && nextPos != null)
        {
            doCamShake();
        }
        else
        {
            currentPos = nextPos;
            nextPos = null;
        }        
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Alpha5))
        {
            keyPressedEvent.Invoke();
        }
    }
    private void LateUpdate()   // late update prevents camera stutter and is generally recommended for camera movement
    {
        // Linear interpolate between the current pos and the next desired pos
        gameObject.transform.position = Vector3.Lerp(transform.position, currentPos.position, Time.deltaTime * transitionSpeed);



        // null check
        if (currentPos == null)
            return;

        Vector3 offset = new Vector3(0.1f, 0.1f, 0.1f);
        // Linear interpolation between the angles of the current pos and the desired pos
        Vector3 currentAngle = new Vector3(
           Mathf.LerpAngle(transform.rotation.eulerAngles.x, currentPos.transform.rotation.eulerAngles.x -0.1f, Time.deltaTime * transitionSpeed),
           Mathf.LerpAngle(transform.rotation.eulerAngles.y, currentPos.transform.rotation.eulerAngles.y -0.1f, Time.deltaTime * transitionSpeed),
           Mathf.LerpAngle(transform.rotation.eulerAngles.z, currentPos.transform.rotation.eulerAngles.z -0.1f, Time.deltaTime * transitionSpeed)
            //Mathf.SmoothStep(transform.rotation.eulerAngles.x, currentPos.transform.rotation.eulerAngles.x - 0.1f, Time.deltaTime * transitionSpeed),
            //Mathf.SmoothStep(transform.rotation.eulerAngles.y, currentPos.transform.rotation.eulerAngles.y - 0.1f, Time.deltaTime * transitionSpeed),
            //Mathf.SmoothStep(transform.rotation.eulerAngles.z, currentPos.transform.rotation.eulerAngles.z - 0.1f, Time.deltaTime * transitionSpeed)
            ); 
        transform.eulerAngles = currentAngle;
    }
}
