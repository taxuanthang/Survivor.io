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
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }

        // hết pool thì tạo thêm (optional)
        GameObject newObj = Instantiate(prefab);
        return newObj;
    }

    public void Return(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}
