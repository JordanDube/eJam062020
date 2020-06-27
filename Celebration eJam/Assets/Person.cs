using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PersonState { Resting, Roaming, Alerted, Arresting, Petting}

public class Person : MonoBehaviour
{
    private float StateTimer { get; set; }
    public float StateTimerDefault { get; set; } = 2f;

    private bool FacingRight { get; set; }


    PersonState CurrentState = PersonState.Resting;
    // Start is called before the first frame update
    void Start()
    {
        StateTimer = StateTimerDefault;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name +"COLL");
        if (collision.gameObject.tag == "Walls")
        {
            FacingRight = !FacingRight;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name + "Trigger");
        if (collision.gameObject.tag == "Walls")
        {
            FacingRight = !FacingRight;
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

                if(Random.Range(0f, 10f) > 5)
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
                    StateTimer = StateTimerDefault *Random.Range(0.5f,2f);
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


                float moveAmount = Random.Range(1, 100) *0.1f * Time.deltaTime * (FacingRight?1:-1);
                transform.position = new Vector3(transform.position.x + moveAmount, transform.position.y, transform.position.z);
                break;

        }
    }

}
