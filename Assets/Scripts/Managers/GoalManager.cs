using System;
using UnityEngine;

public class GoalManager : MonoBehaviour
{
    public static Action onGoal;
    [SerializeField] int eachLevelGoal = 5;
    public static int currentGoals = 0;
    private void Awake()
    {
        currentGoals = 0;
        onGoal += Goal;
        //Subscribe to Reset goal, so level cleared or not, currentgoals will reset
        Levels.onGameWin += ResetGoals;
        Levels.onGameOver += ResetGoals;
    }
    private void OnDisable()
    {
        onGoal = null;
    }
    public void Goal()
    {
        currentGoals++;
        if (currentGoals >= eachLevelGoal)
            Levels.onGameWin?.Invoke();

    }

    // Whenever each level win or lose, current goal will be reset
    public void ResetGoals()
    {
        currentGoals = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            onGoal?.Invoke();
    }
}