using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemNumberTracker : MonoBehaviour
{
    public int numberOfItems = 0;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

}

