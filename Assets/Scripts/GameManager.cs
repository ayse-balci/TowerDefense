using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        gamePanel.SetActive(true);
        _levelManager.SetLevel(1);
        _locateTanks.SetLeftTankCount(1);
        
        killCountText.text = killedMonsterCount.ToString();
        levelText.text = _levelManager.GetLevel().ToString();
        UpdateLeftTankCountText();
        
        SpawnEnemyByLevel();
    }

    public void ContinueGame()
    {
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

    public void RestartGame()
    {
        finishPanel.SetActive(false);
        gamePanel.SetActive(true);
        _levelManager.SetLevel(1);
        _locateTanks.SetLeftTankCount(1);
        killedMonsterCount = 0;
        killCountText.text = killedMonsterCount.ToString();
        levelText.text = _levelManager.GetLevel().ToString();
        totalMonsterInMap = 0;
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
        if (_levelManager.GetLevel() < 5)
        {
            monsterCountEachLevel = _levelManager.GetLevel() * 2 + 1;
            _enemySpawner.SpawnEnemyFromGameManager(monsterCountEachLevel);
        }
        else
        {
            monsterCountEachLevel = _levelManager.GetLevel() + 1;
            _enemySpawner.SpawnEnemyFromGameManager(monsterCountEachLevel);
        }
        
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

    public void FinishGame()
    {
        File.Delete(Application.persistentDataPath + "/towerdefense.game");
        UnityEditor.AssetDatabase.Refresh();
        
        DestroyWithTag("Enemy");
        DestroyWithTag("Tank");
        gamePanel.SetActive(false);
        finishPanel.SetActive(true);
    }

    void DestroyWithTag(String tag)
    {
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
