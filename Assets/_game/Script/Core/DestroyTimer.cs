using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    public float timeToDestroy = 0.5f;
    public PoolType poolType;

    public void Update()
    {
        timeToDestroy -= Time.deltaTime;
        if (timeToDestroy <= 0)
        {
            PoolManager.instance.Return(poolType, this.gameObject);
        }
    }
}
