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
    EXPOrb,
}

public enum UpgradeType
{
    Damage,
    AttackSpeed,
    BulletPierce,               // mechanic upgrade
    BulletBounce,               // mechanic upgrade
    BulletSplit,                // mechanic upgrade
    BulletHoming,               // mechanic upgrade
    BulletExplosive,            // mechanic upgrade
    CritRate,
    CritDamage,

    Health,
    DamageReduces,
    HealthRegen,
    LowHealthShield,

    Speed,
    EXPRange,
    SkillCooldown,
    ItemDroprate,               

    BurningAmmo,                // mechanic upgrade
    EnemySlowingAmmo,           // mechanic upgrade
    PoisonArea,                 // mechanic upgrade
    ChainLightningAmmo,         // mechanic upgrade

    FlyingKnife,                // mechanic upgrade
    DamageAura,                 // mechanic upgrade
    TimeWarp,                   // mechanic upgrade
    SecondLife,                 // mechanic upgrade

}
