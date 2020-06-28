using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndText : MonoBehaviour
{
    int numItems;
    [SerializeField] string[] textLines;
    [SerializeField] Text text;
    AudioSource audioSource;
    [SerializeField] AudioClip[] clip;
    // Start is called before the first frame update
    void Awake()
    {
        numItems = GameObject.Find("ItemNumberTracker").GetComponent<ItemNumberTracker>().numberOfItems;
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void Start()
    {
        text.text = textLines[numItems];
        audioSource.PlayOneShot(clip[numItems]);
    }

    public void Beginning()
    {
        SceneManager.LoadScene(1);
    }
}
