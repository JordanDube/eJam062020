using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private Renderer renderer;
    private int emissionIndex;

    private float timeout = 1f;
    bool colorSwitch = true;

    private void Start()
    {
        renderer = gameObject.GetComponent<Renderer>();
    }
    private void Update()
    {

        if (timeout > 0)
        {
            timeout -= Time.deltaTime;
        }
        else {
            if (colorSwitch)
            {
                renderer.material.SetColor("_EmissionColor", Color.red);
                colorSwitch = false;

            }
            else
            {
                renderer.material.SetColor("_EmissionColor", Color.yellow);
                colorSwitch = true;
            }
            timeout = 1f;
        }
    }
}
