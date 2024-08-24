using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public List<GameObject> Gems = new();
    public List<GameObject> ActivateGems = new();
    public List<GameObject> ToDestroy = new();
    [SerializeField] private float destroyTimerDuration = 0.2f;
    [SerializeField] private int finalSceneIndex = 3;
    [SerializeField] private GameObject explosionPrefab;

    private Coroutine destroyTimerCoroutine;

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
    }

    public void AddToDestroy(GameObject gem)
    {
        if (ToDestroy != null && gem != null)
        {
            ToDestroy.Add(gem);
            if (destroyTimerCoroutine != null)
            {
                StopCoroutine(destroyTimerCoroutine);
            }
            destroyTimerCoroutine = StartCoroutine(DestroyTimer());
        }
    }

    private IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(destroyTimerDuration);
        ClearToDestroy();
    }

    public void ClearToDestroy()
    {
        if (ToDestroy.Count >= 3)
        {
            foreach (GameObject gem in ToDestroy)
            {
                if (explosionPrefab != null)
                {
                    Instantiate(explosionPrefab, gem.transform.position, Quaternion.identity);
                }

                gem.SetActive(false);
            }
        }
        ToDestroy.Clear();
    }

    public void AddGem(GameObject gem)
    {
        if (Gems != null && gem != null)
        {
            Gems.Add(gem);
        }
    }

    public void AddActiveGems(GameObject gem)
    {
        if (ActivateGems != null && gem != null)
        {
            ActivateGems.Add(gem);
        }
    }

    public void CheckActiveGems()
    {
        bool hasActiveGems = false;

        foreach (GameObject gem in Gems)
        {
            if (gem.activeInHierarchy)
            {
                hasActiveGems = true;
                break;
            }
        }

        if (!hasActiveGems)
        {
            LoadFinalScene();
        }
        else
        {
            Debug.Log("Active gems are still present. Cannot proceed to the final scene.");
        }
    }

    private void LoadFinalScene()
    {
        if (finalSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(finalSceneIndex);
        }
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
            /*if (closestGem.TryGetComponent(out Collider2D collider))
            {
                collider.isTrigger = true;
            }*/
            closestGem.GetComponent<Gem>().Activate(index);
        }
    }
}
