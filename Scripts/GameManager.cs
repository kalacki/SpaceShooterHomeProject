using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    private SpawnManager _spawnManager;
    public bool gameOver = true;

    private UIManager _uiManager;
    private void Start()
    {
        _uiManager = GameObject.Find("Canvas")?.GetComponent<UIManager>();
        //player = GameObject.Find("Player")?.GetComponent<GameObject>();
        _spawnManager = GameObject.Find("Spawn_Manager")?.GetComponent<SpawnManager>();
        gameOver = false;
        Instantiate(player, Vector3.zero, Quaternion.identity);
    }

    void Update()
    {
        if (gameOver == true)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {                
                Instantiate(player, Vector3.zero, Quaternion.identity);
                //_spawnManager.StartSpawning();
                gameOver = false;
                _uiManager.HideTitleScreen();
                _uiManager.GameOverTextMessageTurnOff();
                _uiManager.ResetScore();
            }
        }
    }

}
