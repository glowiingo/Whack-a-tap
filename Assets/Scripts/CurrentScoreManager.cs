using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Globalization;
using System;

/// <summary>
/// Get the player's final score from the TextUI and store it in a static variable
/// that will be put onto the next scene.
/// </summary>
public class CurrentScoreManager : MonoBehaviour
{
    static public uint gameScore;
    static private GameObject scoreTextObj;

    /// <summary>
    /// Start method is called before the first frame update. Instantiate
    /// static instance variable by getting the game object by its tag.
    /// </summary>
    void Start()
    {
        scoreTextObj = GameObject.FindGameObjectWithTag("ScoreText");
    }

    /// <summary>
    /// Set the static variable that holds the game score.
    /// </summary>
    static public void setFinalGameScore()
    {
        string scoreStr = scoreTextObj.GetComponent<TMPro.TextMeshProUGUI>().text;
        try
        {
            gameScore = Convert.ToUInt32(scoreStr);
        } 
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
}
