using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackManager : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float _spawnRate;
    [SerializeField] private float _boardCosts;

    [Header("Components")]
    private Rigidbody _rigidbody;
    private Transform _transform;

    [Header("GameObjects")]
    [SerializeField] private GameObject _spawnPoint;
    [SerializeField] private GameObject _board;
    [SerializeField] private GameObject _backpack;

    [Header("Info")]
    public float boardCount;

    [Header("Bools")]
    public bool isStart;
    public bool inCheckpoint;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _transform = GetComponent<Transform>();
    }

    void Update()
    {
        SpawnBoard();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Board")) 
        {
            boardCount += _boardCosts;
            Destroy(other.gameObject);

            _backpack.transform.localScale = new Vector3(_backpack.transform.localScale.x, _backpack.transform.localScale.y + _boardCosts * 0.01f, _backpack.transform.localScale.z);
            _backpack.transform.localPosition = new Vector3(_backpack.transform.localPosition.x, _backpack.transform.localPosition.y + _boardCosts * 0.005f, _backpack.transform.localPosition.z);
        }
        else if (other.CompareTag("CheckPoint"))
        {
            inCheckpoint = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("CheckPoint"))
        {
            isStart = false;
            inCheckpoint = false;

            StopAllCoroutines();
        }
    }

    private void SpawnBoard()
    {
        if(isStart == false)
        {
            StartCoroutine(SpawnBoardForSecond(_spawnRate));
            isStart = true;
        }
    }

    IEnumerator SpawnBoardForSecond(float spawnRate)
    {
        while(true) 
        {
            if (boardCount > 0 && inCheckpoint == false)
            {
                yield return new WaitForSeconds(spawnRate);
                Instantiate(_board, _spawnPoint.transform.position, _transform.rotation);
                boardCount--;
                _backpack.transform.localScale = new Vector3(_backpack.transform.localScale.x, _backpack.transform.localScale.y - 0.01f, _backpack.transform.localScale.z);
                _backpack.transform.localPosition = new Vector3(_backpack.transform.localPosition.x, _backpack.transform.localPosition.y - 0.005f, _backpack.transform.localPosition.z);                  
            }
            else break;
        }
        yield break;
    }


}
