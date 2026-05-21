using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "CreepRangeAttack", menuName = "Attack/CreepRangeAttack")]
public class CreepRangeAttack : Attack
{
    // class này sẽ quản lý các hành động combat của enemy
    public float bulletDuration = 0.5f; // thời gian tồn tại của hitbox
    public float attackCooldown = 2f; // thời gian tồn tại của hitbox

    public Bullet bulletPrefabs;

    public float damageOfBullet;

    public override async Task PerformAttack(Transform origin, Transform target)
    {
        // logic để thực hiện melee attack, có thể gọi animation, kiểm tra
        // giờ sẽ tạo ra một ô vuông gây dame và sẽ xóa nó đi sau 1 khoảng thời gian
        Vector3 dir = (target.position - origin.position).normalized;
        Vector3 createPos = dir * 0.5f + origin.position; // tạo hitbox ở vị trí gần enemy
        Quaternion rotateQua = Quaternion.FromToRotation(Vector3.right, dir);
        Shoot(createPos, rotateQua, dir, BulletType.EnemyBullet);

        await Task.Delay((int)(attackCooldown * 1000)); // chờ cho hitbox tồn tại xong rồi mới có thể thực hiện các hành động tiếp theo
    }

    public void Shoot(Vector3 originPos,Quaternion rotate,Vector2 lookDir, BulletType bulletType)
    {
            // Logic to shoot the gun
            //currentAmmo--;
            // spawn bullet and set its properties based on the gun's properties
            Bullet newBullet = PoolManager.instance.Get(PoolType.EnemyBullet).GetComponent<Bullet>(); // Get a bullet from the pool
            newBullet.destroyTimer.poolType = PoolType.PlayerBullet;
            newBullet.destroyTimer.timeToDestroy = bulletDuration;
            newBullet.transform.position = originPos;
            newBullet.transform.rotation = rotate;
            newBullet.dir = lookDir.normalized;
            newBullet.damage = (int)damageOfBullet;
            newBullet.bulletType = bulletType; // Set the bullet type (player or enemy)
            // nhớ làm object pooling sau để tối ưu hiệu suất thay vì instantiate và destroy bullet liên tục
        
    }


}

