using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameClock : MonoBehaviour {
    
    public Vector3 destination;
    public float gameLength;

    public Text titleText;

    public bool gameIsOver = false;
    
    private void Awake() {
        StartCoroutine(StartGameClock());
    }

    private IEnumerator Start() {
        titleText.enabled = true;
        yield return new WaitForSeconds(2f);
        titleText.enabled = false;
    }

    private IEnumerator StartGameClock() {
        yield return CandyCoded.Animate.MoveTo(gameObject, destination, gameLength, Space.World);
        titleText.text = "GAME OVER!";
        titleText.enabled = true;
        gameIsOver = true;
    }

}
