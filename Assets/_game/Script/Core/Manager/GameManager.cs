using NaughtyAttributes;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerManager player;
    [SerializeField] EnemySpawnManager enemySpawnManager;
    [SerializeField] EnemySpawnType enemySpawnType;


    [Header("Game State")]
    [SerializeField] private GameState gameState;


    public void Awake()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerManager>();
        }



    }

    public void Start()
    {
        StartGame();

    }

    public void OnEnable()
    {
        EventManager.instance.OnPlayerDied.AddListener(PauseGame);
        EventManager.instance.RestartGame.AddListener(RestartGame);
    }

    public void OnDisable()
    {
        EventManager.instance.OnPlayerDied.RemoveListener(PauseGame);
        EventManager.instance.RestartGame.RemoveListener(RestartGame);
    }

    [Button("Start Game")]
    private void StartGame()
    {
        enemySpawnManager.SpawnEnemíe(player, 5, enemySpawnType);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        gameState = GameState.Paused;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1f;
        gameState = GameState.Playing;
    }

    public void RestartGame()
    {
        UnPauseGame();
    }
}

public enum GameState
{
    Playing,
    Paused,
    GameOver
}
