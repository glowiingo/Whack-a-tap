using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;

namespace Wack_A_Tap
{
    public class Song
    {
        private String filePath;
        private MediaPlayer mediaPlayer;
        private List<int> rhythmData;
        private Dictionary<String, dynamic> musicData;
        public Song(String baseFilePath, String name, String artist)
        {
            filePath = baseFilePath + "\\Music\\" + name + " - " + artist;
            mediaPlayer = new MediaPlayer();
            rhythmData = new List<int>();
            musicData = new Dictionary<String, dynamic>();
            bool isViable = true;
            // check validity of RhythmData and MusicData, if both are valid then program will proceed.
            if (!MusicFileHandler.tryRhythmData(filePath, rhythmData))
            {
                Console.WriteLine("Error in RhythmData.csv, please check error log in " + filePath);
                isViable = false;
            }
            if(!MusicFileHandler.tryMusicData(filePath, musicData))
            {
                Console.WriteLine("Error in MusicData.csv, please check error log in " + filePath);
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
                String[] lengthArr = Length.Split(':');
                LengthMilli = Int32.Parse(lengthArr[0]) * 60000 + Int32.Parse(lengthArr[1]) * 1000;
            } else
            {
                Environment.Exit(-1);
            }
        }
        public String Name { get; }
        public String Artist { get; }
        public String Genre { get; }
        public String Length { get; }
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
                Dictionary<String, dynamic> dict = new Dictionary<String, dynamic>();
                dict.Add("Name", Name);
                dict.Add("Artist", Artist);
                dict.Add("Genre", Genre);
                dict.Add("Length", Length);
                dict.Add("Beats", NumOfBeats);
                dict.Add("Highscore", num);
                File.WriteAllText(filePath + "\\MusicData.json", JsonConvert.SerializeObject(dict));
                return true;
            } else
            {
                return false;
            }
        }

        public void play()
        {
            Uri uri = new Uri(filePath + "\\Audio.mp3");
            mediaPlayer.Open(uri);
            mediaPlayer.Play();
            int time = 0;
            int count = 0;
            while (time < LengthMilli && count < rhythmData.Count)
            {
                time = Convert.ToInt32(Math.Floor(mediaPlayer.Position.TotalSeconds * 100 + 0.5) * 10);
                
                if (rhythmData[count] == time)
                {
                   
                    Console.WriteLine("boop " + count);
                    count++;
                }
            }
            Console.WriteLine("end of beats");
        }

        public void stop()
        {
            mediaPlayer.Stop();
        }

        // returns formatted String of song data
        public String toString()
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
}


