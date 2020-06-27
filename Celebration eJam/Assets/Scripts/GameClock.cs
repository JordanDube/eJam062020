using System.Collections;
using UnityEngine;

public class GameClock : MonoBehaviour {
    
    public Vector3 destination;
    public float gameLength;
    
    private void Awake() {
        StartCoroutine(StartGameClock());
    }

    private IEnumerator StartGameClock() {
        yield return CandyCoded.Animate.MoveTo(gameObject, destination, gameLength, Space.World);
        Debug.Log("Game is over!");
    }

}
