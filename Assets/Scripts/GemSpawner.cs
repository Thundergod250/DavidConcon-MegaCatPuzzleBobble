using System.Collections;
using UnityEngine;

public class GemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject gemPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GemFiringController gemFiringController;

    private bool canSpawn = true;

    private void Start()
    {
        if (gemFiringController)
            gemFiringController.EvtGemFired.AddListener(SpawnGem);

        SpawnGem();
    }

    private void SpawnGem()
    {
        if (canSpawn)
        {
            canSpawn = false;
            StartCoroutine(SpawnDelay());
        }
    }

    private IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(0.5f);

        GameObject gem = Instantiate(gemPrefab, spawnPoint.position, Quaternion.identity);
        gem.transform.parent = spawnPoint;
        gem.GetComponent<Gem>().RandomizeGemType();

        if (gemFiringController)
            gemFiringController.SetCurrentGem(gem);

        gem.AddComponent<PlayerGem>(); 

        canSpawn = true;
    }
}
