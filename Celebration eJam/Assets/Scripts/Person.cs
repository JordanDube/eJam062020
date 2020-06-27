using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PersonState { Resting, Roaming, Alerted, Chasing, Petting }

public class Person : MonoBehaviour
{
    private float StateTimer { get; set; }
    public float StateTimerDefault { get; set; } = 2f;

    private bool FacingRight { get; set; }

    private Transform TargetTransform { get; set; }
    private float TargetThreshold { get; set; } = 2f;

    PersonState CurrentState = PersonState.Resting;
    // Start is called before the first frame update
    void Start()
    {
        StateTimer = StateTimerDefault;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Walls")
        {
            //Debug.Log("-----------------------------------------------------------");

            Debug.Log(gameObject.name + " Collided With " + collision.gameObject.name + " With Tag: " + collision.gameObject.tag);

            //Debug.Log("Facing Right: " + FacingRight);

            FacingRight = !FacingRight;

            //Debug.Log("After WALL Facing Right: " + FacingRight);

            //Debug.Log("-----------------------------------------------------------");


        }

    }


    // Update is called once per frame
    void Update()
    {
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
                }
                break;
        }


        DoAction();

    }


    private void DoAction()
    {
        switch (CurrentState)
        {
            case PersonState.Roaming:
                

                float roamingMoveAmount = Random.Range(1, 100) * 0.1f * Time.deltaTime * (FacingRight ? 1 : -1);
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

        }
    }

    public void AlertedToTransform(Transform transform)
    {
        TargetTransform = transform;
        CurrentState = PersonState.Alerted;
    }

}