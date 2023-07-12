using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : MonoBehaviour
{

    [SerializeField] private GameObject bombPrefabs;
    [SerializeField] private GameObject player; 
    private float _spawnInterval = 20f; 
    private float _spawnDelay = 2f; 
    private int _controlCount1200;
    private int _controlCount5000;
    private int _controlCount5001;
    private int _count;
    private void Start()
    {
        _controlCount1200 = 0;
        _controlCount5000 = 0;
        _controlCount5001 = 0;
        _count = 0;
        InvokeRepeating("SpawnBomb", _spawnDelay, _spawnInterval);
    }

    private void Update()
    {
        int score = PlayerPrefs.GetInt("score");
        if (score < 20)
        {
            if (_controlCount1200 == 0)
            {
                _spawnInterval = 40f;
                CancelInvoke("SpawnBomb");
                InvokeRepeating("SpawnBomb", _spawnDelay, _spawnInterval);
                _controlCount1200 = 1;
            }
        }
        else if (score<50)
        {
            if (_controlCount5000 == 0)
            {
                _spawnInterval = 50f;
                CancelInvoke("SpawnBomb");
                InvokeRepeating("SpawnBomb", _spawnDelay, _spawnInterval);
                _controlCount5000 = 1;
            }
        }
        else
        {
            if (_controlCount5001 == 0)
            {
                _spawnInterval = 60f;
                CancelInvoke("SpawnBomb");
                InvokeRepeating("SpawnBomb", _spawnDelay, _spawnInterval);
                _controlCount5001 = 1;
            }
        }
    }

    private void SpawnBomb()
    {
        if (_count % 2 == 0)
        {
            Instantiate(bombPrefabs,  new Vector3(1f, player.transform.position.y+14f, 0f), Quaternion.identity);
        }
        else
        {
            Instantiate(bombPrefabs, new Vector3(-1f, player.transform.position.y+14f, 0f), Quaternion.identity);
        }
        _count++;
    }

}
