using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleSpawner : MonoBehaviour
{

    [SerializeField] private GameObject[] obstaclePrefabs; 
    [SerializeField] private GameObject player;
    private float _spawnInterval = 3f; 
    private float _spawnDelay = 2f; 
    private int _count;
    private int _controlCount1200;
    private int _controlCount5000;
    private int _controlCount5001;
    private void Start()
    {
        _count = 0;
        _controlCount1200 = 0;
        _controlCount5000 = 0;
        _controlCount5001 = 0;
        InvokeRepeating("SpawnObstacle", _spawnDelay, _spawnInterval);
    }

    private void Update()
    {
        int score = PlayerPrefs.GetInt("score");
        if (score < 1200)
        {
            if (_controlCount1200 == 0)
            {
                _spawnInterval = 10f;
                CancelInvoke("SpawnObstacle");
                InvokeRepeating("SpawnObstacle", _spawnDelay, _spawnInterval);
                _controlCount1200 = 1;
            }
        }
        else if (score<5000)
        {
            if (_controlCount5000 == 0)
            {
                _spawnInterval = 8f;
                CancelInvoke("SpawnObstacle");
                InvokeRepeating("SpawnObstacle", _spawnDelay, _spawnInterval);
                _controlCount5000 = 1;
            }
        }
        else
        {
            if (_controlCount5001 == 0)
            {
                _spawnInterval = 6f;
                CancelInvoke("SpawnObstacle");
                InvokeRepeating("SpawnObstacle", _spawnDelay, _spawnInterval);
                _controlCount5001 = 1;
            }
        }
    }

    private void SpawnObstacle()
    {
        int score = PlayerPrefs.GetInt("score");
        if (score < 1200)
        {
            FirstObstacleLevel();
        }
        else if (score<5000)
        {
            SecondObstacleLevel();
        }
        else
        {
            ThirdObstacleLevel();
        }
        _count++;

    }
    
    private void FirstObstacleLevel()
    {
        if (_count % 2 == 0)
        {
            float yPosition = transform.position.y;
            GameObject obstaclePrefab = obstaclePrefabs[2];
            for (int i = 0; i < 3; i++)
            {
                float xPosition = transform.position.x + i;
                Instantiate(obstaclePrefab, new Vector3(xPosition, yPosition + 12f, 0f), Quaternion.identity);
            }
            for (int i = 1; i < 3; i++)
            {
                float xPosition = transform.position.x - i;
                Instantiate(obstaclePrefab, new Vector3(xPosition, yPosition + 12f, 0f), Quaternion.identity);
            }
        }
        else
        {
            int randomIndex = Random.Range(0, obstaclePrefabs.Length - 1);
            GameObject obstaclePrefab = obstaclePrefabs[randomIndex];
            Instantiate(obstaclePrefab, new Vector3(1f, player.transform.position.y + 13f, 0f), Quaternion.identity);
        }
    }

    private void SecondObstacleLevel()
    {
        if (_count % 2 == 0)
        {
            GameObject obstaclePrefab = obstaclePrefabs[0];
            var position = player.transform.position;
            Instantiate(obstaclePrefab, new Vector3(0f, position.y + 7f, 0f), Quaternion.identity);
            obstaclePrefab = obstaclePrefabs[1];
            Instantiate(obstaclePrefab, new Vector3(1f, position.y + 20f, 0f), Quaternion.identity);
        }
        else
        {
            GameObject obstaclePrefab = obstaclePrefabs[1];
            var position = player.transform.position;
            Instantiate(obstaclePrefab, new Vector3(-1f, position.y + 7f, 0f), Quaternion.identity);
            obstaclePrefab = obstaclePrefabs[0];
            Instantiate(obstaclePrefab, new Vector3(0f, position.y + 20f, 0f), Quaternion.identity);
        }
    }

    private void ThirdObstacleLevel()
    {
        if (_count % 2 == 0)
        {
            GameObject obstaclePrefab = obstaclePrefabs[0];
            var position = player.transform.position;
            Instantiate(obstaclePrefab, new Vector3(0f, position.y + 8f, 0f), Quaternion.identity);
            obstaclePrefab = obstaclePrefabs[1];
            Instantiate(obstaclePrefab, new Vector3(1f, position.y + 19f, 0f), Quaternion.identity);
        }
        else
        {
            GameObject obstaclePrefab = obstaclePrefabs[0];
            var position = player.transform.position;
            Instantiate(obstaclePrefab, new Vector3(-1f, position.y + 6f, 0f), Quaternion.identity);
            obstaclePrefab = obstaclePrefabs[1];
            Instantiate(obstaclePrefab, new Vector3(0f, position.y + 17f, 0f), Quaternion.identity);
        }
    }
}