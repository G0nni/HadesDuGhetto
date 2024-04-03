using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;// a rajouter
using UnityEngine.UI;//a ajouter

public class PlayerFeedBack : MonoBehaviour
{
    public GameObject gameOverText;
    public Text scoreText;

    private bool gameover = false;
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        gameOverText.SetActive(false);
        
        scoreText.text = "Points : 0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        gameover = true;
        gameOverText.SetActive(true);
    }

    public void scoreUpdate(int points)
    {
        score += points;
        scoreText.text = "Points : " + score;
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ReturnTitleScreen()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}