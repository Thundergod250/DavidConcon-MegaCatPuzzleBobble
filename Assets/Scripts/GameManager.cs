using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public List<GameObject> Gems = new();
    private GameObject[,] grid;

    public void AddGem(GameObject gem)
    {
        if (Gems != null && gem != null)
        {
            Gems.Add(gem);
        }
    }

    public GameObject[,] GetGrid() 
    {
        return grid;
    }

    public void SetGrid(GameObject[,] gridData) 
    {
        grid = gridData;
    }

    public void EnableTheClosestGrid(Transform transform, int index)
    {
        if (Gems == null || Gems.Count == 0)
        {
            Debug.LogWarning("No gems available to enable.");
            return;
        }

        GameObject closestGem = null;
        float closestDistance = float.MaxValue;

        foreach (GameObject gem in Gems)
        {
            float distance = Vector3.Distance(transform.position, gem.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestGem = gem;
            }
        }

        if (closestGem != null)
        {
            closestGem.SetActive(true);
            closestGem.GetComponent<Gem>().Activate(index);
        }
    }
}
