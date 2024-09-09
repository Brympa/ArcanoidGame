using Lean.Gui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideWindow : MonoBehaviour
{
    [SerializeField] private LeanButton _backButton;

    private void OnEnable()
    {
        _backButton.OnClick.AddListener(BindOnBackButton);
    }

    private void OnDisable()
    {
        _backButton.OnClick.RemoveListener(BindOnBackButton);
    }

    private void BindOnBackButton()
    {
        Destroy(gameObject);
    }
}
