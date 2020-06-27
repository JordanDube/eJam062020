using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    PlayerInputManager controls; //class for controls
    Rigidbody2D rb; //Player rigidbody
    Keyboard kb;
    bool canInteract = false; //Changes when player is near something interactable
    Vector2 movementInput; //Holds left and right

    public float jumpHeight = 5f; //How much force is added to the player
    public float movementSpeed = 5f;
    private void Awake()
    {
        controls = new PlayerInputManager(); //assign controls class
        controls.Player.Interact.performed += ctx => Interact(); //triggers Spacebar method when spacebar is pressed
        controls.Player.Jump.performed += ctx => Jump();
        kb = InputSystem.GetDevice<Keyboard>();
        rb = gameObject.GetComponent<Rigidbody2D>();

    }

    private void Jump()
    {
        Debug.Log("Jump performed");
        rb.velocity = Vector2.up * jumpHeight;
    }

    private void Move(float direction)
    {
        transform.position = new Vector2(direction * movementSpeed * Time.deltaTime, transform.position.y);
    }

    private void Interact()
    {

        if (canInteract)
        {
            //call method if something is interactable
        }
        else
        {
            Debug.Log("Meow");
            //Audio to make meow
            //Animation for meow
        }
    }

    private void Update()
    {
        LeftRight();
    }

    private void LeftRight()
    {

        float horizontalInput = Input.GetAxis("Horizontal");
        if(horizontalInput != 0)
        {
            transform.position = new Vector3(transform.position.x + (horizontalInput * movementSpeed * Time.deltaTime), transform.position.y, transform.position.z);
        }
        
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
