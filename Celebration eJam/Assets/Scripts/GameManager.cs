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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetItem(string item)
    {
        switch(item)
        {
            case "Hat": //check off hat
                Debug.Log("Got the hat!");
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
}
