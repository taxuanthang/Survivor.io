using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerManager player;
    [SerializeField] EnemySpawnManager enemySpawnManager;

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

    private void StartGame()
    {
        enemySpawnManager.SpawnEnemíe(player, 5);
    }
}
