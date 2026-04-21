using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    [SerializeField] Dictionary<PoolType, ObjectPool> pools = new Dictionary<PoolType, ObjectPool>();

    public static PoolManager instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    internal void RegisterPool(PoolType poolType, ObjectPool objectPool)
    {
        pools[poolType] = objectPool;
    }

    public GameObject Get(PoolType poolType)
    {
        if (!pools.ContainsKey(poolType))
        {
            return null;
        }
        switch (poolType)
        {
            case PoolType.Enemy:
                return pools[PoolType.Enemy].Get();
            case PoolType.Bullet:
                return pools[PoolType.Bullet].Get();
            case PoolType.HitBox:
                return pools[PoolType.HitBox].Get();
            default:
                return null;
        }
    }

    public void Return(PoolType poolType, GameObject obj)
    {
        if (pools.ContainsKey(poolType))
        {
            pools[poolType].Return(obj);
        }
    }



}

public enum PoolType
{
    Enemy,
    Bullet,
    HitBox
}
