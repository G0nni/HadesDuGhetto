using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMainScript : MonoBehaviour
{
    // Fonction pour lancer le jeu
    public void PlayGame()
    {
        // Chargement de la scène suivante
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Fonction pour quitter le jeu
    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
