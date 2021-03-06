﻿using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System;

public class Song : MonoBehaviour
{
    private string filePath;
    private List<int> rhythmData;
    private Dictionary<string, dynamic> musicData;

    /// <summary>
    /// Conscruct a Song object that holds data regarding the timing of the rhythm,
    /// and properties like name and artist. Loads in the information from a JSON file
    /// for the song that is specified by the name and artist arguments.
    /// </summary>
    /// <param name="baseFilePath"></param>
    /// <param name="name"></param>
    /// <param name="artist"></param>
    public Song(string baseFilePath, string name, string artist)
    {
        filePath = baseFilePath + "\\Assets\\Scripts\\Music\\" + name + " - " + artist;
        rhythmData = new List<int>();
        musicData = new Dictionary<string, dynamic>();
        bool isViable = true;
        // check validity of RhythmData and MusicData, if both are valid then program will proceed.
        if (!MusicFileHandler.tryRhythmData(filePath, rhythmData))
        {
            Debug.Log("Error in RhythmData.csv, please check error log in " + filePath);
            isViable = false;
        }
        if (!MusicFileHandler.tryMusicData(filePath, musicData))
        {
            Debug.Log("Error in MusicData.csv, please check error log in " + filePath);
            isViable = false;
        }
        if (isViable)
        {
            // extract json data if values are viable
            Name = musicData["Name"];
            Artist = musicData["Artist"];
            Genre = musicData["Genre"];
            Length = musicData["Length"];
            NumOfBeats = musicData["Beats"];
            Highscore = musicData["Highscore"];
            // convert formatted string "Length" to milliseconds
            string[] lengthArr = Length.Split(':');
            LengthMilli = Int32.Parse(lengthArr[0]) * 60000 + Int32.Parse(lengthArr[1]) * 1000;
        }
        else
        {

        }
    }
    public string Name { get; }
    public string Artist { get; }
    public string Genre { get; }
    public string Length { get; }
    public int NumOfBeats { get; }
    public uint Highscore { get; private set; }
    // used for audio timing purposes, not part of MusicData
    private int LengthMilli { get; }
    // returns true and overwrites previous highscore in json file if 
    // the new score is higher, otherwise returns false
    public bool trySetHighscore(uint num)
    {
        if (num > Highscore)
        {
            Highscore = num;
            Dictionary<string, dynamic> dict = new Dictionary<string, dynamic>();
            dict.Add("Name", Name);
            dict.Add("Artist", Artist);
            dict.Add("Genre", Genre);
            dict.Add("Length", Length);
            dict.Add("Beats", NumOfBeats);
            dict.Add("Highscore", num);
            File.WriteAllText(filePath + "\\MusicData.json", JsonConvert.SerializeObject(dict));
            return true;
        }
        else
        {
            return false;
        }
    }

    // returns formatted string of song data
    public string toString()
    {
        return "Name: " + Name
            + "\nArtist: " + Artist
            + "\nGenre: " + Genre
            + "\nLength: " + Length
            + "\nNumber Of Beats: " + NumOfBeats
            + "\nHighscore: " + Highscore;

    }
}

// "Enum members shouldn't be used for user interface display purposes. 
// They should be mapped to a string in order to get displayed" -someone on stackoverflow
public enum Genre
{
    Pop,
    HipHop,
    Rap,
    Rock,
    IndieRock,
    PopRock,
    HardRock,
    AlternativeRock,
    Metal,
    HeavyMetal,
    DeathMetal,
    AliveMetal,
    Punk,
    Funk,
    Jas,
    Folk,
    Blues,
    Classical,
    Country,
    Reggae,
    Techno,
    Disco,
    EDM,
    Dubstep,
    Electronic,
    House,
    Instrumental,
    Orchestra,
    Soul,
    RandB
}