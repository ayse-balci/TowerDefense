using System;
using System.IO;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int killedMonsterCount = 0;
    public TextMeshProUGUI killCountText;
    public GameObject finishPanel;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI leftTankCount;
    public GameObject gamePanel;
    
    private EnemySpawner _enemySpawner;
    public Transform end;

    private LevelManager _levelManager;
    public int monsterCountEachLevel = 1; 
    public int totalMonsterInMap = 0;

    private GameState _gameState;
    public LocateTanks _locateTanks;
    
    private void Awake()
    {
        _enemySpawner = GetComponent<EnemySpawner>();
        _levelManager = FindObjectOfType<LevelManager>();
        _gameState = FindObjectOfType<GameState>();
        _locateTanks = FindObjectOfType<LocateTanks>();
    }
    
    public void StartGame()
    {
        // Called from start button on lobby panel
        finishPanel.SetActive(false);
        gamePanel.SetActive(true);
        _levelManager.SetLevel(1);
        _locateTanks.SetLeftTankCount(1);
        killedMonsterCount = 0;
        totalMonsterInMap = 0;
        killCountText.text = killedMonsterCount.ToString();
        levelText.text = _levelManager.GetLevel().ToString();
        UpdateLeftTankCountText();
        
        SpawnEnemyByLevel();
    }

    public void ContinueGame()
    {
        // Called from continue button on lobby panel 
        // Continue button shown if there is saved state. when click the button, get saved data by LoadGameState() function
        
        _gameState.LoadGameState();
        _levelManager.SetLevel(_gameState.level);
        _locateTanks.CreateTanksAtContinue(_gameState.fullLocations);
        _locateTanks.SetLeftTankCount(_gameState.leftTankCount);
        killedMonsterCount = _gameState.killedMonsterCount;
        
        gamePanel.SetActive(true);
        killCountText.text = killedMonsterCount.ToString();
        levelText.text = _levelManager.GetLevel().ToString();
        UpdateLeftTankCountText();
        SpawnEnemyByLevel();
    }


    public void UpdateKillCount()
    {
        killedMonsterCount++;
        killCountText.text = killedMonsterCount.ToString();
    }
    
    public void UpdateLevelText()
    {
        levelText.text = _levelManager.GetLevel().ToString();
    }
    
    public void SpawnEnemyByLevel()
    {
        // SpawnEnemyByLevel call EnemyScript object with enemy count according to level ( enemy count = level * 2 + 1)
         monsterCountEachLevel = _levelManager.GetLevel() * 2 + 1;
         _enemySpawner.SpawnEnemyFromGameManager(monsterCountEachLevel);
 
        if (_levelManager.GetLevel() % 3 == 0)
        {
            _locateTanks.IncreaseLeftTankCount();
        }
        totalMonsterInMap += monsterCountEachLevel;
    }

    public void DecreaseMonsterCountInMap()
    {
        totalMonsterInMap--;
        if (totalMonsterInMap == 0)
        {
            GoNextLevel();
        }
    }

    public int GetTotalMonsterInMap()
    {
        return totalMonsterInMap;
    }

    public int GetKilledMonsterCount()
    {
        return killedMonsterCount;
    }

    public void GoNextLevel()
    {
        _levelManager.UpdateLevel();
        UpdateLevelText();
        SpawnEnemyByLevel();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void FinishGame()
    {
        File.Delete(Application.persistentDataPath + "/towerdefense.game");

        DestroyWithTag("Tank");
        DestroyWithTag("Enemy");
        
        gamePanel.SetActive(false);
        finishPanel.SetActive(true);
    }

    void DestroyWithTag(String tag)
    {
        // Finish all object that have tag 
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);   
        foreach (GameObject obj in taggedObjects) {
            Destroy(obj);
        }
    }

    public void QuitGame()
    {
        Debug.Log("quit game");
        _gameState.SaveGameState();
        Application.Quit();
    }

    public  void UpdateLeftTankCountText()
    {
        leftTankCount.text = _locateTanks.GetLeftTankCount().ToString();
    }
}
