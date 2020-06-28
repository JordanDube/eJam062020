using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] GameObject instructions;
    [SerializeField] GameObject startScreen;
    [SerializeField] GameObject couch;
    [SerializeField] GameObject catBed;
    [SerializeField] GameObject vase;
    [SerializeField] GameObject shelf;
    [SerializeField] GameObject[] cats;



    public void StartButton()
    {
        instructions.SetActive(true);
        startScreen.SetActive(false);
        couch.SetActive(true);
        catBed.SetActive(true);
        vase.SetActive(true);
        shelf.SetActive(true);
        for(int i = 0; i < cats.Length; i++)
        {
            cats[i].SetActive(false);
        }
    }

    public void Game()
    {
        Debug.Log("Button Pressed");
        SceneManager.LoadScene(1);
    }
}
