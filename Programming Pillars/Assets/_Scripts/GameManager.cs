using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameMan;

    public bool paused;
    public bool gameOver;
    private int score;
    private int highScore;




    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject musicPlayer;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI newHighScoreText;






    private void Awake()
    {
        gameMan = this;
        score = 0;
        scoreText.text = $"{score} SCORE";
        LoadGame();
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
            if (gameOver) return;
            musicPlayer.SetActive(true);
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
        if (FindObjectOfType<BackgroundMusic>().dockLocked) return;
        else musicPlayer.SetActive(false);
    }



    public void GameOver()
    {
        gameOver = true;
        gameOverScreen.SetActive(true);
        UpdateHighScore();
    }

    public void RestartGame()
    {
        ResumeGame();
        newHighScoreText.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void UpdateScore(int scoreAdded)
    {
        score += scoreAdded;
        scoreText.text = $"{score} SCORE";
    }

    public void UpdateHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            newHighScoreText.gameObject.SetActive(true);
        }
        SaveGame();
    }

    public void SaveGame()
    {
        SaveData data = new SaveData();
        data.highScore = highScore;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            highScore = data.highScore;
            highScoreText.text = $"High Score\n {highScore}";
        }
    }



    [System.Serializable]
    private class SaveData
    {
        public int highScore;
    }

}
