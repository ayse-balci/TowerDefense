using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public LevelManager levelManager;
    public LocateTanks locateTanks;
    public int level;
    public int[] fullLocations;
    public int leftTankCount;
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        locateTanks = FindObjectOfType<LocateTanks>();
    }

    public void SaveGameState()
    {
        level = levelManager.GetLevel();
        leftTankCount = locateTanks.GetLeftTankCount();
        fullLocations = new int[10];
        int count = 0;
        for (int i = 0; i < locateTanks.tankLocations.Count; i++)
        {
            if (locateTanks.tankLocations[i].GetIsFull())
            {
                Debug.Log(i);
                fullLocations[count] = i;
                count++;
            }
        }
        
        SaveSystem.SaveGameState(this);
    }

    public void LoadGameState()
    {
        GameData data = SaveSystem.LoadGameState();

        this.level = data.level;
        this.fullLocations = data.fullLocations;
        this.leftTankCount = data.leftTankCount;
    }
}