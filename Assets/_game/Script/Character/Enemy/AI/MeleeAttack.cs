using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "MeleeAttack", menuName = "Attack/MeleeAttack")]
public class MeleeAttack : Attack
{
    // class này sẽ quản lý các hành động combat của enemy
    public HitBoxDame hitBoxDame;
    public float hitBoxDuration = 0.5f; // thời gian tồn tại của hitbox
    public float attackCooldown = 2f; // thời gian tồn tại của hitbox


    public override async Task PerformAttack(Transform origin, Transform target)
    {
        // logic để thực hiện melee attack, có thể gọi animation, kiểm tra
        // giờ sẽ tạo ra một ô vuông gây dame và sẽ xóa nó đi sau 1 khoảng thời gian
        Vector3 createPos = (target.position - origin.position).normalized * 0.5f + origin.position; // tạo hitbox ở vị trí gần enemy
        HitBoxDame hitBox = PoolManager.instance.Get(PoolType.HitBox).GetComponent<HitBoxDame>();
        hitBox.SetUp(createPos, Quaternion.identity, PoolType.HitBox, hitBoxDuration);

        await Task.Delay((int)(attackCooldown * 1000)); // chờ cho hitbox tồn tại xong rồi mới có thể thực hiện các hành động tiếp theo
    }
}

