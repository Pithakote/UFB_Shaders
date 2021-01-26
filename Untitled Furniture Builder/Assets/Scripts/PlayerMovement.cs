using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Camera playerCamera;
    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    //[SerializeField] private Transform respawnPoint;
    [SerializeField] private GameObject player;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    

    //public AudioSource jump;

    bool isGrounded;
    Vector3 velocity;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        controller.height = 1.5f;
    }
    void Update()
    {
        if(Cursor.visible == false)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
               // jump.Play();
            }

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);
        }

    }
    
}
