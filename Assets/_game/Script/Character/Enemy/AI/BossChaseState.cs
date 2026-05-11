using UnityEngine;

[CreateAssetMenu(fileName = "ChaseState", menuName = "ScriptableObjects/BossAI/BossChaseState")]
public class BossChaseState : State
{
    public override void Enter(EnemyAIManager enemy)
    {
        // run chase animation, etc.
        enemy.aiPath.canMove = true;
    }
    public override void Execute(EnemyAIManager enemy)
    {
        // move towards player, etc.

        // nếu người chơi trong tầm thì có thể đổi sang melee attack state
        // cách này chỉ phù hợp với chase melee enemy nếu có nhiều loại enemy cân nhắc xem lại
        if (enemy.aiPath.reachedEndOfPath)
        {
            enemy.ChangeState(enemy._meleeAttackState);
        }

        // nếu trong tầm thì roll trong pool

        //Physics2D.OverlapCircle(enemy.transform.position, enemy._enemyAICombatManager._enemyStatsSO.rollRange, enemy._enemyAICombatManager._enemyStatsSO.playerLayerMask);

    }
    public override void Exit(EnemyAIManager enemy)
    {
        // stop chase animation, etc.
        enemy.aiPath.canMove = false;
    }
}