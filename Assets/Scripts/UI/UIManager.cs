using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private AudioSource backgroundSound;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject endPortals;
    [SerializeField] private Text textScore;

    private int score;

    private void Awake()
    {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
        endPortals.SetActive(true);
    }
    #region GameOver
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        SoundManager.instance.PlaySound(gameOverSound);
        Time.timeScale = 0f;
        backgroundSound.Stop();
    }

    public void Winner()
    {
        winScreen.SetActive(true);
        endPortals.SetActive(false);
        SoundManager.instance.PlaySound(winSound);
        Time.timeScale = 0f;
        backgroundSound.Stop();
        Debug.Log("Win");
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }
    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
        
    }
    #endregion
    public void AddPoints(int points)
    {
        score += points;
        textScore.text = score.ToString(" 0");
    }
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            // If pause screen already active unpause and vice-versa
            if( pauseScreen.activeInHierarchy)
            {
                PauseGame(false);    
            }
            else
            {
                PauseGame(true);
            }
        }
    }
    #region Pause
   public void PauseGame(bool status)
    {
        pauseScreen.SetActive(status);
        if( status )
        {
            Time.timeScale = 0f;
            endPortals.SetActive(false);
        }
        else
        {
            Time.timeScale = 1f;
            endPortals.SetActive(true);
        }

      
        
    }
    #endregion
    public void SoundVolume()
    {
        SoundManager.instance.ChangeSoundVolume(0.2f);
    }

    public void MusicVolume()
    {
        SoundManager.instance.ChangeMusicVolume(0.2f);
    }
}

