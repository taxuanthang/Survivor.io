
using UnityEngine;

[CreateAssetMenu(fileName = "MeleeAttackState", menuName = "ScriptableObjects/EnemyAI/MeleeAttackState")]
public class MeleeAttackState : State
{
    // có thẻ có nhiều loại attack nên ở đây sẽ có 2 cách, 1 là tạo từng class riêng cho từng loại attack, 2 là tạo 1 class AttackState rộng hơn và truyền tham số để xác định loại attack
    // sẽ đi theo cách 1

    public bool canAttack = true;

    CharacterManager target;

    EnemyAIManager _enemyAIManager;

    public override void Enter(EnemyAIManager enemy)
    {
        // run attack animation, etc.
        target = enemy.destinationSetter.target.gameObject.GetComponent<CharacterManager>();
        _enemyAIManager = enemy;
    }
    public override void Execute(EnemyAIManager enemy)
    {
        // perform attack logic, etc.
        // nếu đánh xong rồi mà vẫn ngoài tầm thì chuyển về chase state, còn trong tầm thì đánh tiếp
        // ko thể dùng đến couroutine trong này nên phải dùng đến task(bất đồng bộ)
        if (!enemy.aiPath.reachedEndOfPath)
        {
            enemy.ChangeState(enemy._chaseState);
            return;
        }

        if (!canAttack)
        {
            return;
        }
        canAttack = false;

        Attack(target);







    }
    public override void Exit(EnemyAIManager enemy)
    {
        // stop attack animation, etc.
    }

    public async void Attack(CharacterManager target)
    {

        // thêm cooldown cho attack
        await _enemyAIManager._enemyAICombatManager.PerformMeleeAttack(target);
        canAttack = true;
    }
}