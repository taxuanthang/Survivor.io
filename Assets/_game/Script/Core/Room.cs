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

    public Door door;

    public bool triggered;
    public bool isThisRoomFinished = false;

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
}
