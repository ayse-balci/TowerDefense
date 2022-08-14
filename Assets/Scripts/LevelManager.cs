using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int currentLevel = 1;

    public void UpdateLevel()
    {
        currentLevel++;
    }

    public int GetLevel()
    {
        return currentLevel;
    }
}
