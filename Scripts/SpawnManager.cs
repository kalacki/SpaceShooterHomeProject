using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyShipPrefab;
    [SerializeField]
    private GameObject[] powerups;
    [SerializeField]
    private GameManager _gameManager;
    [SerializeField]
    private GameObject _asteroidPrefab;


    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    private void Update()
    {

    }


    public void StartSpawning()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerUpSpawnRoutine());
        StartCoroutine(AsteroidSpawnRoutine());
    }
    public void StopSpawning()
    {
        StopAllCoroutines();
    }

    //create a coroutine every 5 sec
    IEnumerator EnemySpawnRoutine()
    {
        while (_gameManager.gameOver == false)
        {
            Instantiate(enemyShipPrefab, new Vector3(Random.Range(-7f, 7f), 7f, 0f), Quaternion.identity);
            yield return new WaitForSeconds(5f);
        }
    }
    IEnumerator PowerUpSpawnRoutine()
    {
        while (_gameManager.gameOver == false)
        {
            int randomPowerup = Random.Range(0, 3);
            Instantiate(powerups[randomPowerup], new Vector3(Random.Range(-7f, 7f), 7f, 0f), Quaternion.identity);
            yield return new WaitForSeconds(5f);
        }
    }
    IEnumerator AsteroidSpawnRoutine()
    {
        while (_gameManager.gameOver == false)
        {
            yield return new WaitForSeconds(5f);
            Instantiate(_asteroidPrefab, new Vector3(Random.Range(-7f, 7f), 7f, 0f), Quaternion.identity);
            yield return new WaitForSeconds(5f);
        }
    }
    //void Start()
    //{

    //    StartCoroutine(EnemySpawnRoutine());
    //    StartCoroutine(PowerUpSpawnRoutine());
    //}

}


