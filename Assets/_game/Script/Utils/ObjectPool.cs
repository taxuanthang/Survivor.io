using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;
    public PoolType poolType;
    public int poolSize = 10;

    private Queue<GameObject> pool = new Queue<GameObject>();

    void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab,transform.position,Quaternion.identity,transform);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public void Start()
    {
        PoolManager.instance.RegisterPool(poolType, this);
    }

    public GameObject Get()
    {
        GameObject obj = null;
        if (pool.Count > 0)
        {
            obj = pool.Dequeue();
            obj.SetActive(true);
            obj.GetComponent<IPoolable>()?.OnSpawn();
            return obj;
        }

        // hết pool thì tạo thêm (optional)
        obj = Instantiate(prefab);
        obj.GetComponent<IPoolable>()?.OnSpawn();
        return obj;
    }

    public void Return(GameObject obj)
    {
        obj.SetActive(false);
        obj.GetComponent<IPoolable>()?.OnDespawn();
        pool.Enqueue(obj);
    }
}
