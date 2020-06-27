using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PersonState { Resting, Roaming, Alerted, Chasing, Petting }

public class Person : MonoBehaviour
{
    private float StateTimer { get; set; }
    public float StateTimerDefault { get; set; } = 2f;

    GameManager gameManager; //game manager

    private bool FacingRight { get; set; }

    private Transform TargetTransform { get; set; }
    private float TargetThreshold { get; set; } = 2f;

    public bool canTravel = true;
    public bool travel = false;
    public bool[] areaTracker = new bool[5]; //Keeps track of what scene you're in so you're placed correctly in the next scene


    PersonState CurrentState = PersonState.Resting;
    // Start is called before the first frame update
    void Start()
    {
        StateTimer = StateTimerDefault;
        areaTracker[0] = true;
        gameManager = GameObject.Find("GameManager").gameObject.GetComponent<GameManager>();

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Walls")
        {
            Debug.Log(gameObject.name + " Collided With " + collision.gameObject.name + " With Tag: " + collision.gameObject.tag);

            FacingRight = !FacingRight;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Door")
        {
            if (canTravel)
            {
                canTravel = false;
                int sceneMove = 0;
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
                        gameManager.SwitchScene(sceneMove, i, gameObject.transform);
                        areaTracker[i] = false;
                        areaTracker[sceneMove] = true; //makes the scene you're moving to the current scene
                        break;
                    }
                }

                
            }

        }
    }


    // Update is called once per frame
    void Update()
    {
        //Automatically switch states
        switch (CurrentState)
        {
            case PersonState.Resting:

                if (StateTimer > 0)
                {
                    StateTimer -= Time.deltaTime;
                }
                else
                {
                    StateTimer = StateTimerDefault * Random.Range(5f, 7f);
                    CurrentState = PersonState.Roaming;
                    canTravel = true;

                    //CHANGING STATE TO ROAMING
                    //
                    //ONE TIME STATE CHANGE CODE HERE
                    //

                }

                if (Random.Range(0f, 10f) > 5)
                {
                    FacingRight = !FacingRight;
                }
                break;

            case PersonState.Roaming:

                if (StateTimer > 0)
                {
                    StateTimer -= Time.deltaTime;
                }
                else
                {
                    StateTimer = StateTimerDefault * Random.Range(0.5f, 2f);
                    CurrentState = PersonState.Resting;
                    //CHANGING STATE TO RESTING
                    //
                    //ONE TIME STATE CHANGE CODE HERE
                    //
                }
                break;
        }


        DoAction();

    }


    private void DoAction()
    {
        //IF state is set then do a specific action
        switch (CurrentState)
        {
            case PersonState.Resting:
                break;
            case PersonState.Roaming:


                float roamingMoveAmount = Random.Range(1, 15) * 0.5f * Time.deltaTime * (FacingRight ? 1 : -1);
                transform.position = new Vector3(transform.position.x + roamingMoveAmount, transform.position.y, transform.position.z);
                break;
            case PersonState.Alerted:
                if (TargetTransform != null)
                {

                    FacingRight = TargetTransform.position.x > gameObject.transform.position.x;
                    bool closeToTarget = FacingRight ? TargetTransform.position.x - gameObject.transform.position.x < TargetThreshold : gameObject.transform.position.x - TargetTransform.position.x < TargetThreshold;
                    if (closeToTarget)
                    {
                        TargetTransform = null;
                        StateTimer = StateTimerDefault * Random.Range(0.5f, 2f);

                    }
                    else
                    {
                        float alertedMoveAmount = 15 * Time.deltaTime * (FacingRight ? 1 : -1);
                        transform.position = new Vector3(transform.position.x + alertedMoveAmount, transform.position.y, transform.position.z);
                    }

                }
                else
                {
                    if (StateTimer > 0)
                    {
                        StateTimer -= Time.deltaTime;
                    }
                    else
                    {
                        StateTimer = StateTimerDefault * Random.Range(0.5f, 2f);
                        CurrentState = PersonState.Resting;
                    }
                }
                break;
            case PersonState.Chasing:
                break;
            case PersonState.Petting:
                break;
        }
    }

    public void AlertedToTransform(Transform transform)
    {
        TargetTransform = transform;
        CurrentState = PersonState.Alerted;

        //CHANGING STATE TO ALERTED
        //
        //ONE TIME STATE CHANGE CODE HERE
        //

    }

}