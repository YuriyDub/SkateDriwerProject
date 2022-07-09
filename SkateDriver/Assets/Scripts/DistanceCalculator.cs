using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DistanceCalculator : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image _distanceBar;

    [Header("Transforms")]
    [SerializeField] private Transform _finishTransform;
    [SerializeField] private Transform _playerTransform;

    public float f;

    private float _startPosition, _currentPosition,_finishPosition;

    void Start()
    {
        _startPosition = _playerTransform.position.z;
        _finishPosition = _finishTransform.position.z;
    }

    void Update()
    {
        _currentPosition = _playerTransform.position.z;

        _distanceBar.fillAmount = Mathf.Clamp01((_currentPosition - _startPosition)/(_finishPosition - _startPosition));
        f = (_currentPosition - _startPosition) / (_finishPosition - _startPosition);
    }
}
