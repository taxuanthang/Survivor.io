using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "RotatingLaserCross", menuName = "Attack/RotatingLaserCross")]
public class RotatingLaserCross : Attack
{
    public float damage;

    // class này sẽ quản lý các hành động combat của enemy
    public float hitboxDistanceBetweenEach = 2f; // khoảng cách giữa các hitbox
    public float hitboxCount = 20; // số lượng hitbox được tạo ra
    public float rotationDuration = 5f; // thời gian để hoàn thành một vòng quay
    public float rotateSpeed = 360f; // tốc độ quay (độ trên giây)


    public override async Task PerformAttack(Transform origin, Transform target)
    {
        List<HitBoxDame> hitboxes = new List<HitBoxDame>();

        // tạo một dấu X Quanh người thằng boss và xoay nó ?
        for (int j = 0; j < 4; j++)
        {
            Vector3 createDirection = Quaternion.Euler(0, 0, 90 * j) * Vector3.down;
            for (int i = 0; i < hitboxCount; i++)
            {
                HitBoxDame hitBox = PoolManager.instance.Get(PoolType.HitBox).GetComponent<HitBoxDame>();
                hitboxes.Add(hitBox);
                Vector3 createPos = origin.position + createDirection * i * hitboxDistanceBetweenEach;
                hitBox.SetUp(createPos, Quaternion.identity, PoolType.HitBox, rotationDuration);
            }
        }

        float tempRotationDuration = rotationDuration;
        while (rotationDuration>0)
        {
            foreach (HitBoxDame hitbox in hitboxes)
            {
                if (hitbox != null)
                {
                    hitbox.transform.RotateAround(origin.position, Vector3.forward, rotateSpeed * Time.deltaTime);
                }
            }
            rotationDuration -= Time.deltaTime;
            await Awaitable.NextFrameAsync();
        }
        rotationDuration = tempRotationDuration;
    }
}
