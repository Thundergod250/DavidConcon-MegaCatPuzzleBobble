using System.Collections;
using UnityEngine;

public class GemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] gemPrefabs;
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

        int randomIndex = Random.Range(0, gemPrefabs.Length);
        GameObject gem = Instantiate(gemPrefabs[randomIndex], spawnPoint.position, Quaternion.identity);
        gem.transform.parent = spawnPoint;

        if (gemFiringController)
            gemFiringController.SetCurrentGem(gem);

        canSpawn = true;
    }
}
