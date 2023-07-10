
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    private List<PlayerData> _players;
    private int _score;
    private int _targetScore;
    private int _life;
    private int _level;
    private float _timer;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text lifeText;
    [SerializeField] private Text scoreYLifeText;
    [SerializeField] private Button pauseButton;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Text defeatText;
    [SerializeField] private GameObject defeatMenu;
    private AudioSource _audioSource;
    private DataPersistManager _persistManager;
   
    
    void Start()
    {
        _persistManager = FindObjectOfType<DataPersistManager>();
        _audioSource = GetComponent<AudioSource>();
        pauseButton.gameObject.SetActive(true);
        Time.timeScale = 1f;
        _score = PlayerPrefs.GetInt("score");
        _life = 10;
        scoreText.text = "score: " +_score.ToString();
        lifeText.text = "life:    " +_life.ToString();
        PlayerPrefs.SetInt("life", _life);
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= 5f)
        {
            _score += 1;
            PlayerPrefs.SetInt("score", _score);
            scoreText.text = "score: " + _score.ToString();
            _timer = 0;
        }
        _life = PlayerPrefs.GetInt("life");
        lifeText.text = "life:    " +_life.ToString();
        
        if (_life < 0)
        {
            Time.timeScale = 0f;
            pauseButton.gameObject.SetActive(false);
            _life = 0;
            _score = PlayerPrefs.GetInt("score");
            _level = PlayerPrefs.GetInt("level");
            string loadedPlayerName = PlayerPrefs.GetString("playerName");
            _persistManager.UpdatePlayerScore(_score, loadedPlayerName,"playerData.json");
            _persistManager.UpdatePlayerLevel(_level,loadedPlayerName, "playerData.json");
            defeatText.text = "Score: " +_score.ToString() + "\n"+
                                  "Life: " +_life.ToString();
            _score = LoadScore();
            defeatMenu.SetActive(true);
            
        }
    }
    
    public void PauseGame()
    {
        _audioSource.Play();
        Time.timeScale = 0f;
        pauseButton.gameObject.SetActive(false);
        _life = PlayerPrefs.GetInt("life");
        _score = PlayerPrefs.GetInt("score");
        scoreYLifeText.text = "Score: " +_score.ToString() + "\n"+
                              "Life: " +_life.ToString();
        _level = PlayerPrefs.GetInt("level");
        string loadedPlayerName = PlayerPrefs.GetString("playerName");
        _persistManager.UpdatePlayerScore(_score, loadedPlayerName,"playerData.json");
        _persistManager.UpdatePlayerLevel(_level,loadedPlayerName, "playerData.json");
        pauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        _audioSource.Play();
        Time.timeScale = 1f;
        pauseButton.gameObject.SetActive(true);
        pauseMenu.SetActive(false);
    }
    
    public void PlayAgain()
    {
        _audioSource.Play();
        Time.timeScale = 1f;
        pauseButton.gameObject.SetActive(true);
        defeatMenu.SetActive(false);
        
        PlayerPrefs.SetInt("life", 10);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public int LoadScore()
    {
        if (_level ==1)
        {
            return 1200;
        }
        else if (_level ==2)
        {
            return 3000;
        }
        else if (_level == 3)
        {
            return 6200;
        }
        else if (_level == 4)
        {
            return 9100;
        }
        else 
        {
            return 11000;
        }
    }
    
    public void HomeMenu()
    {
        _audioSource.Play();
        SceneManager.LoadScene("Menu");
    }
}

