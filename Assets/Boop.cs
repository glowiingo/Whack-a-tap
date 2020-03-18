using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Boop : MonoBehaviour
{

    public void boop()
    {
        //Text newText = transform.Find("Text").GetComponent<Text>();
        //newText.text = text;
        //Debug.Log("Boop!");
        Console.WriteLine("Boop!");
        Console.ReadKey();
    }
}
