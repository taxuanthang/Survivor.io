using Game;
using NaughtyAttributes;
using System;
using UnityEngine;
using static UnityEngine.Audio.GeneratorInstance;

public class GameManager : MonoBehaviour
{


    [SerializeField] PlayerManager _player;
    [SerializeField] PlayerInputManager _playerInputManager;
    [SerializeField] EnemySpawnManager _enemySpawnManager;
    [SerializeField] EventManager _eventManager;
    [SerializeField] UIManager _uiManager;
    [SerializeField] LevelManager _levelManager;
    [SerializeField] CameraManager _cameraManager;
    [SerializeField] SoundManager _soundManager;


    [SerializeField] EnemySpawnType enemySpawnType;


    [Header("Game State")]
    [SerializeField] private GameState gameState;


    public void Awake()
    {
        _eventManager.SetUp();
        _uiManager.SetUp();
        _levelManager.SetUp();
        _playerInputManager.SetUp();
        _soundManager.SetUp();

        if (_player != null)
        {
            _player = Instantiate(_player);
        }

        DontDestroyOnLoad(gameObject);

        _playerInputManager.player = _player;
        _enemySpawnManager.player = _player;
        _levelManager.player = _player;
        _cameraManager.player = _player;

        EventManager.instance.StartGame.AddListener(StartGame);
    }

    public void Start()
    {
    }

    public void OnEnable()
    {
        EventManager.instance.OnPlayerDie.AddListener(PauseGame);
        EventManager.instance.RestartGame.AddListener(RestartGame);

        EventManager.instance.PauseGame.AddListener(PauseGame);
        EventManager.instance.UnPauseGame.AddListener(UnPauseGame);
    }

    public void OnDisable()
    {
        EventManager.instance.OnPlayerDie.RemoveListener(PauseGame);
        EventManager.instance.RestartGame.RemoveListener(RestartGame);

        EventManager.instance.PauseGame.RemoveListener(PauseGame);
        EventManager.instance.UnPauseGame.RemoveListener(UnPauseGame);
    }

    [Button("Start Game")]
    private void StartGame()
    {
        //_enemySpawnManager.SpawnEnemies(_player, 1, enemySpawnType);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        gameState = GameState.Paused;
        _playerInputManager.enabled = false;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1f;
        gameState = GameState.Playing;
        _playerInputManager.enabled = true;
    }

    public void RestartGame()
    {
        UnPauseGame();
    }
}

public enum GameState
{
    Menu,
    Playing,
    Paused,
    GameOver
}
