using UnityEngine;

[CreateAssetMenu(fileName = "IdleState", menuName = "ScriptableObjects/EnemyAI/IdleState")]
public class IdleState : State
{
    public override void Enter(EnemyAIManager enemy)
    {
        // run idle animation, etc.
    }
    public override void Execute(EnemyAIManager enemy)
    {
        // check nếu ko ở trong tầm đánh thì đuổi người chơi
        // check nếu ở trong tầm đánh thì tấn công người chơi
        enemy.ChangeState(enemy._chaseState);
    }
    public override void Exit(EnemyAIManager enemy)
    {
        // stop idle animation, etc.
    }
}
