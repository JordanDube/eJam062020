using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    PlayerInputManager controls; //class for controls
    [HideInInspector]
    public Rigidbody2D rb; //Player rigidbody

    GameManager gameManager; //game manager
    
    bool canInteract = false; //Changes when player is near something interactable
    bool pickup = false;

    private GameObject interactableObject { get; set; }

    bool canEat = false;
    bool eat = false;
    public bool canTravel = false;
    public bool travel = false;
    Vector2 movementInput; //Holds left and right
    string itemHeld = ""; //Saves the string of the picked up item
    int sceneMove; //Holds the numbered scene you want to go to
    bool canJump = false; //if cat is touching ground, you can jump

    public bool[] areaTracker = new bool[5]; //Keeps track of what scene you're in so you're placed correctly in the next scene
    public float jumpHeight = 5f; //How much force is added to the player
    public float movementSpeed = 5f;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private GameClock _gameClock;

    AudioSource audioSource;
    public AudioClip[] meow;

    [SerializeField] Text travelText;

    private void Awake() {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _gameClock = FindObjectOfType<GameClock>();
        
        controls = new PlayerInputManager(); //assign controls class
        controls.Player.Interact.performed += ctx => Interact(); //triggers Spacebar method when spacebar is pressed
        controls.Player.Jump.performed += ctx => Jump();
        
        rb = gameObject.GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager").gameObject.GetComponent<GameManager>();
        areaTracker[0] = true;
        audioSource = gameObject.GetComponent<AudioSource>();
        travelText.enabled = false;
        travelText.text = "";
    }

    private bool GameIsOver() {
        if (_gameClock)
            return _gameClock.gameIsOver;

        return false;
    }
    
    private void Jump()
    {
        if (canTravel)
        {
            travel = true;

        }
        else if (canJump && !GameIsOver())
        {
            rb.velocity = Vector2.up * jumpHeight;
            canJump = false;
        }
    }

    private void Interact()
    {
        Debug.Log("INTERACT");
        Debug.Log(canInteract);
        if (canInteract)
        {
            pickup = true;
            eat = true;
        }
        else
        {
            audioSource.PlayOneShot(meow[UnityEngine.Random.Range(0, 12)]);
        }
    }

    private void Update() {

        InteractWithObject();

        LeftRight();
    }

    private void InteractWithObject()
    {
        if (interactableObject != null)
        {
            if (interactableObject.gameObject.tag == "Item")
            {
                canInteract = true;

                if (pickup)
                {
                    Debug.Log(interactableObject.gameObject.tag + " PICKUP");

                    interactableObject.gameObject.transform.SetParent(gameObject.transform.Find("ItemSpace"));
                    interactableObject.gameObject.transform.localPosition = new Vector3(0, 0, -1);
                    pickup = false;
                    itemHeld = interactableObject.gameObject.name;
                    interactableObject.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
            }
            else if (interactableObject.gameObject.tag == "Food")
            {
                canInteract = true;
                if (eat)
                    interactableObject.gameObject.GetComponent<Food>()?.ApplyUpgrade(this);
            }
        }
    }

    private void LeftRight()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        
        _animator.SetFloat("x", horizontalInput);
        _animator.SetFloat("y", rb.velocity.y);
        
        if(horizontalInput != 0 && !GameIsOver()) {
            _spriteRenderer.flipX = horizontalInput > 0 ? true : false;
            var itemSpace = gameObject.transform.Find("ItemSpace");
            itemSpace.transform.localPosition = new Vector3(horizontalInput>0? 6.08f : -6.08f, itemSpace.transform.localPosition.y,itemSpace.transform.localPosition.z);
            transform.position = new Vector3(transform.position.x + (horizontalInput * movementSpeed * Time.deltaTime), transform.position.y, transform.position.z);
        } 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _animator.SetBool("Grounded", true);
            canJump = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            canJump = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _animator.SetBool("Grounded", false);
            canJump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Item" || collision.gameObject.tag == "Food")
        {
            Debug.Log("ITEM HIT");
            canInteract = true;

            interactableObject = collision.gameObject;
            Debug.Log(interactableObject.name);
        }

        if (collision.gameObject.tag == "Bed")
        {
            gameManager.GetItem(itemHeld);
            Destroy(GameObject.Find(itemHeld));
            itemHeld = "";
        }

        if (collision.gameObject.tag == "Door")
        {
            canTravel = true;
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        
        
        
        
        if (collision.gameObject.tag == "Door")
        {
            travelText.enabled = true;
            travelText.text = collision.gameObject.name;

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

            interactableObject = null;


        }
        canTravel = false;
        travel = false;
        travelText.enabled = false;
        travelText.text = "";
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
