using UnityEngine;

public class ShadowSlam : MonoBehaviour , IPoolable
{
    public SpriteRenderer shadowSlamSprite;
    public DestroyTimer timer;


    public void Awake()
    {
        if(shadowSlamSprite == null)  shadowSlamSprite = GetComponent<SpriteRenderer>();
        if(timer == null)           timer = GetComponent<DestroyTimer>();
    }
    public void SetUp(Vector3 createPos, Quaternion rotation, PoolType poolType, float hitBoxDuration)
    {
        transform.position = createPos;
        transform.rotation = rotation;
        timer.poolType = poolType;
        timer.timeToDestroy = hitBoxDuration;
    }

    public void OnSpawn()
    {

    }
    public void OnDespawn()
    {
        shadowSlamSprite.color = Color.white; // Reset màu sprite về mặc định khi despawn
    }
}
