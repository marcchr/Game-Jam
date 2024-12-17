using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public enum GameState
    {
        Gameplay,
        Paused,
        GameOver
    }

    public GameState currentState;
    public GameState previousState;

    [Header("UI")]
    public GameObject pauseScreen;
    public GameObject resultsScreen;

    float stopwatchTime;
    public TextMeshProUGUI stopwatchDisplay;

    public int killCount;
    public TextMeshProUGUI killCountText;

    public int currentScore;
    public TextMeshProUGUI currentScoreText;

    public TextMeshProUGUI survivalTimeText;
    public TextMeshProUGUI enemiesKilledText;

    public bool isGameOver = false;

    void Update()
    {
        UpdateStopwatch();
        UpdateKillCountText();
        UpdateCurrentScore();

        switch(currentState)
        {
            case GameState.Gameplay:
                CheckForPauseAndResume();
                break;
            case GameState.Paused:
                CheckForPauseAndResume();
                break;
            case GameState.GameOver:
                if (!isGameOver)
                {
                    isGameOver = true;
                    Time.timeScale = 0f;
                    DisplayResults();
                }
                break;
            default:
                Debug.LogWarning("STATE DOES NOT EXIST");
                break;
        }
    }

    public void AssignSurvivalTime()
    {
        survivalTimeText.text = stopwatchDisplay.text;
    }

    public void AssignEnemiesKilled()
    {
        enemiesKilledText.text = killCount.ToString();
    }

    void DisplayResults()
    {
        resultsScreen.SetActive(true);
    }

    public void GameOver()
    {
        ChangeState(GameState.GameOver);
    }
    public void PauseGame()
    {
        if (currentState != GameState.Paused)
        {
            previousState = currentState;
            ChangeState(GameState.Paused);
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);

        }
    }

    public void ResumeGame()
    {
        if (currentState == GameState.Paused)
        {
            ChangeState(previousState);
            Time.timeScale = 1f;
            pauseScreen.SetActive(false);
        }
    }

    void CheckForPauseAndResume()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Paused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }

    void UpdateStopwatch()
    {
        stopwatchTime += Time.deltaTime;
        UpdateStopwatchDisplay();
    }

    void UpdateStopwatchDisplay()
    {
        int minutes = Mathf.FloorToInt(stopwatchTime / 60);
        int seconds = Mathf.FloorToInt(stopwatchTime % 60);
        stopwatchDisplay.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void UpdateKillCountText()
    {
        killCountText.text = "Flies eaten: " + killCount.ToString();
    }

    void UpdateCurrentScore()
    {
        currentScoreText.text = currentScore.ToString();
    }
}
