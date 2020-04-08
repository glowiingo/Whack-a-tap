using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;

namespace Wack_A_Tap
{
    class Program
    {
        static void Main(string[] args)
        {
            String songName = "Alive";
            String songArtist = "Mind Vortex";
            Song song = new Song(Directory.GetCurrentDirectory(), songName, songArtist);
            Console.WriteLine(song.toString());
            song.play();

            Console.ReadLine();
        }
    }
}

