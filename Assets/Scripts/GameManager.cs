using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    private void Awake()
    {
        _enemySpawner = GetComponent<EnemySpawner>();
        _levelManager = FindObjectOfType<LevelManager>();
    }

    void Start()
    {
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
    
    public void StartGame()
    {
        Debug.Log("start game");
        SceneManager.LoadScene("GameScene");
    }

    public void SpawnEnemyByLevel()
    {
        if (_levelManager.GetLevel() < 5)
        {
            monsterCountEachLevel = _levelManager.GetLevel() * 2 + 1;
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
}
