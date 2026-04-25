using System;
using System.Collections.Generic;
using System.Text;

public enum RoomType
{
    StartingRoom,
    EnemyRoom,
}

public enum EnemyType
{
    Normal,
    Range,
}

public enum EnemySpawnType
{
    RandomInPlayerRadius,
    RandomBetweenDeclaredSpawnPos,
    RandomInRoomSize
}
public enum PoolType
{
    Enemy,
    Bullet,
    HitBox,
    BloodVFX,
}
