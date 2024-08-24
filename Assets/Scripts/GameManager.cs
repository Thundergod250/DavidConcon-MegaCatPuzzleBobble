using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public UnityEvent EvtGemExploded; 
    public List<GameObject> Gems = new();
    public List<GameObject> ToDestroy = new();
    public List<GameObject> ToFall = new();
    public List<FallOff> fallOffCheckers;
    [SerializeField] private float destroyTimerDuration = 0.2f;
    [SerializeField] private float fallingTimerDuration = 0.6f;
    
    private Coroutine destroyTimerCoroutine;
    private Coroutine fallTimerCoroutine;

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

    private void Start()
    {
        EvtGemExploded.AddListener(CheckAllFallOffs); 
    }

    public void RegisterFallOff(FallOff fallOff)
    {
        fallOffCheckers.Add(fallOff); 
    }

    public void CheckAllFallOffs()
    {
        foreach(FallOff fallOff in fallOffCheckers)
        {
            if (fallOff != null)
                fallOff.gameObject.SetActive(true); 
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

    public void AddToFall(GameObject gem)
    {
        if (ToFall != null && gem != null)
        {
            ToFall.Add(gem);
        }
    }

    public IEnumerator FallTimerToClear()
    {
        yield return new WaitForSeconds(fallingTimerDuration);
        ClearFall();
    }

    public IEnumerator FallTimerToFall()
    {
        yield return new WaitForSeconds(fallingTimerDuration);
        FallAll();
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
                gem.SetActive(false);
                EvtGemExploded?.Invoke(); 
            }
        }
        ToDestroy.Clear();
    }

    public void ClearFall()
    {
        ToFall.Clear();
    }

    public void FallAll()
    {
        foreach (GameObject gem in ToFall)
        {
            gem.SetActive(false);
        }
    }

    public void AddGem(GameObject gem)
    {
        if (Gems != null && gem != null)
        {
            Gems.Add(gem);
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
