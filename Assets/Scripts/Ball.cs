using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Required for UI elements

public class Ball : MonoBehaviour
{
    public Transform target;
    public float Force;
    public TextMeshProUGUI scoreText; // Reference to the UI Text element for displaying the score
    private int score; // Initial score
    private Vector3 initialPosition;
    private bool canShoot = true;

    void Start()
    {
        initialPosition = new Vector3(0, 1, 35);

        // Find the Text component in the scene with the name "ScoreText"
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
        score = 0;
        UpdateScoreText(); // Update the score text initially
    }

    void Update()
    {
        if (canShoot && Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        canShoot = false; // Prevent shooting while in motion

        Vector3 shootDirection = (target.position - transform.position).normalized;
        GetComponent<Rigidbody>().AddForce(shootDirection * Force + new Vector3(0, 3f, 0), ForceMode.Impulse);

        StartCoroutine(ResetBallAfterDelay(3f));
    }

    IEnumerator ResetBallAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        transform.position = initialPosition;

        canShoot = true; // Allow shooting again
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target")) // Assuming the target has a "Target" tag
        {
            score++; // Increment the score
            Debug.Log("Score: " + score);
            UpdateScoreText(); // Update the score text
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString();
    }
}