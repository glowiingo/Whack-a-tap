using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateScore : MonoBehaviour
{
    GameObject scoreText = null;
    public void Start()
    {
        while (scoreText == null)
        {
            scoreText = GameObject.FindGameObjectWithTag("ScoreText");
        }
    }
    public void updateScoreText() {
        int currentScore = 0;
        string currentScoreText = scoreText.GetComponent<TMPro.TextMeshProUGUI>().text.ToString();
        try
        {
            currentScore = int.Parse(currentScoreText);
        }
        catch (Exception e)
        {
            Debug.LogError("Tried to parse: " + currentScoreText);
            Debug.LogError(e.StackTrace);
        }

        Debug.Log("ScoreTextObject: " + scoreText.ToString());
        Debug.Log("ScoreTextUpdated: " + scoreText.GetComponent<TMPro.TextMeshProUGUI>().text.ToString());

        currentScore = currentScore + 10;
        scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = currentScore.ToString();

    }

}
