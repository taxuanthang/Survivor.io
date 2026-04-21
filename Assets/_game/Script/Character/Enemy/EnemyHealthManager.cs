public class EnemyHealthManager : CharacterHealthManager
{
    public override void Die()
    {
        base.Die();
        // run enemy death animation, disable enemy AI, etc.
        PoolManager.instance.Return(PoolType.Enemy, this.gameObject);
    }
}
