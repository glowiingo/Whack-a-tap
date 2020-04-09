using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;

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

    bool called = false;
    // Start is called before the first frame update
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
    /// is over, prepare to move to the next scene.
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
        GameObject randomCube = randomCubeSelector(cubeNumber);
        // changeY(randomCube);
        changeColor(randomCube);
    }

    /***
     * Given a random number between 1 and 3 return the correlating cube.
     * param: cubeNumber, a random number between 1 and 3.
     */
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

    public void changeColor(GameObject gObject) {
        StartCoroutine(colorChangeCoroutine(gObject));
    }

    IEnumerator colorChangeCoroutine(GameObject gameObject)
    {
        //Print the time of when the function is first called.
        // Debug.Log("Started Coroutine at timestamp : " + Time.time);

        gameObject.GetComponent<Renderer>().material.color = Color.green;
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(1.0f);

        gameObject.GetComponent<Renderer>().material.color = Color.yellow;
        //After we have waited 5 seconds print the time again.
        // Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
}
