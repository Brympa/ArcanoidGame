using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int Lives = 3;
    public int Level = 1;
    public bool IsWindowOpen = false;

    public TMP_Text LivesText;
    public TMP_Text PlayerScore;
    public TMP_Text CurrentLevel;
    public Transform BallSpawnPosition;

    public GameObject Ball;
    public GameObject[] LevelPrefabs;
    public GameObject SettingsWindowPrefab;
    public GameObject LoseWindowPrefab;
    public GameObject WinWindowPrefab;
    public GameObject EndGameWindowPrefab;
    public GameObject GuideWindowPrefab;

    private int _score;
    private int _totalBricks;
    private int _destroyedBricks;
    private int _currentLevelIndex = 0;
    
    private BallController _ballController;
    private GameObject _currentLevelInstance;
    private GameObject _settingsWindowInstance;
    private GameObject _loseWindowInstance;
    private GameObject _winWindowInstance;
    private GameObject _EndGameWindowInstance;
    private GameObject _guideWindowInstance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Time.timeScale = 1;
        _ballController = FindObjectOfType<BallController>();
        LoadLevel(_currentLevelIndex);
        UpdateLivesText();
        UpdateScoreText();
        CountTotalBricks();
        UpdateLevelText();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleSettingsWindow();
        }
    }

    private void ToggleSettingsWindow()
    {
        if (!IsWindowOpen)
        {
            IsWindowOpen = true;
        }
        else
        {
            IsWindowOpen = false;
        }

        if (_settingsWindowInstance == null)
        {
            _settingsWindowInstance = Instantiate(SettingsWindowPrefab);
            _settingsWindowInstance.transform.SetParent(null, false);
            _settingsWindowInstance.SetActive(false);
        }
        bool isActive = _settingsWindowInstance.activeSelf;
        _settingsWindowInstance.SetActive(!isActive);
        Time.timeScale = _settingsWindowInstance.activeSelf ? 0 : 1;
    }

    private void ToggleLoseWindow()
    {
        if (!IsWindowOpen)
        {
            IsWindowOpen = true;
        }
        else
        {
            IsWindowOpen = false;
        }

        if (_loseWindowInstance == null)
        {
            _loseWindowInstance = Instantiate(LoseWindowPrefab);
            _loseWindowInstance.transform.SetParent(null, false);
            _loseWindowInstance.SetActive(false);
        }
        bool isActive = _loseWindowInstance.activeSelf;
        _loseWindowInstance.SetActive(!isActive);
        Time.timeScale = _loseWindowInstance.activeSelf ? 0 : 1;
    }

    private void ToggleWinWindow()
    {
        if (!IsWindowOpen)
        {
            IsWindowOpen = true;
        }
        else
        {
            IsWindowOpen = false;
        }

        if (_winWindowInstance == null)
        {
            _winWindowInstance = Instantiate(WinWindowPrefab);
            _winWindowInstance.transform.SetParent(null, false);
            _winWindowInstance.SetActive(false);
        }
        bool isActive = _winWindowInstance.activeSelf;
        _winWindowInstance.SetActive(!isActive);
        Time.timeScale = _winWindowInstance.activeSelf ? 0 : 1;
    }
    private void ToggleEndGameWindow()
    {
        if (!IsWindowOpen)
        {
            IsWindowOpen = true;
        }
        else
        {
            IsWindowOpen = false;
        }

        if (_EndGameWindowInstance == null)
        {
            _EndGameWindowInstance = Instantiate(EndGameWindowPrefab);
            _EndGameWindowInstance.transform.SetParent(null, false);
            _EndGameWindowInstance.SetActive(false);
        }
        bool isActive = _EndGameWindowInstance.activeSelf;
        _EndGameWindowInstance.SetActive(!isActive);
        Time.timeScale = _EndGameWindowInstance.activeSelf ? 0 : 1;
    }

    public void TogglegGuideWindow()
    {
        if (!IsWindowOpen)
        {
            IsWindowOpen = true;
        }
        else
        {
            IsWindowOpen = false;
        }

        if (_guideWindowInstance == null)
        {
            _guideWindowInstance = Instantiate(GuideWindowPrefab);
            _guideWindowInstance.transform.SetParent(null, false);
            _guideWindowInstance.SetActive(false);
        }
        bool isActive = _guideWindowInstance.activeSelf;
        _guideWindowInstance.SetActive(!isActive);
        Time.timeScale = _guideWindowInstance.activeSelf ? 0 : 1;
    }

    private void CountTotalBricks()
    {
        _totalBricks = FindObjectsOfType<Brick>().Length;
    }

    public void AddScore(int points)
    {
        _score += points;
        UpdateScoreText();
        _destroyedBricks++;

        if (_destroyedBricks >= _totalBricks)
        {
            LevelComplete();
        }
    }

    private void UpdateScoreText()
    {
        PlayerScore.text = _score.ToString();
    }

    private void UpdateLevelText()
    {
        CurrentLevel.text = "Текущий уровень: " + Level;
    }

    public void LoseLife()
    {
        Lives--;
        UpdateLivesText();

        if (Lives <= 0)
        {
            GameOver();
        }
    }

    private void LoadLevel(int levelIndex)
    {
        _ballController.ConnectToPlatform();
        if (_currentLevelInstance != null)
        {
            Destroy(_currentLevelInstance);
        }

        if (levelIndex < LevelPrefabs.Length)
        {
            _currentLevelInstance = Instantiate(LevelPrefabs[levelIndex]);
            CountTotalBricks();
            if (_winWindowInstance != null)
            {
                ToggleWinWindow();
            }
        }
        else
        {
            ToggleEndGameWindow();
        }
    }

    private void LevelComplete()
    {
        ToggleWinWindow();
    }
    public void LoadNextLevel()
    {
        _currentLevelIndex++;
        if (_currentLevelIndex < LevelPrefabs.Length)
        {
            LoadLevel(_currentLevelIndex);
        }
        else
        {
            ToggleEndGameWindow();
        }

    }

    public void UpdateLivesText()
    {
        LivesText.text = Lives.ToString();
    }

    public void GameOver()
    {
        ToggleLoseWindow();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}