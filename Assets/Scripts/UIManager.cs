using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IGameEventsListener
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private AudioSource backgroundSound;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject endPortals;
    [SerializeField] private Text textScore;

    private GameData gameData = new GameData();

    private int score;
    public static UIManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
           
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        if (FindObjectsOfType<EventSystem>().Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
        endPortals.SetActive(true);
    }

    private void OnEnable()
    {
        GameEvents.OnGameOver += OnGameOver;
        GameEvents.OnWin += OnWin;
        GameEvents.OnAddPoints += OnAddPoints;
        GameEvents.OnPause += OnPause;
    }

    private void OnDisable()
    {
        GameEvents.OnGameOver -= OnGameOver;
        GameEvents.OnWin -= OnWin;
        GameEvents.OnAddPoints -= OnAddPoints;
        GameEvents.OnPause -= OnPause;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            bool isPaused = pauseScreen.activeInHierarchy;
            GameEvents.TriggerPause(!isPaused);
        }
    }

    #region GameOver
    public void OnGameOver()
    {
        gameOverScreen.SetActive(true);
        if (SoundManager.instance != null) SoundManager.instance.PlaySound(gameOverSound);
        Time.timeScale = 0f;
        backgroundSound?.Stop();
    }

    public void OnWin()
    {
        winScreen.SetActive(true);
        endPortals.SetActive(false);
        if (SoundManager.instance != null) SoundManager.instance.PlaySound(winSound);
        Time.timeScale = 0f;
        backgroundSound?.Stop();
        Debug.Log("Win");
    }
    #endregion

    #region Score
    public void OnAddPoints(int points)
    {
        score += points;
        textScore.text = score.ToString("0");
        if (textScore != null)
            textScore.text = score.ToString("0");
    }
    #endregion

    #region Pause
    public void OnPause(bool status)
    {
        pauseScreen.SetActive(status);
        Time.timeScale = status ? 0f : 1f;
        endPortals.SetActive(!status);
    }
    #endregion

    public void Restart()
    {
        score = 0;
        textScore.text = "0";
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        score = 0;
        textScore.text = "0";
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}




