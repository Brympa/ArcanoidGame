using Lean.Gui;
using TMPro;
using UnityEngine;

public class LoseWindow : MonoBehaviour
{
    public TMP_Text _totalScore;

    [SerializeField] private LeanButton _restartButton;
    [SerializeField] private LeanButton _exitGameButton;

    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _totalScore = _gameManager.PlayerScore;
    }

    private void OnEnable()
    {
        _restartButton.OnClick.AddListener(BindOnRestartButton);
        _exitGameButton.OnClick.AddListener(BindOnExitButton);
    }

    private void OnDisable()
    {
        _restartButton.OnClick.RemoveListener(BindOnRestartButton);
        _exitGameButton.OnClick.RemoveListener(BindOnExitButton);
    }

    private void BindOnRestartButton()
    {
        _gameManager.RestartGame();
    }

    private void BindOnExitButton()
    {
        Application.Quit();
    }
}
