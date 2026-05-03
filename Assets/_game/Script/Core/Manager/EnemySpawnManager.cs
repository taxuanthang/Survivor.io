using NaughtyAttributes;
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


    public void SpawnEnemies(int numberToSpawn, EnemySpawnType spawnType)
    {

        for(int i = 0; i < numberToSpawn; i++) {
            Vector3 spawnPos = new Vector3();
            switch (spawnType)
            {
                case EnemySpawnType.RandomInPlayerRadius:
                    spawnPos = GetPointRandomInPlayerRadius();
                    break;
                case EnemySpawnType.RandomBetweenDeclaredSpawnPos:
                    spawnPos = GetPointRandomBetweenDeclaredSpawnPos();
                    break;
                case EnemySpawnType.RandomInRoomSize:
                    spawnPos = GetPointRandomInRoomSize();
                    break;
            }

            EnemyManager enemy= PoolManager.instance.Get(PoolType.Enemy).GetComponent<EnemyManager>();
            enemy.SetUp(player, spawnPos);

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
        List<Transform> spawnPointsList = currentRoom.GetSpawnPoints();
        if(spawnPointsList.Count == 0)
        {
            return GetPointRandomInPlayerRadius();
        }

        Vector3 spawnPosition = spawnPointsList[Random.Range(0, spawnPointsList.Count)].position;

        return spawnPosition;
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

    [Button("Test Spawn Enemy")]
    public void SpawnEnemies1()
    {
        SpawnEnemies(1, EnemySpawnType.RandomInPlayerRadius);
    }

    public void Start()
    {
        EventManager.instance.SpawnEnemies.AddListener(SpawnEnemies);
        print("dagan");
        EventManager.instance.OnEnterNewRoom.AddListener(OnEnterNewRoom);
    }

    public void OnEnterNewRoom(Room room)
    {
        currentRoom = room;
    }
}

