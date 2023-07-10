using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameObject player;
    private float _spawnInterval = 7f;
    private float _spawnDelay = 1f;
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
        InvokeRepeating("SpawnEnemies", _spawnDelay, _spawnInterval);
    }
    
    private void Update()
    {
        int score = PlayerPrefs.GetInt("score");
        SetInterval(score);
    }
    
    private void SetInterval(int score)
    {
        if (score < 1200)
        {
            if (_controlCount1200 == 0)
            {
                _spawnInterval = 11f;
                CancelInvoke("SpawnEnemies");
                InvokeRepeating("SpawnEnemies", _spawnDelay, _spawnInterval);
                _controlCount1200 = 1;
            }
        }
        else if (score < 5000)
        {
            if (_controlCount5000 == 0)
            {
                _spawnInterval = 9f;
                CancelInvoke("SpawnEnemies");
                InvokeRepeating("SpawnEnemies", _spawnDelay, _spawnInterval);
                _controlCount5000 = 1;
            }
        }
        else
        {
            if (_controlCount5001 == 0)
            {
                _spawnInterval = 7f;
                CancelInvoke("SpawnEnemies");
                InvokeRepeating("SpawnEnemies", _spawnDelay, _spawnInterval);
                _controlCount5001 = 1;
            }
        }
    }


    private void SpawnEnemies()
    {
        int score = PlayerPrefs.GetInt("score");
      
        if (score<1200)
        {
            PlayerPrefs.SetInt("level",1);
            FirstEnemiesLevel();
        }
        else if (score < 3500)
        {
            PlayerPrefs.SetInt("level",2);
            SecondEnemiesLevel();
        }
        else if (score < 6200)
        {
            PlayerPrefs.SetInt("level",3);
            ThirdEnemiesLevel();
        }
        else if (score < 9100)
        {
            PlayerPrefs.SetInt("level",4);
            FourthEnemiesLevel();
        }
        else
        {
            PlayerPrefs.SetInt("level",5);
            FifthEnemiesLevel();
        }
        _count++;
    }

        private void FirstEnemiesLevel()
    {
        if (_count % 2 == 0)
        {
            Instantiate(enemyPrefabs[0], GetSpawnPosition(1f, 27f), Quaternion.identity);
            Instantiate(enemyPrefabs[1], GetSpawnPosition(0f, 9f), Quaternion.identity);
        }
        else
        {
            Instantiate(enemyPrefabs[0], GetSpawnPosition(0f, 27f), Quaternion.identity);
            Instantiate(enemyPrefabs[1], GetSpawnPosition(-1f, 9f), Quaternion.identity);
        }
    }

    private void SecondEnemiesLevel()
    {
        if (_count % 2 == 0)
        {
            Instantiate(enemyPrefabs[0], GetSpawnPosition(-1f, 9f), Quaternion.identity);
            Instantiate(enemyPrefabs[2], GetSpawnPosition(0f, 28f), Quaternion.identity);
        }
        else
        {
            Instantiate(enemyPrefabs[1], GetSpawnPosition(1f, 29f), Quaternion.identity);
            Instantiate(enemyPrefabs[2], GetSpawnPosition(0f, 9f), Quaternion.identity);
        }
    }

    private void ThirdEnemiesLevel()
    {
        if (_count % 2 == 0)
        {
            Instantiate(enemyPrefabs[2], GetSpawnPosition(0f, 10f), Quaternion.identity);
            Instantiate(enemyPrefabs[3], GetSpawnPosition(1f, 27f), Quaternion.identity);
        }
        else
        {
            Instantiate(enemyPrefabs[1], GetSpawnPosition(-1f, 29f), Quaternion.identity);
            Instantiate(enemyPrefabs[3], GetSpawnPosition(0f, 10f), Quaternion.identity);
        }
    }

    private void FourthEnemiesLevel()
    {
        if (_count % 2 == 0)
        {
            Instantiate(enemyPrefabs[2], GetSpawnPosition(0f, 10f), Quaternion.identity);
            Instantiate(enemyPrefabs[4], GetSpawnPosition(-1f, 20f), Quaternion.identity);
            Instantiate(enemyPrefabs[3], GetSpawnPosition(1f, 30f), Quaternion.identity);
        }
        else
        {
            Instantiate(enemyPrefabs[1], GetSpawnPosition(0f, 20f), Quaternion.identity);
            Instantiate(enemyPrefabs[4], GetSpawnPosition(-1f, 30f), Quaternion.identity);
            Instantiate(enemyPrefabs[3], GetSpawnPosition(1f, 10f), Quaternion.identity);
        }
    }

    private void FifthEnemiesLevel()
    {
        if (_count % 2 == 0)
        {
            Instantiate(enemyPrefabs[5], GetSpawnPosition(0f, 25f), Quaternion.identity);
            Instantiate(enemyPrefabs[2], GetSpawnPosition(1f, 9f), Quaternion.identity);
        }
        else
        {
            Instantiate(enemyPrefabs[5], GetSpawnPosition(-1f, 9f), Quaternion.identity);
            Instantiate(enemyPrefabs[3], GetSpawnPosition(0f, 28f), Quaternion.identity);
        }
    }

    private Vector3 GetSpawnPosition(float offsetX, float offsetY)
    {
        Vector3 playerPosition = player.transform.position;
        return new Vector3(offsetX, playerPosition.y + offsetY, 0f);
    }

}