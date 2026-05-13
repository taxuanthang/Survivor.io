using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.UI.Image;

[CreateAssetMenu(fileName = "DelayedShadowSlam", menuName = "Attack/DelayedShadowSlam")]
public class DelayedShadowSlam : Attack
{
    public float damage;

    // class này sẽ quản lý các hành động combat của enemy
    public float hitBoxDuration = 0.5f; // thời gian tồn tại của hitbox
    public float numberOfShadowSlam = 15; // số lượng shadow slam sẽ tạo ra
    public float timeBetweenShadowSlam = 0.5f; // thời gian giữa các shadow slam
    public float timeBetweenSpriteAndHitBox = 0.5f; // thời gian giữa việc hiển thị sprite và tạo hitbox
    public float timeBetweenTakePosAndCreateShadowSlamSprite = 0.1f; // thời gian giữa việc hiển thị sprite và tạo hitbox



    public override async Task PerformAttack(Transform origin, Transform target)
    {

        for(int i = 0; i < numberOfShadowSlam; i++)
        {
            CreateShadowSlam(target);
            await Awaitable.WaitForSecondsAsync(timeBetweenShadowSlam);
        }

    }

    async void CreateShadowSlam(Transform target)
    {

        Vector3 createPos = target.position;
        await Awaitable.WaitForSecondsAsync(timeBetweenTakePosAndCreateShadowSlamSprite);

        ShadowSlam shadowSlam = PoolManager.instance.Get(PoolType.ShadowSlam).GetComponent<ShadowSlam>();
        DestroyTimer destroyTimer = shadowSlam.GetComponent<DestroyTimer>();
        float timeToDestroy = hitBoxDuration + timeBetweenSpriteAndHitBox;
        shadowSlam.SetUp(createPos, Quaternion.identity, PoolType.ShadowSlam, timeToDestroy);

        await Awaitable.WaitForSecondsAsync(timeBetweenSpriteAndHitBox);

        shadowSlam.shadowSlamSprite.color = Color.red; // Đổi màu sprite thành đỏ để tạo hiệu ứng
        HitBoxDame hitBox = PoolManager.instance.Get(PoolType.HitBox).GetComponent<HitBoxDame>();
        hitBox.SetUp(createPos, Quaternion.identity, PoolType.HitBox, hitBoxDuration);
    }
}

