using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System;

// checks validity of rhythm and/or music data, filepath should lead to the
// music folder containing them.
public static class MusicFileHandler
{
    // checks if all the data in the rhythm data is valid
    public static bool tryRhythmData(string filePath, List<int> list)
    {
        List<string> errList = new List<string>();
        using (StreamReader data = new StreamReader(filePath + "\\RhythmData.csv"))
        {
            string line;
            int num;
            for (int i = 1; (line = data.ReadLine()) != null; i++)
            {
                // In excel, having values in multiple columns will cause all the single-column values
                // to have an additional comma placed aftet it. TrimEnd will get rid of these additional
                // commas so the code can proceed to find the values with multiple columns.
                line = line.TrimEnd(',');
                if (line.Contains(","))
                {
                    errList.Add("Line " + i + "(" + line + ")");
                }
                else if (!Int32.TryParse(line, out num) || (num + 0.0) % 10 != 0 || num < 0)
                {
                    errList.Add("Line " + i + "(" + line + ")");
                }
                else
                {
                    list.Add(num);
                }
            }
            data.Close();
        }
        if (errList.Count == 0)
        {
            return true;
        }
        else
        {
            File.WriteAllLines(filePath + "\\RhythmDataErrorLog.txt", errList);
            return false;
        }
    }

    // checks if all the data in the music data is valid
    public static bool tryMusicData(string filePath, Dictionary<string, dynamic> newDict)
    {
        using (StreamReader data = new StreamReader(filePath + "\\MusicData.json"))
        {
            string json = data.ReadToEnd();
            var dataDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            List<string> errList = new List<string>();

            // check if Name and Artist keys exists
            if (!dataDict.ContainsKey("Name"))
            {
                errList.Add("MusicData does not contain key/value for Name");
            }
            if (!dataDict.ContainsKey("Artist"))
            {
                errList.Add("MusicData does not contain a key/value for Artist");
            }

            // check genre (first letter is uppercase and rest is lowercase)
            if (!dataDict.ContainsKey("Genre") || dataDict["Genre"].Length <= 2 ||
                !Enum.IsDefined(typeof(Genre), dataDict["Genre"].Substring(0, 1).ToUpper() + dataDict["Genre"].Substring(1).ToLower()))
            {
                errList.Add("MusicData either doesn't exists or is not valid (please select from our pre-determined list of genres)");
            }

            // check song length 
            string[] lengthStr;
            if (dataDict.ContainsKey("Length"))
            {
                lengthStr = dataDict["Length"].Split(':');
                if (lengthStr.Length != 2 || !Int32.TryParse(lengthStr[0], out int num1) || !Int32.TryParse(lengthStr[1], out int num2))
                {
                    errList.Add("Invalid song length, it should be \"min:sec\" where both min and sec are non-negative 32-bit integers");
                }
            }
            else
            {
                errList.Add("MusicData doesn't contian a song length key/value");
            }

            // check song beats
            int tempInt = 0;
            if (!dataDict.ContainsKey("Beats") || !Int32.TryParse(dataDict["Beats"], out tempInt))
            {
                errList.Add("Song beats either doesn't exists or is invalid (it should be a postitive 32-bit integer)");
            }

            // check song highscore
            uint tempUInt = 0;
            if (!dataDict.ContainsKey("Highscore") || !UInt32.TryParse(dataDict["Highscore"], out tempUInt))
            {
                errList.Add("Song highscore either doesn't exists or is invalid (it should be a non-negative 32-bit integer)");
            }

            // check if any errors accumulated
            if (errList.Count == 0)
            {
                newDict["Name"] = dataDict["Name"];
                newDict["Artist"] = dataDict["Artist"];
                newDict["Genre"] = dataDict["Genre"];
                newDict["Length"] = dataDict["Length"];
                newDict["Beats"] = tempInt;
                newDict["Highscore"] = tempUInt;
                return true;
            }
            else
            {
                File.WriteAllLines(filePath + "\\MusicDataErrorLog.txt", errList);
                return false;
            }
        }
    }
}
