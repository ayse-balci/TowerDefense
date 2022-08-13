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
    
    private EnemySpawner _enemySpawner;
    public Transform end;
    void Start()
    {
        killCountText.text = killedMonsterCount.ToString();
        _enemySpawner = GetComponent<EnemySpawner>();
    }

    public void UpdateKillCount()
    {
        killedMonsterCount++;
        killCountText.text = killedMonsterCount.ToString();
    }

    public void StartGame()
    {
        Debug.Log("start game");
        SceneManager.LoadScene("GameScene");
    }

    public void FinishGame()
    {
        Debug.Log("finish game");
        gameOverText.gameObject.SetActive(true);
    }
}
