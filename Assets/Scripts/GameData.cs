using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int level;
    public int[] fullLocations;
    public int leftTankCount;
    public GameData(GameState gameState)
    {
        level = gameState.level;
        fullLocations = gameState.fullLocations;
        leftTankCount = gameState.leftTankCount;
    }
}
