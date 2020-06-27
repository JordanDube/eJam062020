using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    [SerializeField] GameObject instructions;

    public void StartButton()
    {
        instructions.SetActive(true);
    }

    public void Game()
    {
        SceneManager.LoadScene(1);
    }
}
