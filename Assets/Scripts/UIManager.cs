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


        DontDestroyOnLoad(this.gameObject);
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
    private void Start()
    {
      
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            bool isPaused = pauseScreen.activeInHierarchy;
            GameEvents.TriggerPause(!isPaused);
            
        }
       
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Cena carregada, reconfigurando referências...");

        if (GameManager.Instance != null && GameManager.Instance.GameData != null)
        {
            int score = GameManager.Instance.GameData.GetScore();
            Debug.Log("Atualizando UI com score: " + score);

            if (textScore != null)
                textScore.text = score.ToString("0");
        }
        else
        {
            Debug.LogWarning("GameManager ou GameData não estão prontos ao carregar cena.");
        }

        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
        winScreen.SetActive(false);
        
        Time.timeScale = 1f;

        if (backgroundSound != null && !backgroundSound.isPlaying)
            backgroundSound.Play();
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
        
        if (SoundManager.instance != null) SoundManager.instance.PlaySound(winSound);
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
        backgroundSound?.Stop();

    }
    #endregion

    #region Options
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
        
    }

    public void Quit()
    {
        Application.Quit();
    }
    #endregion

    #region Volume
    public void SoundVolume()
    {
        SoundManager.instance.ChangeSoundVolume(0.2f);
    }

    public void MusicVolume()
    {
        SoundManager.instance.ChangeMusicVolume(0.2f);
    }
    #endregion
    public void ResetScoreManualmente()
    {
        GameManager.Instance.GameData.ResetScore();
        GameManager.Instance.GameData.Save();
        textScore.text = "0";
    }

    public void OnTheEnd()
    {

    }
}








