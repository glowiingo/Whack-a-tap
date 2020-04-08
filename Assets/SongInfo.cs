using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SongInfo : MonoBehaviour
{
    public TextMeshPro title;
    public TextMeshPro genre;
    public TextMeshPro artist;
    public TextMeshPro length;
    public TextMeshPro highScore;
    private Dictionary<string, dynamic> musicData;
    private string baseFilePath;
    private string filePath;
    // Start is called before the first frame update
    void Start()
    {
        musicData = new Dictionary<string, dynamic>();
        baseFilePath = Directory.GetCurrentDirectory();
        filePath = baseFilePath + "\\Assets\\" + "\\Scripts\\" + "\\Music\\Alive - Mind Vortex\\";
             
        if (MusicFileHandler.tryMusicData(filePath, musicData))
        {
            // set json data to song card if possible
            title.SetText(musicData["Name"]);
            artist.SetText(musicData["Artist"]);
            genre.SetText(musicData["Genre"]);
            length.SetText(musicData["Length"]);
            highScore.SetText(musicData["Highscore"].ToString());
        }
        else
        {
            Debug.Log("Error in MusicData.json, please check error log in " + filePath);
            Environment.Exit(-1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
