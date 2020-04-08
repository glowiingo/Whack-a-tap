using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateMultiplierStreakScore : MonoBehaviour
{
    public GameObject scoreText = null;
    public GameObject comboText = null;
    public GameObject multiplierText = null;
    int comboStreak = 0;
    int multiplier = 1;
    int baseScore = 10;
    // default to false
    // set to true for testing.
    public bool activeTimer = true;
    // float startTime;
    float timer;
    float waitTime = 10.0f;
    float waitTimeSecond = 15.0f;
    public void Start()
    {
        while (scoreText == null && multiplierText == null) {
            scoreText = GameObject.FindGameObjectWithTag("ScoreText");
            comboText = GameObject.FindGameObjectWithTag("ComboText");
            multiplierText = GameObject.FindGameObjectWithTag("MultiplierText");
            // startTime = Time.time;
        }
    }


    public void Update()
    {

        /**
         * // simulate combo break
         * Uncomment this code for testing
         * and simulating a combo break

        timer += Time.deltaTime;
        if (timer > waitTime)
        {
            activeTimer = false;
        }

        if (timer > waitTimeSecond) {
            activeTimer = true;
        }

        */
    }

    // Logic for game
    // Only call updateScoreText when allowed to, otherwise, update multiplier + comboStreak to 1, 0
    // Boolean to set with timer when activated on a random cube
    // Timer must be called by function that is called half a second earlier than marker (times should be already set)
    // Only when timer is active can update score be called
    // If timer is not active, combo + multiplier set to 0 and 1 repectively

    // Remove spatial mapping functionality so that cubes don't disappear
    // or move scene with camera
    /**
     * UpdateScoreText function is only called when the cubes are hit
     * it should break the combo streak if the timer is not active, 
     * and if it is, then update combo + multiplier properly
     */
    public void updateScoreText() {
        int currentScore = 0;
        string currentScoreText = scoreText.GetComponent<TMPro.TextMeshProUGUI>().text.ToString();
        
        try {
            currentScore = int.Parse(currentScoreText);
        } catch (Exception e) {
            Debug.LogError("Tried to parse: " + currentScoreText);
            Debug.LogError(e.StackTrace);
        }

        Debug.Log("ScoreTextObject: " + scoreText.ToString());
        Debug.Log("ScoreTextUpdated: " + scoreText.GetComponent<TMPro.TextMeshProUGUI>().text.ToString());

        // only add to the combo, multiplier, score if the timer is active, otherwise, break combo
        if (activeTimer)
        {
            updateComboStreak();
            updateMultiplier();
            currentScore = currentScore + (baseScore * multiplier);
        }
        else {
            // break combo
            // set multiplier to 1
            multiplier = 1;
            multiplierText.GetComponent<TMPro.TextMeshProUGUI>().text = multiplier.ToString();
            // set combo to 0
            comboStreak = 0;
            comboText.GetComponent<TMPro.TextMeshProUGUI>().text = comboStreak.ToString();
            // score
        }

        scoreText.GetComponent<TMPro.TextMeshProUGUI>().text = currentScore.ToString();
    }

    public void updateMultiplier() {
        int currentmultiplier = 1;
        string currentMultiplierText = multiplierText.GetComponent<TMPro.TextMeshProUGUI>().text.ToString();

        try {
            if (currentMultiplierText == "") {
                // set multiplier to 1 if multiplier is blank
                currentmultiplier = 1;
            } else {
                // otherwise, get current multiplier
                currentmultiplier = int.Parse(currentMultiplierText);
            }
        } catch (Exception e) {
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
