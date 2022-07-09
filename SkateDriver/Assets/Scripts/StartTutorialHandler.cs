using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartTutorialHandler : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TrackManager _trackManager;

    [Header("UiElement")]
    [SerializeField] private GameObject _armImage;
    [SerializeField] private GameObject _overlayImage;

    [Header("Controls")]
    private Controls _controls;
    private void Awake()
    {
        Time.timeScale = 0f;

        _trackManager.isStart = true;

        _controls = new Controls();

        _controls.Player.Tap.performed += context => HideOverlay();
    }

    private void HideOverlay()
    {
        _armImage.SetActive(false);
        _overlayImage.SetActive(false);

        Time.timeScale = 1f;

        _trackManager.isStart = false;
    }

    private void OnEnable()
    {
        _controls.Enable();
    }

    private void OnDisable()
    {
        _controls.Disable();
    }
}
