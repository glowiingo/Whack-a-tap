using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;
using System.Globalization;

public class ScoreInfoManager : MonoBehaviour
{
    private GameObject songTitleObj;
    private GameObject songArtistObj;
    private GameObject currScoreObj;
    private GameObject highScoreObj;

    // Start is called before the first frame update
    void Start()
    {
        songTitleObj = GameObject.FindGameObjectWithTag("ScoreboardSongTitle");
        songArtistObj = GameObject.FindGameObjectWithTag("ScoreboardSongArtist");
        currScoreObj = GameObject.FindGameObjectWithTag("ScoreboardCurrentScore");
        highScoreObj = GameObject.FindGameObjectWithTag("ScoreboardHighScore");
        string title = songTitleObj.GetComponent<UnityEngine.UI.Text>().text;
        string artist = songArtistObj.GetComponent<UnityEngine.UI.Text>().text;
        Song song = new Song(Directory.GetCurrentDirectory(), title, artist);

        string currScore = song.Highscore.ToString(new CultureInfo("en-CA"));

        Debug.Log(currScore);

        // Set the text value in the UI
        highScoreObj.GetComponent<UnityEngine.UI.Text>().text = currScore;
    }
}
