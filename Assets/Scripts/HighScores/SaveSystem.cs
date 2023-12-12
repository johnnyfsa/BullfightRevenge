using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;

public static class SaveSystem
{
    [Serializable]
    private class TopScoresWrapper
    {
        public List<PlayerData> topScores;
    }
    private const string saveFileName = "topScores.json";

    public static void SaveTopScores(List<PlayerData> topScores)
    {
        TopScoresWrapper wrapper = new TopScoresWrapper();
        wrapper.topScores = topScores;

        string json = JsonUtility.ToJson(wrapper);
        File.WriteAllText(Application.persistentDataPath + "/" + saveFileName, json);
    }

    public static List<PlayerData> LoadTopScores()
    {
        string filePath = Application.persistentDataPath + "/" + saveFileName;
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            TopScoresWrapper wrapper = JsonUtility.FromJson<TopScoresWrapper>(json);
            return wrapper.topScores;
        }
        else
        {
            return new List<PlayerData>(); // Return an empty list if the file doesn't exist or is empty. 
                                           // This will prevent errors if the file is deleted or the data is corrupted. 
                                           // You can handle this in your game logic if you want. 
                                           // For example, you can display a message or load a default list. 
                                           // You can also handle this in the LoadTopScores() method if you want. 
        }
    }
}