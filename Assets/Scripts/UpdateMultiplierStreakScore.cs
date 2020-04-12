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
    const int BASE_SCORE = 10;
    int count = 0;

    const int MULTIPLIER_LEVEL_ZERO = 0;
    const int MULTIPLIER_LEVEL_ONE = 1;
    const int MULTIPLIER_LEVEL_TWO = 2;
    const int MULTIPLIER_LEVEL_THREE = 4;
    const int MULTIPLIER_LEVEL_FOUR = 8;

    const int COMBO_REQ_LVL_ONE = 1;
    const int COMBO_REQ_LVL_TWO = 4;
    const int COMBO_REQ_LVL_THREE = 8;
    const int COMBO_REQ_LVL_FOUR = 16;

    public void Start()
    {
        // waits for the scoretext and multipliertext to load or counts 10 iterations before stopping
        while ((scoreText == null && multiplierText == null) || count < 10) {
            scoreText = GameObject.FindGameObjectWithTag("ScoreText");
            comboText = GameObject.FindGameObjectWithTag("ComboText");
            multiplierText = GameObject.FindGameObjectWithTag("MultiplierText");
            count++;
        }
    }

    /// <summary>
    /// UpdateScoreText function is only called when the cubes are hit 
    /// it should break the combo streak if the timer is not active,
    /// and if it is, then update combo + multiplier properly
    /// </summary>
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
        if (gameObject.GetComponent<Renderer>().material.color == Color.green)
        {
            updateComboStreak();
            updateMultiplier();
            currentScore = currentScore + (BASE_SCORE * multiplier);
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

    /// <summary>
    /// Updates the current multiplier by checking what the current multiplier has been currently set to
    /// and updates based on the specified conditions in CheckAndUpdateComboMultiplier
    /// </summary>
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

    /// <summary>
    /// Get the current multiplier and check the combo streak for specific conditions.
    /// Then set the current multiplier based on those conditions.
    /// </summary>
    /// <param name="currentMultiplier"></param>
    /// <returns></returns>
    public int CheckAndUpdateComboMultiplier(int currentMultiplier) {
        if (currentMultiplier == MULTIPLIER_LEVEL_THREE)
        {
            if (comboStreak >= COMBO_REQ_LVL_FOUR)
            {
                currentMultiplier = MULTIPLIER_LEVEL_FOUR;
            }
            return currentMultiplier;
        }
        else if (currentMultiplier == MULTIPLIER_LEVEL_TWO)
        {
            if (comboStreak >= COMBO_REQ_LVL_THREE)
            {
                currentMultiplier = MULTIPLIER_LEVEL_THREE;
            }
            return currentMultiplier;
        }
        else if (currentMultiplier == MULTIPLIER_LEVEL_ONE)
        {
            if (comboStreak >= COMBO_REQ_LVL_TWO)
            {
                currentMultiplier = MULTIPLIER_LEVEL_TWO;
            }
            return currentMultiplier;
        }
        else if (currentMultiplier == MULTIPLIER_LEVEL_ZERO) {
            if (comboStreak == COMBO_REQ_LVL_ONE) {
                currentMultiplier = MULTIPLIER_LEVEL_ONE;
            }
            return currentMultiplier;
        }
        else
        {
            return currentMultiplier;
        }
    }

    /// <summary>
    /// Updates the combo streak by adding one every time.
    /// </summary>
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
