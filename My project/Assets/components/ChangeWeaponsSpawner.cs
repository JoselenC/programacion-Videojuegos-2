using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeaponsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject changeWeapons;
    [SerializeField] private GameObject player; 
    private float _spawnInterval = 20f; 
    private float _spawnDelay = 30f; 
    private int _count;
    private void Start()
    {
        _count = 0;
        InvokeRepeating("SpawnChangeWeapons", _spawnDelay, _spawnInterval);
    }
    
    private void SpawnChangeWeapons()
    {
        if (_count % 2 == 0)
        {
            GameObject prefab1 = Instantiate(changeWeapons,new Vector3(1f, player.transform.position.y+10, 0f), Quaternion.identity);
            DestroyPrefab(prefab1);
        }
        else
        {
            GameObject prefab1 = Instantiate(changeWeapons,new Vector3(1f, player.transform.position.y+10, 0f), Quaternion.identity);
            DestroyPrefab(prefab1);
        }
        _count++;
    }
    
    private void DestroyPrefab(GameObject prefab)
    {
        Destroy(prefab, 30f);
    }
}
