using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehaviour : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    private int _asteroidPrice = 50;
    private UIManager _uiManager;
    [SerializeField]
    private GameObject _AsteroidExplosionPrefab;
    private SpawnManager _spawnManager;
    int _xRandom;
    // Start is called before the first frame update
    
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        if(_uiManager == null)
        {
            Debug.Log("UI Manager is NULL!"); // just for check
        }
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        
    }
    private void Awake()
    {
        _xRandom = Random.Range(-5, 5);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        
    }

    public void OnTriggerEnter2D (Collider2D other)
    {
        if(other.tag == "Laser")
        {
            _uiManager.UpdateScore(_asteroidPrice);
            //_spawnManager.StartSpawning();
            Instantiate(_AsteroidExplosionPrefab, transform.position, Quaternion.identity);
            
            Destroy(this.gameObject);
            Destroy(other.gameObject);  
            
        }
        else if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                _uiManager.UpdateScore(_asteroidPrice);
                player.PlayerDamage();
            }
            Instantiate(_AsteroidExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        else if (other.tag == "Shield")
        {

            Collider2D shield = other.GetComponentInChildren<Collider2D>();

            if (shield != null)
            {
                _uiManager.UpdateScore(_asteroidPrice);
            }
            Instantiate(_AsteroidExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        
    }
    public void Movement()
    {
        
        transform.Rotate(0, 0, 1 * _speed, Space.World);
        
        transform.position += new Vector3(_xRandom * Time.deltaTime, -1 * Time.deltaTime * _speed, 0);
        if (transform.position.y <= -7)
        {
            Destroy(this.gameObject);
        }
    }
}
