using System;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class PlayerHealthManager : CharacterHealthManager
{
    public float damageReduction = 0f; // 10% damage reduction
    public float healthRegen = 0f;

    float currentHealCooldown = 0f;
    public void Update()
    {

        HealOverTime();
    }

    private void HealOverTime()
    {
        if (currentHealCooldown > 0)
        {
            currentHealCooldown -= Time.deltaTime;
            return;
        }
        else
        {
            currentHealCooldown = 1f;
        }
        Heal((int)healthRegen);
    }

    public override void TakeDamage(int damage)
    {
        currentHealth -= (int)(damage * (1- damageReduction));
        if (currentHealth <= 0)
        {
            Die();
            character.OnDie?.Invoke();
        }
        float percentageHealth = (float)currentHealth / (float)maxHealth;
        EventManager.instance.OnHealthChanged?.Invoke(percentageHealth);
        EventManager.instance.OnPlayerHit?.Invoke(percentageHealth);
    }

    public override void Die()
    {
        base.Die();
        EventManager.instance.OnPlayerDie?.Invoke();
    }

    public void Heal(int amount)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += amount;
            if (currentHealth > maxHealth) currentHealth = maxHealth;
            float percentageHealth = (float)currentHealth / (float)maxHealth;
            EventManager.instance.OnHealthChanged?.Invoke(percentageHealth);
        }
    }

    internal void HealFull()
    {
        Heal(maxHealth);
    }
}
