using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class EndText : MonoBehaviour
{
    [SerializeField] int numItems;
    [SerializeField] string[] textLines;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject[] catPhotos;
    [SerializeField] GameObject[] itemPhotos;
    AudioSource audioSource;
    [SerializeField] AudioClip[] clip;
    // Start is called before the first frame update


    private void Start()
    {

        numItems = GameObject.Find("ItemNumberTracker").GetComponent<ItemNumberTracker>().numberOfItems;

        Debug.Log(numItems);

        audioSource = gameObject.GetComponent<AudioSource>();

        text.text = textLines[numItems];
        audioSource.PlayOneShot(clip[numItems]);

        for(int i = 0; i < numItems; i++)
        {
            catPhotos[i].SetActive(true);
            itemPhotos[i].SetActive(true);
        }
    }

    public void Beginning()
    {
        SceneManager.LoadScene(1);
    }
}
