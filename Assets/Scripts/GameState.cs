using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public LevelManager levelManager;
    public LocateTanks locateTanks;
    private GameManager _gameManager;
    
    public int level;
    public int[] fullLocations;
    public int leftTankCount;
    public int killedMonsterCount;
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        locateTanks = FindObjectOfType<LocateTanks>();
        _gameManager = FindObjectOfType<GameManager>();
    }

    public void SaveGameState()
    {
        level = levelManager.GetLevel();
        leftTankCount = locateTanks.GetLeftTankCount();
        killedMonsterCount = _gameManager.GetKilledMonsterCount();
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

        for (int i = count; i < fullLocations.Length; i++)
        {
            fullLocations[i] = 99;   // If the tanklocation is less then 10, fill list with invalid values
        }
        
        SaveSystem.SaveGameState(this);
    }

    public void LoadGameState()
    {
        GameData data = SaveSystem.LoadGameState();

        this.level = data.level;
        this.fullLocations = data.fullLocations;
        this.leftTankCount = data.leftTankCount;
        this.killedMonsterCount = data.killedMonsterCount;
    }
}