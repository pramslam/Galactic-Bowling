using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameEnd : MonoBehaviour
{

    public Text totalScoreText;

    private bool restart;
    private int totalScore;

    // Use this for initialization
    void Start()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 0.0f;
        gameObject.GetComponent<CanvasGroup>().interactable = false;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false;
        totalScoreText.text = "Total Score: ";
        restart = false;
        totalScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (restart)
        {
            SceneManager.LoadScene("Game");
        }
    }

    // Updates score
    public void UpdateScore(int score)
    {
        totalScore = score;
        totalScoreText.text = "Total Score: " + totalScore.ToString();
    }

    // Shows menu at end game
    public void EndGame()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 1.0f;
        gameObject.GetComponent<CanvasGroup>().interactable = true;
        gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }

    // Restarts game
    public void RestartGame()
    {
        restart = true;
    }
}
