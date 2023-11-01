using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Required for UI elements

public class GoalScript : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Reference to the UI Text element for displaying the score
    private int score; // Initial score

    void Start()
    {
        // Find the Text component in the scene with the name "ScoreText"
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        score = 0;
        UpdateScoreText(); // Update the score text initially
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            // Increment the score when the ball enters the goal area
            score++;
            UpdateScoreText();
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}