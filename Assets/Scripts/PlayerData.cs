using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData
{
    public int highScore;

    public PlayerData(int highScore)
    {
        this.highScore = highScore;
    }

    public override string ToString()
    {
        return $"New high score saved: {highScore}";
    }
}
