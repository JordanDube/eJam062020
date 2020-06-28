﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool hat = false;
    public bool noiseMaker = false;
    public bool streamers = false;
    public bool cups = false;
    public bool cake = false;
    public bool cards = false;
    public bool balloon = false;
    public bool partyPopper = false;

    bool[] itemChecks = new bool[8];
    [SerializeField] int[] cameraX;
    [SerializeField] GameObject mainCam;
    [SerializeField] GameObject []items;
    Player player;
    ItemNumberTracker itemTracker;

    private void Awake()
    {
        player = GameObject.Find("Cat").gameObject.GetComponent<Player>();
        itemTracker = GameObject.Find("ItemNumberTracker")?.gameObject?.GetComponent<ItemNumberTracker>() ?? new ItemNumberTracker();
        itemTracker.numberOfItems = 0;
        for(int i = 0; i < itemChecks.Length; i++)
        {
            itemChecks[i] = false;
        }
    }

    public void GetItem(string item)
    {
        switch(item)
        {
            case "Hat": //check off hat
                itemChecks[0] = true;
                break;
            case "Noise Maker": //check off hat
                itemChecks[1] = true;
                break;
            case "Streamers": //check off hat
                itemChecks[2] = true;
                break;
            case "Cups": //check off hat
                itemChecks[3] = true;
                break;
            case "Cake": //check off hat
                itemChecks[4] = true;
                break;
            case "Cards": //check off hat
                itemChecks[5] = true;
                break;
            case "Balloon": //check off hat
                itemChecks[6] = true;
                break;
            case "PartyPopper": //check off hat
                itemChecks[7] = true;
                break;
        }

        int itemCounter = 0;
        for (int i = 0; i < itemChecks.Length; i++)
        {
            if(itemChecks[i])
            {
                var itemText = items[i].GetComponent<TMP_Text>();
                itemText.color = Color.green;
                itemText.text = "<s><b>"+ itemText.text +"</b></s>";
                itemCounter++;
            }
        }

        if(itemCounter == 8)
        {
            OutOfTime();
        }
    }

    public void SwitchPlayerScene(int toLocation, int fromLocation)
    {
        SwitchScene(toLocation, fromLocation,player.gameObject.transform);

        player.canTravel = false;
        player.travel = false;
        mainCam.transform.position = new Vector3(cameraX[toLocation], 1, -10);
        GameObject.Find("Travel text").GetComponent<TMPro.TMP_Text>().enabled = false;
        GameObject.Find("Travel text").GetComponent<TMPro.TMP_Text>().text = "";
    }

    public void PeekScene(int toLocation)
    {
        mainCam.transform.position = new Vector3(cameraX[toLocation], 1, -10);
    }

    public void SwitchScene(int toLocation, int fromLocation,Transform transform = null)
    {
        if(transform == null)
        {
            return;
        }

        switch(toLocation)
        {
            case 0:
                switch (fromLocation)
                {
                    case 1: //cat's location from the kitchen
                        transform.position = new Vector3(7.27f, transform.position.y, transform.position.z);
                        break;
                    case 2: //cat's location from coming dow nthe stairs
                        transform.position = new Vector3(-7.05f, transform.position.y, transform.position.z);
                        break;
                }
                break;
            case 1: //cat's location from livingroom
                transform.position = new Vector3(24f, transform.position.y, transform.position.z);
                break;
            case 2: switch(fromLocation)
            {
                case 0: //cat's location from coming up the stairs
                        transform.position = new Vector3(60.5f,transform.position.y, transform.position.z);
                        break;
                case 3: //cat's location from the bedroom
                        transform.position = new Vector3(53.8f,transform.position.y, transform.position.z);
                        break;
                case 4: //cat's location from the bathroom
                        transform.position = new Vector3(66.8f,transform.position.y, transform.position.z);
                        break;
            }
                break;
            case 3: //cat's location form the hallway
                transform.position = new Vector3(95.75f, transform.position.y, transform.position.z);
                break;
            case 4: //cat's location from hallway
                transform.position = new Vector3(114.23f, transform.position.y, transform.position.z);
                break;

        }

    }
    
    public void OutOfTime()
    {
        for(int i = 0; i < itemChecks.Length; i++)
        {
            if(itemChecks[i])
            {
                itemTracker.numberOfItems++;
            }
        }
        SceneManager.LoadScene("End");
    }
}
