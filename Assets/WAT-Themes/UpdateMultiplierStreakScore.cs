using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateMultiplierStreakScore : MonoBehaviour
{
    GameObject scoreText = null;
    GameObject comboText = null;
    GameObject multiplierText = null;
    int comboStreak = 0;
    int multiplier = 1;
    public void Start()
    {
        while (scoreText == null && multiplierText == null)
        {
            scoreText = GameObject.FindGameObjectWithTag("ScoreText");
            comboText = GameObject.FindGameObjectWithTag("ComboText");
            multiplierText = GameObject.FindGameObjectWithTag("MultiplierText");
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
        updateComboStreak();
        updateMultiplier();
        currentScore = currentScore + (10 * multiplier);

        scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = currentScore.ToString();
 
    }

    public void updateMultiplier() {
        int currentmultiplier = 0;
        string currentMultiplierText = multiplierText.GetComponent<TMPro.TextMeshProUGUI>().text.ToString();

        try
        {
            if (currentMultiplierText == "")
            {
                currentmultiplier = 0;
            }
            else {
                currentmultiplier = int.Parse(currentMultiplierText);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Tried to parse: " + currentMultiplierText);
            Debug.LogError(e.StackTrace);
        }
        multiplier = CheckAndUpdateComboMultiplier(currentmultiplier);
        Debug.Log("Multiplier: " + multiplier.ToString());

        Debug.Log("MultiplierTextObject: " + multiplierText.ToString());
        Debug.Log("MultiplierTextUpdated: " + multiplierText.GetComponent<TMPro.TextMeshProUGUI>().text.ToString());

        multiplierText.GetComponent<TMPro.TextMeshProUGUI>().text = multiplier.ToString();
    }

    public int CheckAndUpdateComboMultiplier(int currentMultiplier) {
        if (currentMultiplier == 4)
        {
            if (comboStreak >= 16)
            {
                currentMultiplier = 8;
            }
            return currentMultiplier;
        }
        else if (currentMultiplier == 2)
        {
            if (comboStreak >= 8)
            {
                currentMultiplier = 4;
            }
            return currentMultiplier;
        }
        else if (currentMultiplier == 1)
        {
            if (comboStreak >= 4)
            {
                currentMultiplier = 2;
            }
            return currentMultiplier;
        }
        else if (currentMultiplier == 0) {
            if (comboStreak == 1) {
                currentMultiplier = 1;
            }
            return currentMultiplier;
        }
        else
        {
            return currentMultiplier;
        }
    }

    public void updateComboStreak()
    {
        int currentCombo = 0;
        string comboStreakText = comboText.GetComponent<TMPro.TextMeshProUGUI>().text.ToString();

        try
        {
            if (comboStreakText == "")
            {
                currentCombo = 0;
            }
            else
            {
                currentCombo = int.Parse(comboStreakText);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Tried to parse: " + comboStreakText);
            Debug.LogError(e.StackTrace);
        }

        currentCombo = currentCombo + 1;
        comboStreak = currentCombo;

        comboText.GetComponent<TMPro.TextMeshProUGUI>().text = currentCombo.ToString();

    }

}
