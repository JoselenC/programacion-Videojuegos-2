using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoginController : MonoBehaviour
{
    [SerializeField] private InputField usernameInput;
    [SerializeField] private InputField passwordInput;
    [SerializeField] private Text errorText;
    private List<PlayerData> players;
    private DataPersistManager persistManager;
    private AudioSource _audioSource;
    public void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        persistManager = FindObjectOfType<DataPersistManager>();
    }

    public void Login()
    {
        _audioSource.Play();
        string username = usernameInput.text;
        string password = passwordInput.text;
        
        if (ValidCredentials(username, password))
        {
            PlayerPrefs.SetString("playerName", username);
            PlayerData currentPlayer=null;
            players = persistManager.LoadPlayerData("playerData.json");
            foreach (var player in players)
            {
                if (username == player.playerUsername)
                {
                    currentPlayer = player;
                }
            }

            if (currentPlayer!=null)
            {
                PlayerPrefs.SetInt("level", currentPlayer.playerLevel);
                PlayerPrefs.SetInt("score", currentPlayer.playerScore);

            }
            
            errorText.text = "login successful";
            Debug.Log("login successful");
            SceneManager.LoadScene("Level");
        }
        else
        {
            errorText.text = "invalid credentials";
            Debug.Log("invalid credentials");
        }
    }

    public void Register()
    {
        _audioSource.Play();
         SceneManager.LoadScene("RegisterMenu");
    }

    private bool ValidCredentials(string username, string password)
    {
       players = persistManager.LoadPlayerData("playerData.json");
        foreach (var player in players)
        {
            if (username == player.playerUsername)
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