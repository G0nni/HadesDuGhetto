using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangeManager : MonoBehaviour
{
    [SerializeField] private int scoreThreshold = 100; // Le seuil de score pour changer de scène

    private DragonController dragonController;

    private void Start()
    {
        
    }

    void Update()
    {
        if (PlayerPrefs.GetInt("PlayerScore")>=scoreThreshold)
        {
            Debug.Log("JSUIS LA CONNARD");
            SceneManager.LoadScene("lvl-boss");
        }
    }



}
