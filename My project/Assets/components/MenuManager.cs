using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
     private AudioSource _audioSource;
     public void Start()
     {
          _audioSource = GetComponent<AudioSource>();
     }
     
    public void PlayClick()
    {
         _audioSource.Play();
        string loadedPlayerName = PlayerPrefs.GetString("playerName");
        if (loadedPlayerName != "")
        {
            // si ya tiene el login, lo llevo a la pantalla de juego
            SceneManager.LoadScene("Level");
        }
        else
        {
            //si no tiene login, lo llevo a la pantalla de login
            SceneManager.LoadScene("LoginMenu"); 
        }
    }
    
    
    public void LoginClick()
    {
         _audioSource.Play();
         SceneManager.LoadScene("LoginMenu");
    }
    
    public void RankingClick()
    {
         _audioSource.Play();
         SceneManager.LoadScene("Ranking");
    }
    
    public void GameInfoClick()
    {
         _audioSource.Play();
         SceneManager.LoadScene("GameInfo");
    }
    
    public void DevelopersClick()
    {
         _audioSource.Play();
         SceneManager.LoadScene("Developers");
    }
    
    public void CreditsClick()
    {
         _audioSource.Play();
         SceneManager.LoadScene("Credits");
    }
    
    public void GameObjective()
    {
         _audioSource.Play();
         SceneManager.LoadScene("GameObjective");
    }
    
    public void HomeClick()
    {
         _audioSource.Play();
         SceneManager.LoadScene("Menu");
    }
    
    public void HomeGameInfoClick()
    {
         _audioSource.Play();
         SceneManager.LoadScene("GameInfo");
    }

    
    public void QuitGame()
    {
         PlayerPrefs.DeleteKey("playerName");
         PlayerPrefs.DeleteKey("score");
         PlayerPrefs.DeleteKey("level");
         PlayerPrefs.DeleteKey("life");
         _audioSource.Play();
          #if UNITY_EDITOR
                   UnityEditor.EditorApplication.isPlaying = false;
          #else
                 Application.Quit();
          #endif
    }
}