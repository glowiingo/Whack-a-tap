using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class SongCardManager : MonoBehaviour
{
    /// <summary>
    /// A list of GameObjects that represent a SongCard in Unity.
    /// These are the object whose text fields must be populated.
    /// 
    /// NOTE: All of the SongCards in the Unity Scene must be dragged into the SongCardManager's list.
    /// </summary>
    public List<GameObject> songCardList;

    // Start is called before the first frame update
    void Start()
    {
        string[] separator = { "-" };
        foreach (GameObject songCard in songCardList)
        {
            string[] songInfo = songCard.name.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            Song song = new Song(Directory.GetCurrentDirectory(), songInfo[0], songInfo[1]);

            string title = song.Name;
            string artist = song.Artist;
            string genre = song.Genre;
            string length = song.Length;

            // Get the child object of the song card, which contains the text fields to populate:
            GameObject songInfoChildObj = songCard.transform.FindChild("SongInfo").gameObject;

        }
    }
}
