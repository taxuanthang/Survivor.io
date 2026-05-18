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

    public GameObject directionPrefabs;


    public override async Task PerformAttack(Transform origin, Transform target)
    {
        List<HitBoxDame> hitboxes = new List<HitBoxDame>();

        // tạo một dấu X Quanh người thằng boss và xoay nó ?
        for (int j = 0; j < 4; j++)
        {
            Vector3 createDirection = Quaternion.Euler(0, 0, 90 * j) * Vector3.down;

                //HitBoxDame hitBox = PoolManager.instance.Get(PoolType.HitBox).GetComponent<HitBoxDame>();
                //hitboxes.Add(hitBox);
                //Vector3 createPos = origin.position + createDirection * i * hitboxDistanceBetweenEach;
                //hitBox.SetUp(createPos, Quaternion.identity, PoolType.HitBox, rotationDuration);

                GameObject direction = Instantiate(directionPrefabs, origin.transform.position, Quaternion.Euler(0, 0, 90 * j));

                DestroyTimer timer = direction.AddComponent<DestroyTimer>();
                timer.timeToDestroy = rotationDuration;
                timer.returnToPooled = false;
                BoxCollider2D collider = direction.AddComponent<BoxCollider2D>();
                collider.offset = new Vector2 (3.15f, 0);
                collider.size = new Vector2(5.7f, 1);
                collider.isTrigger = true;
                HitBoxDame hitBoxDame = direction.AddComponent<HitBoxDame>();
                hitBoxDame.hitBoxType = HitBoxType.Rect;

                direction.transform.localScale = new Vector3 (20f,1f,1f);
                hitboxes.Add(hitBoxDame);

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
