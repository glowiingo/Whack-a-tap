using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.IO;

public class MusicPlayer : MonoBehaviour
{
    //public GameObject audioSourceObj;
    public AudioSource audioSource;
    public float delay;
    private GameObject songCardObj;
    private List<int> rhythmData;
    private string filePath;
    int time;
    int count; 
    float lengthMilli;
    // Start is called before the first frame update
    void Start()
    {
        // songCardObj = GameObject.FindGameObjectWithTag("SongAliveByMindVortex");
        filePath = Directory.GetCurrentDirectory() + "\\Assets\\Scripts\\Music\\Alive - Mind Vortex";
        rhythmData = new List<int>();
        MusicFileHandler.tryRhythmData(filePath, rhythmData);
        time = 0;
        count = 0;
        
        // string[] separator = { "-" };
        // string[] songInfo = songCardObj.name.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        Song song = new Song(Directory.GetCurrentDirectory(), "Alive", "Mind Vortex");
        audioSource.PlayDelayed(delay);
        lengthMilli = audioSource.clip.length * 1000;
        //song.play(audioSource, delay);
    }

    private void Update()
    {
        if (time < lengthMilli && count < rhythmData.Count)
        {
            time = Convert.ToInt32(Math.Floor(audioSource.time * 100 + 0.5) * 10);
            if (time >= rhythmData[count])
            {
                Debug.Log("boop " + count);
                count++;
            }
        }
    }
}
