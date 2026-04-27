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

public enum UpgradeType
{
    Damage,
    AttackSpeed,
    BulletSize,
    BulletPierce,
    BulletBounce,
    BulletSplit,
    BulletHoming,
    BulletExplosive,
    CritRate,
    CritDamage,

    Health,
    DamageReduces,
    HealthRegene,
    LowHealthShield,

    Speed,
    EXPRange,
    SkillCooldown,
    ItemDroprate,

    BurningAmmo,
    EnemySlowingAmmo,
    PoisonArea,
    ChainLightningAmmo,

    FlyingKnife,
    DamageAura,
    TimeWarp,
    SecondLife,

}
