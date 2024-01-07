using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public static Action onGameStart;
    //public static Action onGameOver;
    [SerializeField] Transform core;
    [SerializeField] Transform[] levels;
    private int maxLevels = 0;
    public static int currentLevel = 0;
    public static Timer levelTimer;
    [SerializeField] private Timer timer;
    private void OnDisable()
    {
        onGameStart = null;
        Levels.onGameWin = null;
        Levels.onGameOver = null;
    }
    private void OnEnable()
    {
        //find timer object
        levelTimer = timer;
        currentLevel = 0;
        // 30 seconds for each Level And Timer Not Start yet
        InitTimer();

        maxLevels = levels.Length;
        //Subscribe to Lose Condition of Game
        Levels.onGameOver += OnGameOver;


        Levels.onGameWin += OnLevelCleared;
        onGameStart += OnGameStart;
    }
    private void Update()
    {
        if (!levelTimer.IsTimerRunning() && Levels.onGameOver != null)
        {
            Levels.onGameOver?.Invoke();
            Levels.onGameOver = null;
        }
    }
    public void OnGameStart()
    {
        core.gameObject.SetActive(true);
    }
    public void OnGameOver()
    {
        //InitTimer();
        core.gameObject.SetActive(false);

    }
    public void OnLevelCleared()
    {
        //Disable current Level
        levels[currentLevel].gameObject.SetActive(false);

        currentLevel++;
        //Enable Next Level if level still remain to clear
        if (currentLevel < maxLevels)
        {
            InitTimer();
            levels[currentLevel].gameObject.SetActive(true);
        }
        else
        {
            Levels.onGameOver?.Invoke();
            //CallGameWin
        }
    }
    void InitTimer()
    {
        TimerData timerData = new TimerData(30, true);
        levelTimer.timeData = timerData;
    }
}