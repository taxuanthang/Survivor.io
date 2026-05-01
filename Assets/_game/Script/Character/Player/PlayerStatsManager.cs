using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    [Header("Offensive Stats")]
    public float damage = 10f;
    public float attackSpeed = 1f;      // bullet per second
    public float critRate = 0.1f; // 10% crit chance
    public float critDamage = 2f; // 200% damage on crit

     float baseDamage; // Base damage for calculations, can be used for scaling
     float baseAttackSpeed;
     float baseCritRate;
     float baseCritDamage;

    [Header("Defendsive Stats")]
    public float health = 100f;
    public float damageReduction = 0.1f; // 10% damage reduction
    public float healthRegen = 1f; // 1 health per second
    public float lowHealthShield = 0.2f; // 20% damage reduction when health is below a certain threshold

     float baseHealth;
     float baseHealthRegen;
     float baseDamageReduction;
     float baseLowHealthShield;

    [Header("Utility Stats")]
    public float speed = 10f;
    public float expRange = 10f; // Range for gaining experience
    public float skillCooldownReduction = 0.1f; // 10% cooldown reduction
    public float itemDropRate = 0.1f; // 10% increased item drop rate

    float baseSpeed;
    float baseExpRate;
    float baseSkillCooldownReduction;
    float baseItemDropRate;
    
    PlayerCombatManager playerCombatManager;
    PlayerHealthManager playerHealthManager;
    PlayerLocomotionManager playerLocomotionManager;
    PlayerEquipmentManager playerEquipmentManager;

    public void Awake()
    {
        // Initialize base stats
        baseDamage = damage;
        baseAttackSpeed = attackSpeed;
        baseCritRate = critRate;
        baseCritDamage = critDamage;

        baseHealth = health;
        baseDamageReduction = damageReduction;
        baseHealthRegen = healthRegen;
        baseLowHealthShield = lowHealthShield;

        baseSpeed = speed;
        baseExpRate = expRange;
        baseSkillCooldownReduction = skillCooldownReduction;
        baseItemDropRate = itemDropRate;

        playerCombatManager = GetComponent<PlayerCombatManager>();
        playerHealthManager = GetComponent<PlayerHealthManager>();
        playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
        playerEquipmentManager = GetComponent<PlayerEquipmentManager>();
    }

    public void ApplyModifier(UpgradeType upgradeType, float value)
    {
        switch(upgradeType)
        {
            case UpgradeType.Damage:
                damage += value;
                playerEquipmentManager.UpdateCurrentGunProperties();
                break;
            case UpgradeType.AttackSpeed:
                attackSpeed += baseAttackSpeed * value/100;           // percent
                playerCombatManager.shootCooldown = 1f / attackSpeed; // Update shoot cooldown based on new attack speed
                break;
            case UpgradeType.CritRate:
                critRate += value;
                playerEquipmentManager.UpdateCurrentGunProperties();
                break;
            case UpgradeType.CritDamage:
                critDamage += value;
                playerEquipmentManager.UpdateCurrentGunProperties();
                break;
            case UpgradeType.Health:
                health += value;
                float ratioHealth = playerHealthManager.currentHealth / playerHealthManager.maxHealth; // Calculate current health ratio
                playerHealthManager.maxHealth = (int)health; // Update max health in PlayerHealthManager
                playerHealthManager.currentHealth = (int)(ratioHealth * health); // Ensure current health doesn't exceed new max health
                break;
            case UpgradeType.DamageReduces:
                damageReduction += value;
                playerHealthManager.damageReduction = damageReduction; // Update damage reduction in PlayerHealthManager
                break;
            case UpgradeType.HealthRegen:
                healthRegen += value;
                playerHealthManager.healthRegen = healthRegen; // Update health regen in PlayerHealthManager
                break;

            case UpgradeType.Speed:
                speed += baseSpeed * value / 100;
                playerLocomotionManager.speed = speed;
                break;
            case UpgradeType.EXPRange:
                expRange += value;
                break;
            case UpgradeType.SkillCooldown:
                skillCooldownReduction += value;
                break;
            case UpgradeType.ItemDroprate:
                itemDropRate += value;
                break;
        }
    }

    public void RemoveModifier(UpgradeType upgradeType, float value)
    {
        switch (upgradeType)
        {
            case UpgradeType.Damage:
                damage -= value;
                break;
            case UpgradeType.AttackSpeed:
                attackSpeed -= baseAttackSpeed * value / 100;           // percent
                playerCombatManager.shootCooldown = 1f / attackSpeed; // Update shoot cooldown based on new attack speed
                break;
            case UpgradeType.CritRate:
                critRate -= value;
                break;
            case UpgradeType.CritDamage:
                critDamage -= value;
                break;
            case UpgradeType.Health:
                health -= value;
                float ratioHealth = playerHealthManager.currentHealth / playerHealthManager.maxHealth; // Calculate current health ratio
                playerHealthManager.maxHealth = (int)health; // Update max health in PlayerHealthManager
                playerHealthManager.currentHealth = (int)(ratioHealth * health); // Ensure current health doesn't exceed new max health
                break;
            case UpgradeType.DamageReduces:
                damageReduction -= value;
                playerHealthManager.damageReduction = damageReduction; // Update damage reduction in PlayerHealthManager
                break;
            case UpgradeType.HealthRegen:
                healthRegen -= value;
                playerHealthManager.healthRegen = healthRegen; // Update health regen in PlayerHealthManager
                break;

            case UpgradeType.Speed:
                speed -= baseSpeed * value / 100;
                playerLocomotionManager.speed = speed;
                break;
            case UpgradeType.EXPRange:
                expRange -= value;
                break;
            case UpgradeType.SkillCooldown:
                skillCooldownReduction -= value;
                break;
            case UpgradeType.ItemDroprate:
                itemDropRate -= value;
                break;
        }
    }
}

