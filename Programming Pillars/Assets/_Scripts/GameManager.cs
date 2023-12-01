using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameMan;

    public bool paused;
    public bool gameOver;
    private int score;



    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private TextMeshProUGUI scoreText;

    private void Awake()
    {
        gameMan = this;
        score = 0;
        scoreText.text = $"{score} SCORE";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PauseGame();
        }
    }


    private void PauseGame()
    {
        if (paused) ResumeGame();
        else
        {
            Time.timeScale = 0;
            paused = true;
            pauseScreen.SetActive(true);
        }
    }


    public void ResumeGame()
    {
        Time.timeScale = 1;
        paused = false;
        pauseScreen.SetActive(false);
    }



    public void GameOver()
    {
        gameOver = true;
        gameOverScreen.SetActive(true);
    }

    public void RestartGame()
    {
        ResumeGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UpdateScore(int scoreAdded)
    {
        score += scoreAdded;
        scoreText.text = $"{score} SCORE";
    }
}
