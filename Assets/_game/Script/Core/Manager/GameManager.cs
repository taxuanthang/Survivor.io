using NaughtyAttributes;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerManager player;
    [SerializeField] EnemySpawnManager enemySpawnManager;
    [SerializeField] EnemySpawnType enemySpawnType;
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

    [Button("Start Game")]
    private void StartGame()
    {
        enemySpawnManager.SpawnEnemíe(player, 5, enemySpawnType);
    }
}
