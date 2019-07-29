using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private GameObject _EnemyPrefab;
    [SerializeField]
    private GameObject _EnemyExplosionPrefab;
    private float _speed = 5f;
    private float _enemyRespawnTime = 5f;
    private float _enemyRespawnRate = 5f;
    private int _enemyPrice = 10;
    private UIManager _uiManager;
    private Animator _anim;
    private AudioSource _audio;
    [SerializeField]
    private GameObject _laserShotPrefab;
    private GameManager _gameManager;
    

    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _anim = GetComponent<Animator>();
        if(_anim == null)
        {
            Debug.Log("Animator is null");
        }
        _audio = GetComponent<AudioSource>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(EnemyShootingRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();
    }
    private void Shooting()
    {

    }
    private void EnemyMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y < -6.0f)
        {
            transform.position = new Vector3(Random.Range(-7.0f, +7.0f), 7f, 0f);
        }
    }
    private void OnTriggerEnter2D(Collider2D some)
    {
        if (some.tag == "Laser")
        {
            _uiManager.UpdateScore(_enemyPrice);

            Instantiate(_EnemyExplosionPrefab, transform.position, Quaternion.identity);
            
            Destroy(this.gameObject);
            Destroy(some.gameObject);
        }
        else if (some.tag == "Player")
        {
            Player player = some.GetComponent<Player>();
            if (player != null)
            {
                _uiManager.UpdateScore(_enemyPrice);
                player.PlayerDamage();
            }
            Instantiate(_EnemyExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if (some.tag == "Shield")
        {
           
            Collider2D shield = some.GetComponentInChildren<Collider2D>();
            
            if (shield != null)
            {
                _uiManager.UpdateScore(_enemyPrice);
            }
            Instantiate(_EnemyExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
    IEnumerator EnemyShootingRoutine()
    {
        while (_gameManager.gameOver == false)
        {
            yield return new WaitForSeconds(Random.Range(0f,2f));
            Instantiate(_laserShotPrefab, transform.position + new Vector3(0, -1.5f, 0), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3f, 7f));
        }
    }
}



