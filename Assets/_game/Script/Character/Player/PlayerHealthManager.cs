using System;

public class PlayerHealthManager : CharacterHealthManager
{
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        float percentageHealth = (float)currentHealth / (float)maxHealth;
        EventManager.instance.OnHealthChanged?.Invoke(percentageHealth);
    }

    public override void Die()
    {
        base.Die();
        EventManager.instance.OnPlayerDied?.Invoke();
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

    internal void HealAll()
    {
        Heal(maxHealth);
    }
}
