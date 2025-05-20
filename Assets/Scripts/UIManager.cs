using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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

        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        GameEvents.OnGameOver += OnGameOver;
        GameEvents.OnWin += OnWin;
        GameEvents.OnAddPoints += OnAddPoints;
        GameEvents.OnPause += OnPause;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        GameEvents.OnGameOver -= OnGameOver;
        GameEvents.OnWin -= OnWin;
        GameEvents.OnAddPoints -= OnAddPoints;
        GameEvents.OnPause -= OnPause;

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            bool isPaused = pauseScreen.activeInHierarchy;
            GameEvents.TriggerPause(!isPaused);
            endPortals.SetActive(false);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Cena carregada, reconfigurando referências...");

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            var health = player.GetComponent<Health>();
            if (health != null)
            {
                health.ResetHealth();

                var healthBar = FindObjectOfType<HealthBar>();
                if (healthBar != null)
                {
                    healthBar.SetTarget(health);
                }
            }
        }

        if (textScore != null)
            textScore.text = GameManager.Instance.GameData.GetScore().ToString("0");

        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
        winScreen.SetActive(false);
        endPortals.SetActive(false);
        Time.timeScale = 1f;

        if (backgroundSound != null && !backgroundSound.isPlaying)
            backgroundSound.Play();
    }

    #region GameOver
    public void OnGameOver()
    {
        gameOverScreen.SetActive(true);
        if (SoundManager.Instance != null) SoundManager.Instance.PlaySound(gameOverSound);
        Time.timeScale = 0f;
        backgroundSound?.Stop();
    }

    public void OnWin()
    {
        winScreen.SetActive(true);
        endPortals.SetActive(false);
        if (SoundManager.Instance != null) SoundManager.Instance.PlaySound(winSound);
        Time.timeScale = 0f;
        backgroundSound?.Stop();
        Debug.Log("Win");
    }
    #endregion

    #region Score
    public void OnAddPoints(int points)
    {
        GameManager.Instance.GameData.AddPoints(points);
        if (textScore != null)
            textScore.text = GameManager.Instance.GameData.GetScore().ToString("0");
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
        GameManager.Instance.GameData.ResetScore();
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        GameManager.Instance.GameData.ResetScore();
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
        endPortals.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ResetScoreManualmente()
    {
        GameManager.Instance.GameData.ResetScore();
        GameManager.Instance.GameData.Save();
        textScore.text = "0";
    }
}








