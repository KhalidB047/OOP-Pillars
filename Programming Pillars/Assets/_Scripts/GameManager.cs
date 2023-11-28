using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gameMan;

    public bool gameOver;



    [SerializeField] private GameObject gameOverScreen;

    private void Awake()
    {
        gameMan = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }





    public void GameOver()
    {
        gameOver = true;
        gameOverScreen.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
