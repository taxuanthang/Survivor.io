    using System.Collections.Generic;
using UnityEngine;

public class StartingRoom : Room
{
    public List<Transform> spawnPoints;
    public List<Transform> GetPlayerSpawnPoints()
    {
        return spawnPoints;
    }

    public override void OnPlayerCrossDoor(PlayerManager player)
    {
    }
}