using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public static class CSVReader
{
   public static Dictionary<string,string> ReadFile(string filename, int language)
   {
        TextAsset textAsset = Resources.Load<TextAsset>(filename);
        string[] fLines = Regex.Split(textAsset.text, Environment.NewLine);

        var lines = new Dictionary<string, string>();

        for (int i = 0; i < fLines.Length; i++)
        {
            string[] currentLine = fLines[i].Split(',');
            lines.Add(currentLine[0], currentLine[language]);
        }

        return lines;
   }
}