using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "CircleExpand", menuName = "Attack/CircleExpand")]
public class CircleExpandAttack : Attack
{
    public float damage;

    // class này sẽ quản lý các hành động combat của enemy
    public float hitBoxDuration = 0.5f; // thời gian tồn tại của hitbox
    public float circleMaxScale = 20; // scale lớn nhất của circle hitbox
    public float currentCircleMaxScale = 0f; // số lượng hitbox được tạo ra
    public float increaseScaleSpeed = 0.01f; // tốc độ tăng scale của hitbox
    public float innerToOuterOfHitBox = 1f; // khoảng cách từ vòng trong đến vòng ngoài của hitbox



    public override async Task PerformAttack(Transform origin, Transform target)
    {

        HitBoxDame hitBox = PoolManager.instance.Get(PoolType.HitBox).GetComponent<HitBoxDame>();
        Vector3 createPos = origin.position;
        hitBox.SetUp(createPos, Quaternion.identity, PoolType.HitBox, hitBoxDuration, innerToOuterOfHitBox);
        while (currentCircleMaxScale< circleMaxScale)
        {
            await Awaitable.NextFrameAsync();
            currentCircleMaxScale += increaseScaleSpeed;
            hitBox.transform.localScale = new Vector3(currentCircleMaxScale, currentCircleMaxScale, 1);
        }

    }
}