using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Globalization;
using System;

public class CurrentScoreManager : MonoBehaviour
{
    static public uint gameScore;
    private GameObject scoreTextObj;

    void Start()
    {
        scoreTextObj = GameObject.FindGameObjectWithTag("ScoreText");
    }

    public void setFinalGameScore()
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
