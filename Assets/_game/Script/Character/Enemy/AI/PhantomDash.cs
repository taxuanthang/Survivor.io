using System;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "PhantomDash", menuName = "Attack/PhantomDash")]
public class PhantomDash : Attack
{
    public float damage;

    // class này sẽ quản lý các hành động combat của enemy
    public float hitBoxDuration = 0.5f; // thời gian tồn tại của hitbox
    public float dashTime = 1f;
    public float dashSpeed = 1f;
    public float timeOfSkillCharges = 1f;



    public override async Task PerformAttack(Transform origin, Transform target)
    {

        // xác định vị trí hiện tại của nhân vật, chờ vài giây và dash nhân vật đến vị trí đó

        Vector3 dashPos = target.position;

        // play anim rặn chiêu
        await Awaitable.WaitForSecondsAsync(timeOfSkillCharges);

        // dash đến vị trí đã xác định
        await DashToPosition(origin, dashPos);

        // tạo hitbox tại vị trí dash
        HitBoxDame hitBox = PoolManager.instance.Get(PoolType.HitBox).GetComponent<HitBoxDame>();
        hitBox.SetUp(dashPos, Quaternion.identity, PoolType.HitBox, hitBoxDuration);


    }

    public async Task DashToPosition(Transform origin, Vector3 dashPos)
    {

        // Set để ko di chuyển được khi đang dodge
        Vector2 dir = (dashPos - origin.position).normalized;
        while (Vector3.Distance(origin.position, dashPos) > 0.1)
        {
            origin.position = Vector3.Lerp(origin.position, dashPos, dashSpeed * dashTime);
            dashTime += Time.deltaTime;
            await Awaitable.NextFrameAsync();
        }
        origin.position = dashPos;
        dashTime = 0;
    }
}