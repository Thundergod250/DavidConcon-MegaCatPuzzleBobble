using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ObjectPoolEntry
{
    public GameObject prefab;
    public int poolSize;
    public Transform poolParent;

    private Queue<GameObject> pool;

    public void InitializePool()
    {
        pool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = GameObject.Instantiate(prefab);
            obj.SetActive(false);
            obj.transform.parent = poolParent;
            pool.Enqueue(obj);
        }
    }

    public GameObject GetObject()
    {
        if (pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject obj = GameObject.Instantiate(prefab);
            obj.transform.parent = poolParent;
            return obj;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.parent = poolParent;
        pool.Enqueue(obj);
    }
}

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler Instance;

    [SerializeField] private ObjectPoolEntry[] poolEntries;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        foreach (ObjectPoolEntry entry in poolEntries)
        {
            entry.InitializePool();
        }
    }

    public GameObject GetObject(GameObject prefab)
    {
        foreach (ObjectPoolEntry entry in poolEntries)
        {
            if (entry.prefab == prefab)
            {
                return entry.GetObject();
            }
        }
        return null;
    }

    public void ReturnObject(GameObject obj)
    {
        foreach (ObjectPoolEntry entry in poolEntries)
        {
            if (entry.prefab == obj)
            {
                entry.ReturnObject(obj);
                return;
            }
        }
    }
}
