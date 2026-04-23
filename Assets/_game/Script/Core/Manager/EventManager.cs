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


    public Action<float> OnHealthChanged;
    public Action<float> OnPlayerAmmoChanged;

    public UnityEvent<float> onHealth;

    public UnityEvent OnPlayerDied;

    public UnityEvent<Room> OnEnterEnemyRoom;
    public UnityEvent<Room> OnEnterNewRoom;
    public UnityEvent OnFinishEnemyRoom;
    public UnityEvent OnEnemyDie;

    public UnityEvent<int,EnemySpawnType> SpawnEnemies;


    [Header("Game State")]
    public UnityEvent<float> OnGamePause;
    public UnityEvent<float> OnGameUnPause;

    public UnityEvent RestartGame;
    public UnityEvent StartGame;

    public UnityEvent OnEnterNewLevel;


}
