using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    PlayerInputManager controls; //class for controls
    [HideInInspector]
    public Rigidbody2D rb; //Player rigidbody

    GameManager gameManager; //game manager
    
    bool canInteract = false; //Changes when player is near something interactable
    bool pickup = false;
    Vector2 movementInput; //Holds left and right
    string itemHeld = ""; //Saves the string of the picked up item

    public float jumpHeight = 5f; //How much force is added to the player
    public float movementSpeed = 5f;
    private void Awake()
    {
        controls = new PlayerInputManager(); //assign controls class
        controls.Player.Interact.performed += ctx => Interact(); //triggers Spacebar method when spacebar is pressed
        controls.Player.Jump.performed += ctx => Jump();
        
        rb = gameObject.GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager").gameObject.GetComponent<GameManager>();
    }

    private void Jump()
    {
        Debug.Log("Jump performed");
        rb.velocity = Vector2.up * jumpHeight;
    }

    private void Interact()
    {

        if (canInteract)
        {
            //call method if something is interactable
            pickup = true;
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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Item")
        {
            canInteract = true;
        }

        if (collision.gameObject.tag == "Bed")
        {
            gameManager.GetItem(itemHeld);
            Destroy(GameObject.Find(itemHeld));
            itemHeld = "";
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            canInteract = true;
            if (pickup)
            {
                collision.gameObject.transform.SetParent(gameObject.transform.GetChild(0));
                collision.gameObject.transform.localPosition = new Vector3(0, 0, 0);
                pickup = false;
                itemHeld = collision.gameObject.name;
                collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
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
