using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    public void SetUp()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public UnityEvent<float> OnHealthChanged;
    public UnityEvent<float> OnPlayerHit;
    public UnityEvent<float> OnPlayerAmmoChanged;


    public UnityEvent OnPlayerDie;

    public UnityEvent<Room> OnEnterEnemyRoom;
    public UnityEvent<Room> OnEnterNewRoom;
    public UnityEvent OnFinishEnemyRoom;
    public UnityEvent OnEnemyHit;
    public UnityEvent OnEnemyDie;

    public UnityEvent<int,EnemySpawnType> SpawnEnemies;


    [Header("Game State")]
    public UnityEvent PauseGame;
    public UnityEvent UnPauseGame;

    public UnityEvent RestartGame;
    public UnityEvent StartGame;

    public UnityEvent OnEnterNewLevel;
    public UnityEvent OnPlayerReachNewLevel;
    public UnityEvent<Upgrade> OnPlayerCompleteSelectingCard;
}
