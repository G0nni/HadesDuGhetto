using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;// a rajouter
using UnityEngine.UI;//a ajouter

public class PlayerFeedBack : MonoBehaviour
{
    // Variables
    [Header("Text")]
    public GameObject gameOverText;
    public Text scoreText;

    private bool gameover = false;
    private int score = 0;

    void Start()
    {
        // Initialisation
        gameOverText.SetActive(false);
        
        // Initialisation du score
        scoreText.text = "Points : 0";
    }

    // Game Over
    public void GameOver()
    {
        gameover = true;
        gameOverText.SetActive(true);
    }

    // Score
    public void scoreUpdate(int points)
    {
        score += points;
        scoreText.text = "Points : " + score;
        
    }

    // Restart Game
    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    // Relance le jeu
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    // Retour au menu
    public void ReturnTitleScreen()
    {
        SceneManager.LoadScene(0);
    }

    // Quitter le jeu
    public void QuitGame()
    {
        Application.Quit();
    }
}