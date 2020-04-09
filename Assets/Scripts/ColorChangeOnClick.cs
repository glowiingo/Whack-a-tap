using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeOnClick : MonoBehaviour
{
    public void updateColorOnClick() {
        if (gameObject.GetComponent<Renderer>().material.color == Color.green)
        {
            gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        }
        else { 
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
    }
        
}
