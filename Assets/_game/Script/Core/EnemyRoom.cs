using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyRoom : Room
{
    [Header("Room Enemy Wave")]
    public List<RoomWave> waves;

    [SerializeField] int waveCount =0;
    [SerializeField] int currentEnemiesExisting = 0;

    public List<Transform> spawnsPoints;

    public override void Update()
    {
        base.Update();

        SpawnWave();


    }

    public void SpawnWave()
    {
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
                default:
                    EventManager.instance.SpawnEnemies?.Invoke(selectedType, currentEnemiesOfThisSelectedType, selectedSpawnType);
                    break;
                case EnemyType.Boss1:
                    // tìm đến boss rồi kích hoạt nó

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

    public void OnPlayerEnter(PlayerManager player)
    {
        EventManager.instance.OnEnterNewRoom?.Invoke(this);
        EventManager.instance.OnEnemyDie.AddListener(OnEnemyDie);
        EventManager.instance.OnEnterEnemyRoom?.Invoke(this);
        switch (this.roomType)
        {
            case RoomType.EnemyRoom:
                CloseAllDoor();
                break;
            case RoomType.StartingRoom:
                break;
        }
    }

    public override void OnPlayerCrossDoor(PlayerManager player)
    {
        if (triggered == false)
        {
            triggered = true;
            OnPlayerEnter(player);
        }
        else
        {

        }
    }

    public void OnFinishRoom()
    {
        isThisRoomFinished = true;
        OpenAllDoor();
        EventManager.instance.OnEnemyDie.RemoveListener(OnEnemyDie);
        EventManager.instance.OnFinishEnemyRoom?.Invoke();
    }

    public void OnEnemyDie()
    {
        currentEnemiesExisting -= 1;
    }
}
