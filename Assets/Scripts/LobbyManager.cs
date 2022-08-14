using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class LobbyManager : MonoBehaviour
{
    public Button continueButton;
    public GameObject lobbyPanel; 
    private GameState _gameState;
    private GameManager _gameManager;
    void Awake()
    {
        _gameState = FindObjectOfType<GameState>();
        _gameManager = FindObjectOfType<GameManager>();
        
        if (File.Exists(Application.persistentDataPath + "/towerdefense.game"))
        {
            continueButton.gameObject.SetActive(true);
        }
    }
    public void StartGame()
    {
        _gameManager.StartGame();
        lobbyPanel.SetActive(false);
    }

    public void ContinueGame()
    {
        _gameState.LoadGameState();
        _gameManager.ContinueGame();
        lobbyPanel.SetActive(false);
    }
}
