using Lean.Gui;
using TMPro;
using UnityEngine;

public class WinWindow : MonoBehaviour
{
    [SerializeField] private LeanButton _nextLevelButton;
    [SerializeField] private LeanButton _exitGameButton;
    [SerializeField] private TMP_Text _currentLevelText;
    [SerializeField] private TMP_Text _currentPlayerScore;

    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _currentLevelText.text = "Уровень " + _gameManager.Level + " завершён";
        _currentPlayerScore.text = _gameManager.PlayerScore.text.ToString();

    }

    private void OnEnable()
    {
        _nextLevelButton.OnClick.AddListener(BindOnNextLevelButton);
        _exitGameButton.OnClick.AddListener(BindOnExitButton);
    }

    private void OnDisable()
    {
        _nextLevelButton.OnClick.RemoveListener(BindOnNextLevelButton);
        _exitGameButton.OnClick.RemoveListener(BindOnExitButton);
    }

    private void BindOnNextLevelButton()
    {
        _gameManager.LoadNextLevel();
    }

    private void BindOnExitButton()
    {
        Application.Quit();
    }
}
