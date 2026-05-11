using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] string _levelName;
    public StartingRoom _startingRoom;
    public Room _bossReadyRoom;
    public Room _bossRoom;
    [SerializeField] List<EnemyRoom> _enemyRoomList = new List<EnemyRoom>();

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

    public void RegisterRoomTolist(EnemyRoom room)
    {

    }

    public void OnDestroy()
    {
        LevelManager.instance.currentLevel = null;
    }

    public void OnEnterEnemyRoom(EnemyRoom room)
    {

    }

    public void OnFinishEnemyRoom(EnemyRoom room)
    { 
    }

    public bool IsAllEnemyRoomClear()
    {
        return false;
    }
}
