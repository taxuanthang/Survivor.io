using UnityEngine;

public class EnemyHealthManager : CharacterHealthManager
{
    public float DieDuration;
    public override void Die()
    {
        base.Die();
        // run enemy death animation, disable enemy AI, etc.
        EventManager.instance.OnEnemyDie?.Invoke();
        WaitDieAnim();
    }

    public async void WaitDieAnim()
    {
        await Awaitable.WaitForSecondsAsync(DieDuration);
        PoolManager.instance.Return(PoolType.Enemy, this.gameObject);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        EventManager.instance.OnEnemyHit?.Invoke();
    }

}
