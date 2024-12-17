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

    //public TextMeshProUGUI survivalTimeText;
    //public TextMeshProUGUI enemiesKilledText;

    public bool isGameOver = false;

    void Update()
    {
        UpdateStopwatch();
        UpdateKillCountText();
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
        stopwatchDisplay.text = string.Format("Time survived: " + "{0:00}:{1:00}", minutes, seconds);
    }

    void UpdateKillCountText()
    {
        killCountText.text = "Flies eaten: " + killCount.ToString();
    }
}
