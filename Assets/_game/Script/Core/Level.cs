using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] string _levelName;
    public Room _startingRoom;
    public Room _bossReadyRoom;
    public Room _bossRoom;
    [SerializeField] List<Room> _normalRoomList = new List<Room>();

    public void Awake()
    {
        LevelManager.instance.currentLevel = this;
    }

    public void Start()
    {
        EventManager.instance.OnEnterEnemyRoom.AddListener(OnEnterEnemyRoom);
    }
    public void ClearCurrentRoomList()
    {

    }

    public void RegisterRoomTolist(Room room)
    {

    }

    public void OnDestroy()
    {
        LevelManager.instance.currentLevel = null;
    }

    public void OnEnterEnemyRoom(Room room)
    {

    }

    public void OnFinishEnemyRoom(Room room)
    { 
    }
}
