using UnityEngine;

public class GemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] gemPrefabs;
    [SerializeField] private Transform spawnPoint;

    private void Start()
    {
        SpawnGem();
    }

    private void SpawnGem()
    {
        int randomIndex = Random.Range(0, gemPrefabs.Length);
        GameObject gem = Instantiate(gemPrefabs[randomIndex], spawnPoint.position, Quaternion.identity);
        gem.transform.parent = spawnPoint;
    }
}
