using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    [Header("Room TileMap")]
    public Tilemap groundTilemap;
    public Tilemap collisionTilemap;
    public Tilemap noSpawnTilemap;

    [Header("Room Bound")]
    public BoundsInt cellBounds;
    // convert sang world
    public Bounds worldBounds;
    [Header("Room Enemy Wave")]
    
    public List<RoomWave> waves;

    [SerializeField] int waveCount =0;
    [SerializeField] int currentEnemiesExisting = 0;

    [Header("Other")]
    public RoomType roomType;

    public Door door;

    public bool triggered;
    public bool isThisRoomFinished = false;

    public List<Transform> spawnsPoints;

    public  void Update()
    {
        if(!triggered)
        {
            return;
        }
        if (isThisRoomFinished)
        {
            return;
        }

        if (currentEnemiesExisting != 0)
        {
            return;
        }
        waveCount++;
        if (waveCount > waves.Count)
        {
            OnFinishRoom();
            waveCount = 0;
            return;
        }


        int waveIndex = waveCount - 1;
        for (int i = 0; i < waves[waveIndex].enemies.Count; i++)
        {
            EnemyType selectedType = waves[waveIndex].enemies[i].typeOfEnemies;
            int currentEnemiesOfThisSelectedType = waves[waveIndex].enemies[i].numberOfEnemies;
            EnemySpawnType selectedSpawnType = waves[waveIndex].enemies[i].spawnType;

            currentEnemiesExisting += currentEnemiesOfThisSelectedType;
            switch (selectedType)
            {
                case EnemyType.Normal:
                    EventManager.instance.SpawnEnemies?.Invoke(currentEnemiesOfThisSelectedType, selectedSpawnType);
                    break;
                case EnemyType.Range:
                    EventManager.instance.SpawnEnemies?.Invoke(currentEnemiesOfThisSelectedType, selectedSpawnType);
                    break;
            }
        }
        waves[waveIndex].created = true;





    }

    public void IsThisWaveFinish()
    {

    }
    public bool IsSpawnable(Vector3 pos)
    {
        Vector3Int cell = groundTilemap.WorldToCell(pos);

        if (!groundTilemap.HasTile(cell)) return false;
        if (collisionTilemap.HasTile(cell)) return false;
        if (noSpawnTilemap && noSpawnTilemap.HasTile(cell)) return false;

        return true;
    }

    public void Start()
    {
        cellBounds = groundTilemap.cellBounds;

        Vector3 min = groundTilemap.CellToWorld(cellBounds.min);
        Vector3 max = groundTilemap.CellToWorld(cellBounds.max) + groundTilemap.cellSize;

        worldBounds = new Bounds();
        worldBounds.SetMinMax(min, max);


        door.room = this;
    }

    public List<Transform> GetSpawnPoints()
    {
        return spawnsPoints;
    }

    internal void CloseAllDoor()
    {
        door.Close();

    }

    internal void OpenAllDoor()
    {
        door.Open();
    }

    public void OnPlayerEnter()
    {
        EventManager.instance.OnEnterNewRoom?.Invoke(this);
        EventManager.instance.OnEnemyDie.AddListener(OnEnemyDie);
        switch (this.roomType)
        {
            case RoomType.EnemyRoom:
                EventManager.instance.OnEnterEnemyRoom?.Invoke(this);
                if (triggered == false)
                {
                    triggered = true;
                    CloseAllDoor();
                }
                break;
            case RoomType.StartingRoom:
                EventManager.instance.OnEnterEnemyRoom?.Invoke(this);
                break;
        }
    }

    public void OnFinishRoom()
    {
        isThisRoomFinished = true;
        OpenAllDoor();
        EventManager.instance.OnEnemyDie.RemoveListener(OnEnemyDie);
    }

    public void OnEnemyDie()
    {
        currentEnemiesExisting -= 1;
    }
}
