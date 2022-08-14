using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int killedMonsterCount = 0;
    public TextMeshProUGUI killCountText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI levelText;
    
    private EnemySpawner _enemySpawner;
    public Transform end;

    private LevelManager _levelManager;
    public int monsterCountEachLevel = 1; 
    public int totalMonsterInMap = 0;

    private GameState _gameState;

    public GameObject gamePanel;
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
        killCountText.text = killedMonsterCount.ToString();
        levelText.text = _levelManager.GetLevel().ToString();
        _levelManager.SetLevel(1);
        SpawnEnemyByLevel();
    }

    public void ContinueGame()
    {
        _gameState.LoadGameState();

        _locateTanks.CreateTanksAtContinue(_gameState.fullLocations);
        _levelManager.SetLevel(_gameState.level);
        gamePanel.SetActive(true);
        killCountText.text = killedMonsterCount.ToString();
        levelText.text = _levelManager.GetLevel().ToString();
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

    public void GoNextLevel()
    {
        _levelManager.UpdateLevel();
        UpdateLevelText();
        SpawnEnemyByLevel();
    }

    public void FinishGame()
    {
        Debug.Log("finish game");
        gameOverText.gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("quit game");
        _gameState.SaveGameState();
        Application.Quit();
    }
}
