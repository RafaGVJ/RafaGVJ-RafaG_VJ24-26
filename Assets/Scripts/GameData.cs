using System.IO;
using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class GameData
{
    private const string fileName = "gamedata.json";

    [SerializeField] private Dictionary<string, int> playerData = new Dictionary<string, int>();

    public GameData()
    {
        if (!playerData.ContainsKey("score"))
            playerData["score"] = 0;
    }

    public int GetScore()
    {
        return playerData.ContainsKey("score") ? playerData["score"] : 0;
    }

    public void AddPoints(int points)
    {
        if (!playerData.ContainsKey("score"))
            playerData["score"] = 0;

        playerData["score"] += points;
    }

    public void ResetScore()
    {
        playerData["score"] = 0;
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(new GameDataWrapper(playerData), true);
        File.WriteAllText(GetFilePath(), json);
    }

    public void Load()
    {
        string path = GetFilePath();
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            GameDataWrapper wrapper = JsonUtility.FromJson<GameDataWrapper>(json);
            playerData = wrapper.ToDictionary();
        }
        else
        {
            ResetScore();
        }
    }

    private string GetFilePath()
    {
        return Path.Combine(Application.persistentDataPath, fileName);
    }

    [System.Serializable]
    private class GameDataWrapper
    {
        public int score;

        public GameDataWrapper(Dictionary<string, int> data)
        {
            data.TryGetValue("score", out score);
        }

        public Dictionary<string, int> ToDictionary()
        {
            return new Dictionary<string, int> { { "score", score } };
        }
    }
}

