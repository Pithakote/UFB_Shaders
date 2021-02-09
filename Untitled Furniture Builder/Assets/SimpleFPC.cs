using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFPC : MonoBehaviour
{
    public float moveSpeed = 10.0f; // metres per second
    public float rotRate = 300.0f; // degrees per second
    private CharacterController controller;
    private Transform cameraObject;
    private float yRot;
    private float zRot;
    private Quaternion startRot;
    private float ySpeed;
    private const float ySpeedMin = -5.0f;
    private Vector3 lastVelocity;
    private bool grounded;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraObject = gameObject.transform.GetChild(0);
        startRot = transform.rotation;
        yRot = zRot = 0.0f;
        ySpeed = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (grounded && Input.GetAxis("Jump") > 0.0f)
            ySpeed = 5.0f;
    }

    void FixedUpdate()
    {
        // get the input
        bool jump = Input.GetAxis("Jump") > 0.0f;
        float strafe = Input.GetAxis("Horizontal");
        float forwards = Input.GetAxis("Vertical");
        float xlook = Input.GetAxis("Mouse X");
        float ylook = Input.GetAxis("Mouse Y");

        // rotate the camera angles
        yRot += xlook * rotRate * Time.fixedDeltaTime;
        zRot -= ylook * rotRate * Time.fixedDeltaTime;
        if (yRot > 360.0) yRot -= 360.0f;
        if (yRot < 0.0f) yRot += 360.0f;
        if (zRot > 80.0f) zRot = 80.0f;
        if (zRot < -80.0f) zRot = -80.0f;

        // apply the transforms
        Quaternion controllerRot = Quaternion.AngleAxis(yRot, Vector3.up);
        transform.rotation = startRot * controllerRot;
        Quaternion cameraRot = Quaternion.AngleAxis(zRot, Vector3.right);
        cameraObject.transform.localRotation = cameraRot;
        // ground check
        RaycastHit hitinfo;
        grounded = Physics.Raycast(transform.position, -Vector3.up, out hitinfo, controller.height + 0.1f);
        // move the controller
        Vector3 move;
        if (grounded)
        {
            move = (transform.forward * forwards + transform.right * strafe) * moveSpeed;
            lastVelocity = move;
        }
        else
        {
            move = lastVelocity;
        }
        // gravity
        ySpeed -= 10.0f*Time.fixedDeltaTime;
        if (ySpeed < ySpeedMin)
            ySpeed = ySpeedMin; // terminal velocity
        move += Vector3.up * ySpeed;
        controller.Move(move*Time.fixedDeltaTime);
    }
}
