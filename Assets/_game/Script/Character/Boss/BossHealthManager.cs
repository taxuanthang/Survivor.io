using UnityEngine;

public class BossHealthManager : CharacterHealthManager
{
    public float DieDuration;
    public override void Die()
    {
        base.Die();
        // run enemy death animation, disable enemy AI, etc.
        print("Call Enemy Die");
        EventManager.instance.OnBossDie?.Invoke();
        EventManager.instance.OnFinishBossRoom?.Invoke();
        WaitDieAnim();
    }

    public async void WaitDieAnim()
    {
        await Awaitable.WaitForSecondsAsync(DieDuration);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        float percentageHealth = (float)currentHealth / (float)maxHealth;
        EventManager.instance.OnBossHit?.Invoke(percentageHealth);
    }
}
