using Lean.Gui;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndGameWindow : MonoBehaviour
{
    [SerializeField] private LeanButton _restartButton;
    [SerializeField] private LeanButton _exitGameButton;
    [SerializeField] private TMP_Text _currentPlayerScore;

    private GameManager _gameManager;

    private void Awake()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _currentPlayerScore.text = _gameManager.PlayerScore.text.ToString();
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
