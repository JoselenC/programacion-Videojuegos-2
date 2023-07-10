using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class RegisterController : MonoBehaviour
{
    public InputField usernameInput;
    public InputField passwordInput;
    private List<PlayerData> _players;
    private DataPersistManager _persistManager;
    [SerializeField] private Text errorText;
    private AudioSource _audioSource;
    public void Start()
    {
        _persistManager = FindObjectOfType<DataPersistManager>();
        _players = new List<PlayerData>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void Register()
    {
        _audioSource.Play();
        string username = usernameInput.text;
        string password = passwordInput.text;
        if (username == "" || password == "")
        {
            errorText.text = "Fill in user data";
            Debug.Log("Fill in user data");
        }
        else
        {
            if (!AlreadyExistUser(username, password))
            {
                PlayerData player = new PlayerData()
                    { playerUsername = username, playerPassword = password, playerLevel = 0, playerScore = 0 };
                _persistManager.SavePlayerData(player, "playerData.json");

                PlayerPrefs.SetString("playerName", username);
                PlayerData currentPlayer = null;
                _players = _persistManager.LoadPlayerData("playerData.json");
                foreach (var player2 in _players)
                {
                    if (username == player2.playerUsername)
                    {
                        currentPlayer = player2;
                    }
                }

                if (currentPlayer != null)
                {
                    PlayerPrefs.SetInt("level", currentPlayer.playerLevel);
                    PlayerPrefs.SetInt("score", currentPlayer.playerScore);
                }

                errorText.text = "Successful registration";
                Debug.Log("Successful registration");
                SceneManager.LoadScene("Level");
            }
            else
            {
                errorText.text = "Already exist user";
                Debug.Log("Already exist user");
            }
        }
    }

    private bool AlreadyExistUser(string username, string password)
    {
       _players = _persistManager.LoadPlayerData("playerData.json");
       foreach (var player2 in _players)
       {
           if (username == player2.playerUsername)
           {
               return true;
           }
       }
       return false;
    }
    
    public void HomeMenu()
    {
        _audioSource.Play();
        SceneManager.LoadScene("Menu");
    }
}