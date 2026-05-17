using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    public float timeToDestroy = 0.5f;
    public PoolType poolType;
    public bool returnToPooled = true;

    public void Update()
    {
        timeToDestroy -= Time.deltaTime;
        if (timeToDestroy <= 0)
        {
            if (returnToPooled)
            { 
                PoolManager.instance.Return(poolType, this.gameObject);
            }
            else
            {
                Object.Destroy(this.gameObject);
            }
        }
    }
}
