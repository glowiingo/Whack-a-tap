﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenAlpha : MonoBehaviour
{
    //float transparent = 0f;
    //float opague = 1f;
    // Start is called before the first frame update
    // void Start()
    // {

    // }

    // Update is called once per frame
    // void Update()
    // {
        
    // }

    // Function to change the alpha
    public void changeAlpha() 
    {
        float time = 1f;
        Debug.Log("Test on click.");
        Debug.Log(gameObject.name.ToString());
        Debug.Log(gameObject.GetComponent<Renderer>().material.ToString());

        gameObject.LeanMoveLocalY(10.0f, time);
        gameObject.LeanMoveLocalY(0f, time).setDelay(time);


    }

}