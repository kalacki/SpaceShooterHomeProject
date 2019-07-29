using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    //public or private
    // data type (int, floats, bool,  strings)
    // every variable has a NAME
    // option value assigned
    [SerializeField]
    private GameObject _playerExplosionPrefab;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShootPrefab;
    [SerializeField]
    private GameObject _shieldGameObject;
    [SerializeField]
    private float _fireRate = 0.25f;
    [SerializeField]
    private float _canFire = 0.0f;
    [SerializeField]
    private GameObject[] _damageStates;

    private UIManager _uiManager;
    private EnemyAI eachEnemy;
    private GameManager _gameManager;
    private SpawnManager _spawnManager;
    private Animator _anim;


    [SerializeField] private float _speed = 5.0f;

    public int _healthBar = 3;
    public int _currentScore = 0;

    public bool canTripleShot = false;
    public bool canSpeedUp = false;
    public bool shieldUpOn = false;

    public bool CanSpeedUp
    {
        get
        {
            return canSpeedUp;
        }
        set
        {
            canSpeedUp = value;
        }
    }


    private void Start()
    {
        //Debug.Log(transform.position);
        transform.position = new Vector3(0, -4, 0);

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _anim = GetComponent<Animator>();

        if (_uiManager != null)
        {
            _uiManager.UpdateLives(_healthBar);

        }
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if (_spawnManager != null)
        {
            _spawnManager.StartSpawning();
        }

    }

    private void Update()
    {
        Movement();
#if UNITY_ANDROID
        if (CrossPlatformInputManager.GetButtonDown("Fire") || Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

#elif UNITY_IOS
if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
#else
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
#endif
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
    private void Shoot()
    {
        if (Time.time > _canFire)
        {
            if (canTripleShot == true)
            {
                Instantiate(_tripleShootPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
            }
            _canFire = Time.time + _fireRate;
        }
    }


    private void Movement()
    {

        float horizontalInput = CrossPlatformInputManager.GetAxis("Horizontal");
        float verticalInput = CrossPlatformInputManager.GetAxis("Vertical");

       // float horizontalInput = Input.GetAxis("Horizontal");
       // float verticalInput = Input.GetAxis("Vertical");



        if (canSpeedUp == true)
        {
            transform.Translate(Vector3.right * 2.5f * _speed * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * 2.5f * _speed * verticalInput * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
            transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);
        }
        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        if (transform.position.y < -4.2)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }
        if (transform.position.x > 9.5)
        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        }
        if (transform.position.x < -9.5)
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        }
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _anim.SetBool("LeftTurn", true);
            _anim.SetBool("RightTurn", false);
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            _anim.SetBool("LeftTurn", false);
            _anim.SetBool("RightTurn", false);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            _anim.SetBool("LeftTurn", false);
            _anim.SetBool("RightTurn", true);
        }
        else if(Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            _anim.SetBool("LeftTurn", false);
            _anim.SetBool("RightTurn", false);
        }
    }
    public void SpeedPowerUpOn()
    {
        canSpeedUp = true;
        StartCoroutine(SpeedPowerUpRoutine());
    }
    public void TripleShotPowerupOn()
    {
        canTripleShot = true;

        StartCoroutine(TripleShotPowerDownRoutine());
    }
    public void ShieldPowerupOn()
    {
        shieldUpOn = true;
        _shieldGameObject.SetActive(true);

        StartCoroutine(ShieldPowerUpRoutine());
    }
    public void PlayerDamage()
    {
        if (shieldUpOn == true)
        {
            return;
        }
        else
        {

            _healthBar--;
            _uiManager.UpdateLives(_healthBar);
            if(_healthBar == 2)
            {
                _damageStates[Random.Range(0,2)].SetActive(true);
            }
            if (_healthBar == 1)
            {
                if (_damageStates[0].activeInHierarchy == true)
                    _damageStates[1].SetActive(true);
                else
                    _damageStates[0].SetActive(true);
            }

            if (_healthBar == 0)
            {
                _gameManager.gameOver = true;
                //_uiManager.ShowTitleScreen();
                _uiManager.GameOverTextMessageTurnOn();
                _spawnManager.StopSpawning();
                Instantiate(_playerExplosionPrefab, transform.position, Quaternion.identity);
                //_uiManager.ResetScore();

                Destroy(this.gameObject);

            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemyLaser")
        {
            PlayerDamage();
            Debug.Log("Got this method!");
            Destroy(other.gameObject);
        }
    }


    internal IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }
    internal IEnumerator SpeedPowerUpRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canSpeedUp = false;
    }
    internal IEnumerator ShieldPowerUpRoutine()
    {
        yield return new WaitForSeconds(10.0f);
        _shieldGameObject.SetActive(false);
        shieldUpOn = false;
    }

}
