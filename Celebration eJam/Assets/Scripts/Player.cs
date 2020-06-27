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
        Debug.Log("INTERACT");
        Debug.Log(canInteract);
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
            Debug.Log("ITEM HIT");
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
            Debug.Log(pickup);

            if (pickup)
            {
                Debug.Log(collision.gameObject.tag +" PICKUP");

                collision.gameObject.transform.SetParent(gameObject.transform.GetChild(0));
                collision.gameObject.transform.localPosition = new Vector3(0, 0, 0);
                pickup = false;
                itemHeld = collision.gameObject.name;
                collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        } 
        else if (collision.gameObject.tag == "Food") {
            canInteract = true;
            if (eat) 
                collision.gameObject.GetComponent<Food>()?.ApplyUpgrade(this);
        }
        
        if (collision.gameObject.tag == "Door")
        {
            if (travel)
            {
                canTravel = false;
                switch (collision.gameObject.name)
                {
                    case "ToLivingRoom":
                        travel = false;
                        sceneMove = 0;
                        break;
                    case "ToKitchen":
                        travel = false;
                        sceneMove = 1;
                        break;
                    case "ToUpstairs":
                        travel = false;
                        sceneMove = 2;
                        break;
                    case "ToBedroom":
                        travel = false;
                        sceneMove = 3;
                        break;
                    case "ToBathroom":
                        travel = false;
                        sceneMove = 4;
                        break;
                }

                for (int i = 0; i < areaTracker.Length; i++)
                {
                    if (areaTracker[i])
                    {
                        gameManager.SwitchPlayerScene(sceneMove, i);
                        areaTracker[i] = false;
                        areaTracker[sceneMove] = true; //makes the scene you're moving to the current scene
                        break;
                    }
                }
            }
            else
            {
                canTravel = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Item" || collision.gameObject.tag == "Food") {
            Debug.Log("Item left");
            canInteract = false;
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
