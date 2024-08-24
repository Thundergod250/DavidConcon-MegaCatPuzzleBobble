using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ObjectPoolEntry
{
    public GameObject prefab;
    public int poolSize;

    private Queue<GameObject> pool;

    public void InitializePool()
    {
        pool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = GameObject.Instantiate(prefab);
            obj.SetActive(false);
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
            return obj;
        }
    }

    public void ReturnObject(GameObject obj)
    {
        obj.SetActive(false);
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

        Debug.LogWarning("No pool found for prefab: " + prefab.name);
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

        Debug.LogWarning("No pool found for object: " + obj.name);
    }
}
