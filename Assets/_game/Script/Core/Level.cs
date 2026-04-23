using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] string _levelName;
    public Room _startingRoom;
    [SerializeField] List<Room> _roomList = new List<Room>();

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
        if(room.triggered == false)
        {
            room.triggered = true;
            print("1");
            room.CloseAllDoor();
            print("chuanbiinvoke");
            EventManager.instance.SpawnEnemies?.Invoke(5, EnemySpawnType.RandomInRoomSize);
            print("dainvoke");
        }

    }

    public void OnFinishEnemyRoom(Room room)
    { 
        room.OpenAllDoor();
    }
}
