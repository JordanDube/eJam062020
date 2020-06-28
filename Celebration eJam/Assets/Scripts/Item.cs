using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private Renderer emissionRenderer;
    private int emissionIndex;
    private Color emissionDefaultColor;

    private float timeout = 3f;
    bool colorSwitch = false;

    private Vector3 spawnPoint;
    private BoxCollider2D _boxCollider2D;
    
    private void Start() {
        spawnPoint = transform.position;
        _boxCollider2D = GetComponent<BoxCollider2D>();
        emissionRenderer = gameObject.GetComponent<Renderer>();
        //emissionDefaultColor = emissionRenderer.material.GetColor("_EmissionColor");
    }
    //private void Update()
    //{

    //    if (timeout > 0)
    //    {
    //        timeout -= Time.deltaTime;
    //    }
    //    else {
    //        if (colorSwitch)
    //        {
    //            emissionRenderer.material.SetColor("_EmissionColor", emissionDefaultColor);
    //            colorSwitch = false;

    //        }
    //        else
    //        {
    //            emissionRenderer.material.SetColor("_EmissionColor", Color.grey);

    //            colorSwitch = true;
    //        }
    //        timeout = 3f;
    //    }
    //}

    public void ResetItem() {

        transform.parent = null;
        transform.position = spawnPoint;
        _boxCollider2D.enabled = true;
    }
}
