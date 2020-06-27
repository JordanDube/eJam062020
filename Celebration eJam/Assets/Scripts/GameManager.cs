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
            case "Noise Maker": //check off hat
                break;
            case "Streamers": //check off hat
                break;
            case "Cups": //check off hat
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

    public void SwitchScene(int toLocation, int fromLocation)
    {
        switch(toLocation)
        {
            case 0:
                switch (fromLocation)
                {
                    case 1: //cat's location from the kitchen
                        player.gameObject.transform.position = new Vector3(7.27f, -3.21f, 0f);
                        break;
                    case 2: //cat's location from coming dow nthe stairs
                        player.gameObject.transform.position = new Vector3(-7.05f, -3.21f, 0f);
                        break;
                }
                break;
            case 1: //cat's location from livingroom
                player.gameObject.transform.position = new Vector3(37f, -3.21f, 0f);
                break;
            case 2: switch(fromLocation)
            {
                case 0: //cat's location from coming up the stairs
                        player.gameObject.transform.position = new Vector3(60.5f, -3.21f, 0f);
                        break;
                case 3: //cat's location from the bedroom
                        player.gameObject.transform.position = new Vector3(53.8f, -3.21f, 0f);
                        break;
                case 4: //cat's location from the bathroom
                        player.gameObject.transform.position = new Vector3(66.8f, -3.21f, 0f);
                        break;
            }
                break;
            case 3: //cat's location form the hallway
                player.gameObject.transform.position = new Vector3(95.75f, -3.21f, 0f);
                break;
            case 4: //cat's location from hallway
                player.gameObject.transform.position = new Vector3(114.23f, -3.21f, 0f);
                break;

        }
        player.canTravel = false;
        player.travel = false;
        mainCam.transform.position = new Vector3(cameraX[toLocation], 1, -10);

    }
    
}
