using UnityEngine;

public class LifeSpawner : MonoBehaviour
{
    
    [SerializeField] private GameObject[] starsPrefabs; 
    [SerializeField] private GameObject player;
    private float spawnInterval = 25f; 
    private float spawnDelay = 0f; 
    private int count = 0;
    private void Start()
    {
        count = 0;
        InvokeRepeating("SpawnStar", spawnDelay, spawnInterval);
    }

    private void SpawnStar()
    {
        if (count % 2 == 0)
        {
            int randomIndex = Random.Range(0, starsPrefabs.Length);
            GameObject obstaclePrefab = starsPrefabs[randomIndex];
            GameObject prefab1 = Instantiate(obstaclePrefab, new Vector3(-1f, player.transform.position.y+24f, 0f), Quaternion.identity);
            DestroyPrefab(prefab1);
        }
        else
        {
            int randomIndex = Random.Range(0, starsPrefabs.Length);
            GameObject obstaclePrefab = starsPrefabs[randomIndex];
            GameObject prefab1 = Instantiate(obstaclePrefab, new Vector3(1f, player.transform.position.y+24f, 0f), Quaternion.identity);
            DestroyPrefab(prefab1);
        }
        count++;
    }
    
    private void DestroyPrefab(GameObject prefab)
    {
        Destroy(prefab, 30f);
    }
}