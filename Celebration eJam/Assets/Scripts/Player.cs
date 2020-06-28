using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public bool canPeek = false;
    public bool peek = false;

    Vector2 movementInput; //Holds left and right
    string itemHeld = ""; //Saves the string of the picked up item
    int sceneMove; //Holds the numbered scene you want to go to
    int scenePeek; //Holds the numbered scene you want to peek to
    bool canJump = false; //if cat is touching ground, you can jump
    public bool badKitty = false;
    
    public bool[] areaTracker = new bool[5]; //Keeps track of what scene you're in so you're placed correctly in the next scene
    public float jumpHeight = 5f; //How much force is added to the player
    public float movementSpeed = 5f;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;

    private GameClock _gameClock;
    private Vector3 startingPoint;

    AudioSource audioSource;
    public AudioClip[] meow;
    public AudioClip[] itemSounds;
    public AudioClip angryParent;
    public AudioClip depositItem;
    public AudioClip bedBounce;

    [SerializeField] TMPro.TMP_Text travelText;

    private void Awake() {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();

        _gameClock = FindObjectOfType<GameClock>();
        startingPoint = transform.position;

        controls = new PlayerInputManager(); //assign controls class
        controls.Player.Interact.performed += ctx => Interact(); //triggers Spacebar method when spacebar is pressed
        controls.Player.Jump.performed += ctx => Jump();

        rb = gameObject.GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager").gameObject.GetComponent<GameManager>();
        areaTracker[0] = true;
        audioSource = gameObject.GetComponent<AudioSource>();
        if (travelText != null)
        {
            travelText.enabled = false;
            travelText.text = "";
        }
    }

    private bool GameIsOver() {
        if (badKitty)
            return true;
        
        if (_gameClock)
            return _gameClock.gameIsOver;

        return false;
    }

    private void Jump() {
        if (canTravel) {
            travel = true;
        }
        else if (canJump && !GameIsOver()) {
            // _boxCollider2D.enabled = false;
            Physics2D.IgnoreLayerCollision(11, 12, true);
            rb.velocity = Vector2.up * jumpHeight;
            canJump = false;
        }
    }

    private void Interact()
    {
        Debug.Log("INTERACT");
        Debug.Log(canInteract);
        if (canInteract && itemHeld == "")
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

        if (peek == false && badKitty == false)
        {
            LeftRight();
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (canPeek)
            {
                peek = true;
                gameManager.PeekScene(scenePeek);
            }
        }

        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            peek = false;

            for (int i = 0; i < areaTracker.Length; i++)
            {
                if (areaTracker[i])
                {
                    Debug.Log(i);
                    gameManager.PeekScene(i);
                    break;
                }
            }

        }
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
                    eat = false;
                    pickup = false;
                    itemHeld = interactableObject.gameObject.name;
                    interactableObject.gameObject.GetComponent<BoxCollider2D>().enabled = false;
                    
                    FindObjectsOfType<LineOfSight>().ToList().ForEach(los => {
                        los.EnableLineOfSight();
                    });

                    switch(itemHeld) //plays sound depending on item
                    {

                        case "Hat":
                            audioSource.PlayOneShot(itemSounds[0]);
                            break;
                        case "Noise Maker":
                            audioSource.PlayOneShot(itemSounds[1]);
                            break;
                        case "Streamers":
                            audioSource.PlayOneShot(itemSounds[2]);
                            break;
                        case "Cake":
                            audioSource.PlayOneShot(itemSounds[3]);
                            break;
                        case "Cups":
                            audioSource.PlayOneShot(itemSounds[4]);
                            break;
                        case "Cards":
                            audioSource.PlayOneShot(itemSounds[5]);
                            break;
                        case "Balloon":
                            audioSource.PlayOneShot(itemSounds[6]);
                            break;
                        case "PartyPopper":
                            audioSource.PlayOneShot(itemSounds[7]);
                            break;
                    }
                }
            }
            else if (interactableObject.gameObject.tag == "Food")
            {
                canInteract = true;
                if (eat) {
                    interactableObject.gameObject.GetComponent<Food>()?.ApplyUpgrade(this);
                    audioSource.PlayOneShot(itemSounds[8]);
                    eat = false;
                    pickup = false;
                    canInteract = false;
                }
                    
            }
        }
    }

    private void LeftRight()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        
        _animator.SetFloat("x", horizontalInput);
        _animator.SetFloat("y", rb.velocity.y);
        
        if (rb.velocity.y <= 0f && Physics2D.GetIgnoreLayerCollision(11, 12)) {
            Physics2D.IgnoreLayerCollision(11, 12, false);
            // _boxCollider2D.enabled = true;
        }

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

        if(collision.gameObject.name == "Bed")
        {
            audioSource.PlayOneShot(bedBounce);
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
            if(itemHeld != "")
            {
                gameManager.GetItem(itemHeld);
                audioSource.PlayOneShot(depositItem);
                Destroy(GameObject.Find(itemHeld));
                itemHeld = "";
            }
            
            
            FindObjectsOfType<LineOfSight>().ToList().ForEach(los => {
                los.DisableLineOfSight();
            });
        }

        if (collision.gameObject.tag == "Door")
        {
            canTravel = true;
            canPeek = true;
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            travelText.enabled = true;
            travelText.text = collision.gameObject.name;

            if (travel && peek == false)
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

            if (canPeek)
            {
                switch (collision.gameObject.name)
                {
                    case "ToLivingRoom":
                        scenePeek = 0;
                        break;
                    case "ToKitchen":
                        scenePeek = 1;
                        break;
                    case "ToUpstairs":
                        scenePeek = 2;
                        break;
                    case "ToBedroom":
                        scenePeek = 3;
                        break;
                    case "ToBathroom":
                        scenePeek = 4;
                        break;
                }

            }
        }
    }

    public void ResetToSpawn() {
        if (itemHeld.Length > 0) {
            GameObject itemObj = GameObject.Find(itemHeld);
            itemObj?.GetComponent<Item>().ResetItem();
            itemHeld = "";

        }

        FindObjectsOfType<LineOfSight>().ToList().ForEach(los => {
            los.DisableLineOfSight();
        });
        
        for (int i = 0; i < areaTracker.Length; i++)
        {
            if (areaTracker[i])
            {
                gameManager.SwitchPlayerScene(0, i);
                audioSource.PlayOneShot(angryParent);
                areaTracker[i] = false;
                areaTracker[0] = true; //makes the scene you're moving to the current scene
                break;
            }
        }

        transform.position = startingPoint + new Vector3(0f, 2f, 0f);

    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Item" || collision.gameObject.tag == "Food") {
            //Debug.Log("Item left");
            canInteract = false;

            interactableObject = null;


        }
        canTravel = false;
        canPeek = false;
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
