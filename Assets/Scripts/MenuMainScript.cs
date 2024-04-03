using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMainScript : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
