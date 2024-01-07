using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI goalText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI currentLevelText;
    [SerializeField] Transform inGameUIPanel;
    [SerializeField] Transform mainMenu;
    [SerializeField] Transform gameOverPanel;
    void Start()
    {
        GoalManager.onGoal += OnGoalUpdateText;
        GameManager.onGameStart += OnGameStart;

        Levels.onGameOver += ActivateGameOverPanel;
        Levels.onGameWin += OnGoalUpdateText;
        Levels.onGameWin += UpdateCurrentLevelText;
        Levels.onGameOver += OnGoalUpdateText;


    }
    public void OnGameStart()
    {
        inGameUIPanel.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(false);
    }
    void OnGoalUpdateText()
    {
        goalText.text = "Goals: " + GoalManager.currentGoals + "/5";
    }
    private void Update()
    {
        timerText.text = "Timer: " + GameManager.levelTimer.timeData.seconds.ToString("00") + "s";
    }
    private void ActivateGameOverPanel()
    {
        gameOverPanel.gameObject.SetActive(true);
        inGameUIPanel.gameObject.SetActive(false);
    }
    private void UpdateCurrentLevelText()
    {
        currentLevelText.text = "Level: " + (GameManager.currentLevel + 1).ToString();
    }
}
