using NUnit.Framework;
using System;
using System.Collections.Generic;

[Serializable]
public class RoomWave
{
    public int waveNumber;
    public List<EnemiesAmount> enemies;
    public bool created;
}

[Serializable]
public class EnemiesAmount
{
    public int numberOfEnemies;
    public EnemyType typeOfEnemies;
    public EnemySpawnType spawnType;
}
