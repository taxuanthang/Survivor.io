using UnityEngine;

public class EnemyHealthManager : CharacterHealthManager
{
    public float DieDuration;
    public override void Die()
    {
        base.Die();
        // run enemy death animation, disable enemy AI, etc.
        WaitDieAnim();
        EventManager.instance.OnEnemyDie?.Invoke();
    }

    public async void WaitDieAnim()
    {
        await Awaitable.WaitForSecondsAsync(DieDuration);
        PoolManager.instance.Return(PoolType.Enemy, this.gameObject);
    }


}
