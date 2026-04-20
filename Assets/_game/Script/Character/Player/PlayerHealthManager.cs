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
}
