using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField] int[] cameraX;
    [SerializeField] GameObject mainCam;
    Player player;

    private void Awake()
    {
        player = GameObject.Find("Cat").gameObject.GetComponent<Player>();
    }

    public void GetItem(string item)
    {
        switch(item)
        {
            case "Hat": //check off hat
                Debug.Log("Got the hat");
                break;
            case "Noise Maker": //check off rubber duck
                break;
            case "Streamers": //check off blinds
                break;
            case "Cups": //check off cups
                break;
            case "Cake": //check off hat
                break;
            case "Cards": //check off hat
                break;
            case "Balloon": //check off hat
                break;
            case "PartyPopper": //check off hat
                break;
        }
    }

    public void SwitchPlayerScene(int toLocation, int fromLocation)
    {
        SwitchScene(toLocation, fromLocation,player.gameObject.transform);

        player.canTravel = false;
        player.travel = false;
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
                        transform.position = new Vector3(7.27f, transform.position.y, 0f);
                        break;
                    case 2: //cat's location from coming dow nthe stairs
                        transform.position = new Vector3(-7.05f, transform.position.y, 0f);
                        break;
                }
                break;
            case 1: //cat's location from livingroom
                transform.position = new Vector3(24f, transform.position.y, 0f);
                break;
            case 2: switch(fromLocation)
            {
                case 0: //cat's location from coming up the stairs
                        transform.position = new Vector3(60.5f,transform.position.y, 0f);
                        break;
                case 3: //cat's location from the bedroom
                        transform.position = new Vector3(53.8f,transform.position.y, 0f);
                        break;
                case 4: //cat's location from the bathroom
                        transform.position = new Vector3(66.8f,transform.position.y, 0f);
                        break;
            }
                break;
            case 3: //cat's location form the hallway
                transform.position = new Vector3(95.75f, transform.position.y, 0f);
                break;
            case 4: //cat's location from hallway
                transform.position = new Vector3(114.23f, transform.position.y, 0f);
                break;

        }

    }
    
}
