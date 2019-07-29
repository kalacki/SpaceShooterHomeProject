using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Sprite[] lives;
    [SerializeField]
    private Image livesImageDisplay;
    [SerializeField]
    private GameObject TitleScreen;
    [SerializeField]
    private Text GameOverText;
    public int Score;
    [SerializeField]
    private Text textScoreDisplay;
    Coroutine gameOverTextRoutine = null;
    public void UpdateLives(int currentLives)
    {
        Debug.Log("Player lives: " + currentLives);
        livesImageDisplay.sprite = lives[currentLives];
    }

    public void UpdateScore(int currentScore)
    {
        Score += currentScore;

        textScoreDisplay.text = "Score: " + Score;
        Debug.Log("Player score: " + currentScore);
        //textScoreDisplay.text = Convert.ToString(currentScore);
    }
    public void ShowTitleScreen()
    {
        TitleScreen.SetActive(true);
        Score = 0;
        textScoreDisplay.text = "Score: " + Score;
    }
    public void HideTitleScreen()
    {
        TitleScreen.SetActive(false);
        
    }
    public void GameOverTextMessageTurnOn()
    {
        Debug.Log("I want to say GAME OVER");
        //GameOverText.gameObject.SetActive(true);        
        gameOverTextRoutine = StartCoroutine(GameOverTextRoutine());
    }
    public void GameOverTextMessageTurnOff()
    {
        Debug.Log("I want to say GAME OVER");
        GameOverText.gameObject.SetActive(false);
        StopAllCoroutines();
    }
    IEnumerator GameOverTextRoutine()
    {
        while (true)
        {
            GameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
            GameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(1f);
        }
    }
    public void ResetScore()
    {
        Score = 0;
        textScoreDisplay.text = "Score: " + Score;
    }



    //public void TitleScreenTurnOff()
    //{
    //    Debug.Log("Game started!");
    //    _isGameStarted = FindObjectOfType<StartGame>();
    //    _isGameStarted.GameStart();
    //    TitleScreen.enabled = !TitleScreen.enabled;
    //}
    //public void TitleScreenTurnON()
    //{
    //    Debug.Log("Game ended!");
    //    _isGameStarted = FindObjectOfType<StartGame>();
    //    TitleScreen.enabled = TitleScreen.enabled;
    //    _isGameStarted.GameOver();

    //}
}
