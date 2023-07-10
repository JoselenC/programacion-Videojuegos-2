using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] starsPrefabs; 
    [SerializeField] private GameObject player;
    private float spawnInterval = 10f; 
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
            GameObject starsPrefab = starsPrefabs[randomIndex];
            GameObject prefab1 = Instantiate(starsPrefab, new Vector3(-0.8f, player.transform.position.y+15f, 0f), Quaternion.identity);
            DestroyPrefab(prefab1);
        }
        else
        {
            int randomIndex = Random.Range(0, starsPrefabs.Length);
            GameObject starsPrefab = starsPrefabs[randomIndex];
            GameObject prefab1 = Instantiate(starsPrefab, new Vector3(0.8f, player.transform.position.y+10f, 0f), Quaternion.identity);
            DestroyPrefab(prefab1);
        }
        count++;
    }
    private void DestroyPrefab(GameObject prefab)
    {
        Destroy(prefab, 30f);
    }
}
