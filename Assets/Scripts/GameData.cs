using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData 
{
    private Dictionary<string, int> playerData = new Dictionary<string, int>();

    public GameData()
    {
        // Inicializa��o de dados, como pontua��o inicial
        playerData["score"] = 0;
    }

    public int GetScore()
    {
        return playerData["score"];
    }

    public void AddPoints(int points)
    {
        playerData["score"] += points;
    }

    public void ResetScore()
    {
        playerData["score"] = 0;
    }
}

