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

    void Start()
    {
        // Initialisation
        gameOverText.SetActive(false);
    }

    // Game Over
    public void GameOver()
    {
        gameOverText.SetActive(true);
    }


    // Restart Game
    public void RestartGame()
    {
        UnityEngine.Debug.Log("Restart Game");
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