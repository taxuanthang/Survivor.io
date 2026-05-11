using NaughtyAttributes;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyAICombatManager : MonoBehaviour
{

    public List<Attack> rangeAttacks;

    public Attack test;
    public Transform target;


    public EnemyAIManager enemyAIManager;

    public void Awake()
    {
        enemyAIManager = GetComponent<EnemyAIManager>();

    }

    [Button]
    public void Test()
    {
        print("attack");
        test.PerformAttack(this.transform, target);
    }

    // class này sẽ quản lý các hành động combat của enemy
    public HitBoxDame hitBoxDame;
    public float hitBoxDuration = 0.5f; // thời gian tồn tại của hitbox
    public float attackCooldown = 2f; // thời gian tồn tại của hitbox


    public async Task PerformMeleeAttack(CharacterManager target)
    {
        // logic để thực hiện melee attack, có thể gọi animation, kiểm tra
        // giờ sẽ tạo ra một ô vuông gây dame và sẽ xóa nó đi sau 1 khoảng thời gian
        Vector3 createPos = (target.transform.position - this.transform.position).normalized * 0.5f + this.transform.position; // tạo hitbox ở vị trí gần enemy
        HitBoxDame hitBox = PoolManager.instance.Get(PoolType.HitBox).GetComponent<HitBoxDame>();
        hitBox.SetUp(createPos, Quaternion.identity, PoolType.HitBox, hitBoxDuration);

        await Task.Delay((int)(attackCooldown * 1000)); // chờ cho hitbox tồn tại xong rồi mới có thể thực hiện các hành động tiếp theo
    }
}
