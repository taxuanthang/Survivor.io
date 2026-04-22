using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public PlayerManager player;
    public Level currentLevel;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        this.enabled = false;
    }

    public void SetUp()
    {
        this.enabled = true;
    }

    public void OnEnable()
    {
        if (EventManager.instance == null)
        {
            return;
        }
        EventManager.instance.OnEnterNewLevel.AddListener(OnEnterNewLevel);
    }

    public void OnDisable()
    {
        if(EventManager.instance == null)
        {
            return;
        }
        EventManager.instance.OnEnterNewLevel.RemoveListener(OnEnterNewLevel);
    }
    public void OnEnterNewLevel()
    {
        List<Transform> spawnPoints = currentLevel._startingRoom.GetSpawnPoints();
        player.transform.position = spawnPoints[0].position;
    }
}
