using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float _verticalSpeed, _horizontalSpeed;
    [SerializeField] [Range(0, 10)] private float _movementSmoothness;

    [Header("Limits")]
    [SerializeField] private float _maxHeight, _minHeight;

    [Header("Components")]
    private Rigidbody _rigidbody;
    private Transform _transform;
    private TrackManager _trackManager;

    [Header("InputValues")]
    [SerializeField] private float _moveInput;

    [Header("Controls")]
    private Controls _controls;

    [Header("GameObjects")]
    [SerializeField] private GameObject _cameraTarget;
    [SerializeField] private GameObject _ragdoll;

    [Header("Vectors")]
    [SerializeField] private Vector3 _targetOffset;

    [Header("Bools")]
    public bool _inCheckpoint;
    public bool _isAlive = true;

    private void Awake()
    {
        _controls = new Controls();
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
        _trackManager = GetComponent<TrackManager>();
    }
    private void Update()
    {
        PlayerLose();
    }
    private void FixedUpdate()
    {
        Move();
        TargetMove();
    }
    private void TargetMove()
    {
        _cameraTarget.transform.position = _transform.position + _targetOffset;
    }
    private void Move()
    {
        if (_inCheckpoint)
        {
            _rigidbody.useGravity = true;
        }
        else
        {
            _rigidbody.useGravity = false;


            _moveInput = _controls.Player.Move.ReadValue<float>();

            _rigidbody.velocity =  Vector3.Lerp(_rigidbody.velocity, new Vector3(_rigidbody.velocity.x, _verticalSpeed * _moveInput, _rigidbody.velocity.z),Time.deltaTime* _movementSmoothness);

            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y, _horizontalSpeed);

            if (_transform.position.y < _minHeight)
            {
                _transform.position = new Vector3(_transform.position.x, _minHeight, _transform.position.z);
            }
            else if (_transform.position.y > _maxHeight)
            {
                _transform.position = new Vector3(_transform.position.x, _maxHeight, _transform.position.z);
            }

            _transform.forward = _rigidbody.velocity.normalized;
        }
    }

    private void PlayerLose()
    {
        if (!_isAlive || _trackManager.boardCount <= 0)
        {
            _ragdoll.SetActive(true);
            gameObject.SetActive(false);

            Invoke("RestartLevel", 2);
        }
    }

    public void LoadNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex <= SceneManager.sceneCount - 1)
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1); 
        }
        else
        {
            SceneManager.LoadSceneAsync(0);
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            _isAlive = false;
        }
        else if (other.CompareTag("Finish"))
        {
            Invoke("LoadNextLevel", 2);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("CheckPoint"))
        {
            _inCheckpoint = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CheckPoint"))
        {
            _inCheckpoint = false;
        }
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
