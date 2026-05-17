using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.Rendering.DebugUI;

public class HitBoxDame : MonoBehaviour , IPoolable
{
    public HitBoxType hitBoxType;
    public float radiusOuterOfHitBox;
    public float innerToOuterOfHitBox;
    public SpriteRenderer spriteRenderer ;

    public bool canHitBoxRingHit = true;
    public UnityEvent onDespawn;

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Dùng cho hit box dạng circle
    /// </summary>
    public void SetUp(Vector3 createPos, Quaternion rotation, PoolType poolType, float hitBoxDuration)
    {
        transform.position = createPos;
        transform.rotation = rotation;
        this.hitBoxType = HitBoxType.Circle;
        DestroyTimer destroyTimer = gameObject.GetComponent<DestroyTimer>();
        destroyTimer.poolType = poolType;
        destroyTimer.timeToDestroy = hitBoxDuration;
    }

    /// <summary>
    /// Dùng cho hit box dạng ring, innerToOuterOfHitBox là khoảng cách từ vòng trong đến vòng ngoài của hitbox
    /// </summary>
    public void SetUp(Vector3 createPos, Quaternion rotation, PoolType poolType, float hitBoxDuration,float innerToOuterOfHitBox)
    {
        transform.position = createPos;
        transform.rotation = rotation;
        this.hitBoxType = HitBoxType.Ring;
        this.innerToOuterOfHitBox = innerToOuterOfHitBox;
        DestroyTimer destroyTimer = gameObject.GetComponent<DestroyTimer>();
        destroyTimer.poolType = poolType;
        destroyTimer.timeToDestroy = hitBoxDuration;
    }

    public void Update()
    {
        // logic khi khởi tạo poolable
        if (hitBoxType == HitBoxType.Circle)
        {
            radiusOuterOfHitBox = GetComponent<CircleCollider2D>().radius * transform.localScale.x;
        }
    }

    public void OnSpawn()
    {
        // logic khi spawn hitbox
    }

    public void OnDespawn()
    {
        this.transform.localScale = new Vector3(2,2,1);
        onDespawn?.Invoke();
        onDespawn.RemoveAllListeners();
    }
    // class này sẽ được attach vào hitbox prefab để xử lý va chạm va
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent(out PlayerManager player))
        {
            switch(hitBoxType)
            {
                case HitBoxType.Circle:
                    // logic khi va chạm với player thính thường
                    // khi va chạm với player thi
                    player.OnHit?.Invoke(10);
                    break;
                case HitBoxType.Rect:
                    // logic khi va chạm với player thính thường
                    // khi va chạm với player thi
                    player.OnHit?.Invoke(10);
                    break;
            }

        }
    }
    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerManager player))
        {
            switch (hitBoxType)
            {
                case HitBoxType.Circle:

                    break;
                case HitBoxType.Ring:
                    if(!canHitBoxRingHit)
                    {
                        return;
                    }
                    canHitBoxRingHit = false;
                    Vector3 closestPoint = collision.ClosestPoint(transform.position);
                    // logic khi va chạm với player thính thường nhưng có thêm hiệu ứng ring
                    float distanceToPlayer = Vector2.Distance(transform.position, closestPoint);
                    print(distanceToPlayer + " " + radiusOuterOfHitBox + " " + (radiusOuterOfHitBox - innerToOuterOfHitBox));
                    if (distanceToPlayer <= radiusOuterOfHitBox && distanceToPlayer >= radiusOuterOfHitBox - innerToOuterOfHitBox)
                    {
                        player.OnHit?.Invoke(10);
                        // thêm hiệu ứng 
                    }
                    StartCoolDownHitBoxRing();
                    break;
            }

        }
    }

    public async void StartCoolDownHitBoxRing()
    {
        await Awaitable.WaitForSecondsAsync(2f);
        canHitBoxRingHit = true;
    }

    public void OnDrawGizmos()
    {
        if(hitBoxType == HitBoxType.Ring)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radiusOuterOfHitBox);
            Gizmos.DrawWireSphere(transform.position, radiusOuterOfHitBox - innerToOuterOfHitBox);
        }
    }

    public void SetTimer(float hitBoxDuration)
    {
        DestroyTimer destroyTimer = gameObject.GetComponent<DestroyTimer>();
        destroyTimer.timeToDestroy = hitBoxDuration;
    }

}
