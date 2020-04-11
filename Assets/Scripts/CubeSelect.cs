using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;

/// <summary>
/// 
/// </summary>
public class CubeSelect : MonoBehaviour
{
    public AudioSource audioSource;
    public float delay;
    public GameObject cubeLeft = null;
    public GameObject cubeMid = null;
    public GameObject cubeRight = null;
    int cubeNumber = 0;
    int oldNumber = 0;
    private List<int> rhythmData;
    private string filePath;
    int time;
    int count;
    float lengthMilli;
    private Song song;
    private ButtonSceneManager manager;
    // Start is called before the first frame update

    /// <summary>
    /// Before the first frame update, we create multiple variables and set their values
    /// including the specified cubes, a list of rhythm data to parse through, creating a time and count,
    /// getting the song from the file, and creating the audio source as well as creating a button manager
    /// </summary>
    void Start() {
        setCubes();
        // startTime = Time.time;
        filePath = Directory.GetCurrentDirectory() + "\\Assets\\Scripts\\Music\\Alive - Mind Vortex";
        rhythmData = new List<int>();
        MusicFileHandler.tryRhythmData(filePath, rhythmData);
        time = 0;
        count = 0;
        song = new Song(Directory.GetCurrentDirectory(), "Alive", "Mind Vortex");
        audioSource.PlayDelayed(delay);
        lengthMilli = audioSource.clip.length * 1000;
        manager = new ButtonSceneManager();
    }

    /// <summary>
    /// Update is called once per frame. Updates the song's current playback time.
    /// If the song is still playing, it will continue with the game methods. When the song
    /// is over, prepare to move to the next scene and also overwrite the score if the 
    /// current score is greater than the high score.
    /// </summary>
    void Update() {
        if (count < rhythmData.Count && time < lengthMilli) // song is still playing
        {
            time = Convert.ToInt32(Math.Floor(audioSource.time * 100 + 0.5) * 10);
            if (time >= rhythmData[count])
            {
                pickRandomCube();

                count++;
            }
        } else if(!audioSource.isPlaying) // song is over
        {
            // get user's score
            GameObject gameObject = GameObject.FindGameObjectWithTag("ScoreText");
            string scoreStr = gameObject.GetComponent<TextMeshProUGUI>().text;
            uint userScore;
            if (!UInt32.TryParse(scoreStr, out userScore))
            {
                Debug.LogError("Could not parse GameObject \"ScoreText\"" +
                    " text component to an unsigned integer");
            }

            // get highscore from json file
            Dictionary<string, dynamic> dict = new Dictionary<string, dynamic>();
            MusicFileHandler.tryMusicData(filePath, dict);
            uint highscore = dict["Highscore"];

            // overwrite previous highscore if user beat it
            if (userScore > highscore)
            {
                dict["Highscore"] = userScore;
                File.WriteAllText(filePath + "\\MusicData.json", JsonConvert.SerializeObject(dict));
            }
            CurrentScoreManager.setFinalGameScore();
            manager.ButtonChangeScene("ScoreboardScene");
        }
    }

    /// <summary>
    /// This function will see the cube variables for quick reference of the cube objects.
    /// </summary>
    void setCubes() {
        cubeLeft = GameObject.FindGameObjectWithTag("CubeLeft");
        cubeMid = GameObject.FindGameObjectWithTag("CubeMid");
        cubeRight = GameObject.FindGameObjectWithTag("CubeRight");

        // set the color to a default color
        // this cannot be set by the theme because we are unable
        // to set the 
        cubeLeft.GetComponent<Renderer>().material.color = Color.yellow;
        cubeMid.GetComponent<Renderer>().material.color = Color.yellow;
        cubeRight.GetComponent<Renderer>().material.color = Color.yellow;
    }

    /// <summary>
    /// This function selects a random integer correllating to one of the cubes, 
    /// and changes the color of the specified cube to green for one second, before
    /// changing it to yellow. It should not be allowed to pick the same cube twice in a row.
    /// </summary>
    public void pickRandomCube() {
        // pick a random cube number
        cubeNumber = UnityEngine.Random.Range(1, 4);
        // check if old number is the same as cube number
        // if it is, pick again
        while (oldNumber == cubeNumber) {
            cubeNumber = UnityEngine.Random.Range(1, 4);
        }
        // if it isn't set new old number to cube number
        oldNumber = cubeNumber;

        // select a random cube and change its color
        GameObject randomCube = randomCubeSelector(cubeNumber);
        changeColor(randomCube);
    }

    /// <summary>
    /// This function will return the specified cube gameObject based on the 
    /// cube number that was generated and selected.
    /// </summary>
    /// <param name="cubeNumber">The cube number that was generated between 1 to 3.</param>
    /// <returns>A correlating cube.</returns>
    GameObject randomCubeSelector(int cubeNumber) {
        GameObject selectedCube = null;
        switch (cubeNumber) {
            case 1:
                selectedCube = cubeLeft;
                break;
            case 2:
                selectedCube = cubeMid;
                break;
            case 3:
                selectedCube = cubeRight;
                break;
        }
        return selectedCube;
    
    }

    /// <summary>
    /// Starts the coroutine to change the specified game object to green and back to yellow after one second.
    /// </summary>
    /// <param name="gObject">The specified game object.</param>
    public void changeColor(GameObject gObject) {
        StartCoroutine(colorChangeCoroutine(gObject));
    }

    /// <summary>
    /// The coroutine to change the color of the cube to green,
    /// signaling to the player that they should 'tap' on the cube
    /// to earn more points, and waits one second before changing the cube back to yellow.
    /// </summary>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    IEnumerator colorChangeCoroutine(GameObject gameObject)
    {
        //Print the time of when the function is first called.
        // Debug.Log("Started Coroutine at timestamp : " + Time.time);

        gameObject.GetComponent<Renderer>().material.color = Color.green;
        //yield on a new YieldInstruction that waits for 1 second.
        yield return new WaitForSeconds(1.0f);
        gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        // Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
}
