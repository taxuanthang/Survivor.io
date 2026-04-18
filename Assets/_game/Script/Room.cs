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
    public Bounds worldBounds ;
    public List<Vector3> spawnsPoints;

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
    }

    public List<Vector3> GetSpawnPoints()
    {
        return spawnsPoints;
    }
}
