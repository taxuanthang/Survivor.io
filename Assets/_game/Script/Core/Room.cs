using NaughtyAttributes;
using System.Collections.Generic;
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


    [Header("Other")]
    public RoomType roomType;

    public List<Door> doorList;

    public bool triggered;
    public bool isThisRoomFinished = false;
    public RoomData data;

    public virtual void Awake()
    {
        foreach (var door in doorList)
        {
            if (door.room == null) door.room = this;
        }
    }

    public virtual void Update()
    {
        if (!triggered)
        {
            return;
        }
        if (isThisRoomFinished)
        {
            return;
        }
    }

    public virtual void OnPlayerCrossDoor(PlayerManager player)
    {
        if (triggered == false)
        {
            triggered = true;
        }
        else
        {

        }
    }

    [Button]
    internal void CloseAllDoor()
    {
        foreach (var door in doorList)
        {
            door.Close();
        }
    }

    public void CloseDoor(Vector2 doorCoordinate)
    {
        if (doorList.Count == 0) return;
        if(doorCoordinate == new Vector2(0f,1f))
        {
            doorList[0].Close();
        }
        else if (doorCoordinate == new Vector2(-1f, 0f))
        {
            doorList[1].Close();
        }
        else if (doorCoordinate == new Vector2(0f, -1f))
        {

            doorList[2].Close();
        }
        else if (doorCoordinate == new Vector2(1f, 0f))
        {

            doorList[3].Close();
        }
    }

    public void OpenDoor(Vector2 doorCoordinate)
    {
        if (doorList.Count == 0) return;
        if (doorCoordinate == new Vector2(0f, 1f))
        {
            doorList[0].Open();
        }
        else if (doorCoordinate == new Vector2(-1f, 0f))
        {
            doorList[1].Open();
        }
        else if (doorCoordinate == new Vector2(0f, -1f))
        {
            doorList[2].Open();
        }
        else if (doorCoordinate == new Vector2(1f, 0f))
        {
            doorList[3].Open();
        }
    }



}
