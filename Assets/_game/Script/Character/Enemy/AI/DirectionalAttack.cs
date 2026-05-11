using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "DirectionalAttack", menuName = "Attack/Directional")]
public class DirectionalAttack : Attack
{
    public float damage;
    public float range;
    public Vector2 direction;
    public float angle;
    public LayerMask targetLayer;

    // class này sẽ quản lý các hành động combat của enemy
    public float hitBoxDuration = 0.5f; // thời gian tồn tại của hitbox
    public float hitboxDistanceBetweenEach = 2f; // khoảng cách giữa các hitbox
    public float hitboxCount = 20; // số lượng hitbox được tạo ra
    public float timeBetweenEachTimeCreateHitbox = 0.5f; // 



    public override async Task PerformAttack(Transform origin, Transform target)
    {

        Vector3 directionToTargetVector = (target.position - origin.position).normalized;
        
        for (int i = 0; i < hitboxCount; i++)
        {
            HitBoxDame hitBox = PoolManager.instance.Get(PoolType.HitBox).GetComponent<HitBoxDame>();
            Vector3 createPos = origin.position + (Vector3)directionToTargetVector * i* hitboxDistanceBetweenEach;
            hitBox.SetUp(createPos, Quaternion.identity, PoolType.HitBox, hitBoxDuration);
            await Awaitable.WaitForSecondsAsync(timeBetweenEachTimeCreateHitbox);
        }

    }
}
