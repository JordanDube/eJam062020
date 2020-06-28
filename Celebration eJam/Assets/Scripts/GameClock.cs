using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameClock : MonoBehaviour {
    
    public Vector3 destination;
    public float gameLength;

    public TMPro.TMP_Text gameClockText;

    public bool gameIsOver = false;

    public float addedTime = 0f;

    public float currenttime;
    
    private void Awake() {
        currenttime = gameLength;
        StartCoroutine(StartGameClock());
    }

    private IEnumerator StartGameClock() {
        StartCoroutine(IncrementGameClock());
        yield return CandyCoded.Animate.MoveTo(gameObject, destination, gameLength, Space.World);
        yield return new WaitForSeconds(addedTime);
        
        gameIsOver = true;
        FindObjectOfType<GameManager>().OutOfTime();
    }

    private void Update() {
        gameClockText.text = "Time Left: " + currenttime.ToString();
    }

    private IEnumerator IncrementGameClock() {
//        for (int i = (int) gameLength - 1; i >= 0; i--) {
//            float timeLeft = i + addedTime; 
//            gameClockText.text = "Time Left: " + timeLeft.ToString();
//            yield return new WaitForSeconds(1f);
//        }

        currenttime--;
        
        yield return new WaitForSeconds(1f);
        
        if (currenttime > 0)
            StartCoroutine(IncrementGameClock());
    }

}
