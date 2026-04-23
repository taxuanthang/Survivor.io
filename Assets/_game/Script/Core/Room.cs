using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Room : MonoBehaviour
{
    public Tilemap groundTilemap;
    public Tilemap collisionTilemap;
    public Tilemap noSpawnTilemap;

    public BoundsInt cellBounds;
    // convert sang world
    public Bounds worldBounds;


    public RoomType roomType;

    public Door door;

    public bool triggered;

    public List<Transform> spawnsPoints;

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
        switch (this.roomType)
        {
            case RoomType.EnemyRoom:
                EventManager.instance.OnEnterEnemyRoom?.Invoke(this);
                break;
            case RoomType.StartingRoom:
                EventManager.instance.OnEnterEnemyRoom?.Invoke(this);
                break;
        }
    }
}

public enum RoomType
{
    StartingRoom,
    EnemyRoom,
}
