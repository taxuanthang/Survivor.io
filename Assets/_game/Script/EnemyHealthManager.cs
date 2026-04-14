public class EnemyHealthManager : CharacterHealthManager
{
    public override void Die()
    {
        base.Die();
        // run enemy death animation, disable enemy AI, etc.
        Destroy(gameObject, 0.3f);
    }
}
