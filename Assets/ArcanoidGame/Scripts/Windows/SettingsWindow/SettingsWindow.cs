using Lean.Gui;
using UnityEngine;

public class SettingsWindow : MonoBehaviour
{
    [SerializeField] private LeanButton _restartButton;
    [SerializeField] private LeanButton _exitGameButton;
    [SerializeField] private LeanButton _guideButton;
    [SerializeField] private LeanToggle _controlToggle;
    [SerializeField] private LeanToggle _musicToggle;
    [SerializeField] private LeanToggle _ambientToggle;

    private GameManager _gameManager;
    private PlatformController _platformController;
    private MusicManager _musicManager;
    private BallController _ballController;
    private Brick _brick;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _platformController = FindObjectOfType<PlatformController>();
        _musicManager = FindObjectOfType<MusicManager>();
        _ballController = FindObjectOfType<BallController>();
        _brick = FindObjectOfType<Brick>();
    }

    private void OnEnable()
    {
        _restartButton.OnClick.AddListener(BindOnRestartButton);
        _exitGameButton.OnClick.AddListener(BindOnExitButton);
        _guideButton.OnClick.AddListener(BindOnGuideButton);
        _controlToggle.OnOn.AddListener(BindOnOnControlToggle);
        _controlToggle.OnOff.AddListener(BindOnOffControlToggle);
        _musicToggle.OnOn.AddListener(BindOnOnMusicToggle);
        _musicToggle.OnOff.AddListener(BindOnOffMusicToggle);
        _ambientToggle.OnOn.AddListener(BindOnOnAmbientToggle);
        _ambientToggle.OnOff.AddListener(BindOnOffAmbientToggle);
    }

    private void OnDisable()
    {
        _restartButton.OnClick.RemoveListener(BindOnRestartButton);
        _exitGameButton.OnClick.RemoveListener(BindOnExitButton);
        _guideButton.OnClick.RemoveListener(BindOnGuideButton);
        _controlToggle.OnOn.RemoveListener(BindOnOnControlToggle);
        _controlToggle.OnOff.RemoveListener(BindOnOffControlToggle);
        _musicToggle.OnOn.RemoveListener(BindOnOnMusicToggle);
        _musicToggle.OnOff.RemoveListener(BindOnOffMusicToggle);
        _ambientToggle.OnOn.RemoveListener(BindOnOnAmbientToggle);
        _ambientToggle.OnOff.RemoveListener(BindOnOffAmbientToggle);
    }

    private void BindOnOnControlToggle()
    {
        _platformController.IsMouseControl = true;
    }

    private void BindOnOffControlToggle()
    {
        _platformController.IsMouseControl = false;
    }

    private void BindOnOnMusicToggle()
    {
        _musicManager.PlayMusic();
    }

    private void BindOnOffMusicToggle()
    {
        _musicManager.StopMusic();
    }

    private void BindOnOnAmbientToggle()
    {
        _ballController.GetComponent<AudioSource>().enabled = true;
    }

    private void BindOnOffAmbientToggle()
    {
        _ballController.GetComponent<AudioSource>().enabled = false;
    }

    private void BindOnRestartButton()
    {
        _gameManager.RestartGame();
    }

    private void BindOnGuideButton()
    {
        _gameManager.TogglegGuideWindow();
    }

    private void BindOnExitButton()
    {
        Application.Quit();
    }
}
