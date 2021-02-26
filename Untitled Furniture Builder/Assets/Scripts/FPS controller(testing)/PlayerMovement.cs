using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;
    
    [SerializeField]
    float speed = 12f;
    

    //[SerializeField] private Transform respawnPoint;
    
  

    

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
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);

        }

    }
    
}
