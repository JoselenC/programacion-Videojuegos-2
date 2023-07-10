using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataPersistManager:MonoBehaviour
{
    
    [SerializeField]
    private PlayerData player;
    
    [SerializeField]
    private List<PlayerData> playerDataList;

    [System.Serializable]
    public class PlayerDataListWrapper
    {
        public List<PlayerData> playerDataList;
    }
    
    public void SavePlayerData( PlayerData playerData,string fileName)
    {
        player = playerData;
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        playerDataList.Add(player);
        PlayerDataListWrapper players = new PlayerDataListWrapper()
        {
            playerDataList = playerDataList
        };
        string jsonData = JsonUtility.ToJson(players);
        File.WriteAllText(filePath, jsonData);
    }

    public List<PlayerData> LoadPlayerData(string fileName)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            PlayerDataListWrapper wrapper = JsonUtility.FromJson<PlayerDataListWrapper>(jsonData);
            if (wrapper != null)
            {
                playerDataList = wrapper.playerDataList;
                return playerDataList;
            }
        }
    
        Debug.LogWarning("Player data file does not exist or is empty.");
        return new List<PlayerData>();
    }
    
    public void UpdatePlayerLevel(int level,string username, string fileName)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            PlayerDataListWrapper wrapper = JsonUtility.FromJson<PlayerDataListWrapper>(jsonData);
            if (wrapper != null)
            {
                playerDataList = wrapper.playerDataList;
                int index = playerDataList.FindIndex(p => p.playerUsername == username);
                PlayerData player = playerDataList.Find(p => p.playerUsername == username);
                player.playerLevel = level;
                if (index != -1)
                {
                    playerDataList[index] = player;
                }
                else
                {
                    Debug.LogWarning("Player data with ID " + player.playerUsername + " does not exist.");
                }
            }
        }
        else
        {
            Debug.LogWarning("Player data file does not exist or is empty.");
        }

        PlayerDataListWrapper players = new PlayerDataListWrapper()
        {
            playerDataList = playerDataList
        };
        string updatedJsonData = JsonUtility.ToJson(players);
        File.WriteAllText(filePath, updatedJsonData);
    }

    public void UpdatePlayerScore(int score,string username, string fileName)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            PlayerDataListWrapper wrapper = JsonUtility.FromJson<PlayerDataListWrapper>(jsonData);
            if (wrapper != null)
            {
                playerDataList = wrapper.playerDataList;
                int index = playerDataList.FindIndex(p => p.playerUsername == username);
                PlayerData player = playerDataList.Find(p => p.playerUsername == username);
                player.playerScore = score;
                if (index != -1)
                {
                    playerDataList[index] = player;
                }
                else
                {
                    Debug.LogWarning("Player data with ID " + player.playerUsername + " does not exist.");
                }
            }
        }
        else
        {
            Debug.LogWarning("Player data file does not exist or is empty.");
        }

        PlayerDataListWrapper players = new PlayerDataListWrapper()
        {
            playerDataList = playerDataList
        };
        string updatedJsonData = JsonUtility.ToJson(players);
        File.WriteAllText(filePath, updatedJsonData);
    }
}
