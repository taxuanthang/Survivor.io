using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.PlayerSettings;

public class EnemySpawnManager : MonoBehaviour
{
    public Room currentRoom;

    public PlayerManager player;

    public GameObject enemyPrefab;

    public void SpawnEnemíe(PlayerManager player ,int numberToSpawn)
    {
        //Vector3 spawnPos = currentRoom.transform.position + new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);
        //if (currentRoom.IsSpawnable(pos))
        //{
        //    SpawnEnemy();
        //}

        for(int i = 0; i < numberToSpawn; i++) {
            Vector3 spawnPos = GetPointRandomInPlayerRadius();

            EnemyManager enemy= Instantiate(enemyPrefab, spawnPos, Quaternion.identity).GetComponent<EnemyManager>();
            enemy.SetUp(player);

        }
    }

    public Vector3 GetPointRandomInPlayerRadius()
    {
        Vector3 spawnPos;
        do { spawnPos = player.transform.position + new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0); }
        while(!currentRoom.IsSpawnable(spawnPos));

        return spawnPos;

    }

    public Vector3 GetPointRandomBetweenDeclaredSpawnPos()
    {
        List<Vector3> spawnPointsList = currentRoom.GetSpawnPoints();
        if(spawnPointsList.Count == 0)
        {
            return GetPointRandomInPlayerRadius();
        }

        Vector3 spawnPoints = spawnPointsList[Random.Range(0, spawnPointsList.Count)];

        return spawnPoints;
    }

    public Vector3 GetPointRandomInRoomSize()
    {
        Bounds roomBounds = currentRoom.worldBounds;
        Vector3 spawnPos;

        do
        {
            float x = Random.Range(roomBounds.min.x, roomBounds.max.x);
            float y = Random.Range(roomBounds.min.y, roomBounds.max.y);
            spawnPos = new Vector3(x, y, 0);
        }
        while (!currentRoom.IsSpawnable(spawnPos));

        return spawnPos;
    }
}
