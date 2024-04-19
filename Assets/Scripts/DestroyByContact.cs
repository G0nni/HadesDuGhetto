using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    //Variables
    [Header("Score")]
    public int scoreValue = 50;
    private GameObject objetRecherche;
    private PlayerFeedBack objetRechercheScript;

    
    void Start()
    {
        // Recherche l'objet avec le tag GameController
        objetRecherche = GameObject.FindWithTag("GameController");

        // Récupère le script PlayerFeedBack de l'objet trouvé
        objetRechercheScript = objetRecherche.GetComponent<PlayerFeedBack>();
    }

    void OnTriggerEnter(Collider other)
    {
        // Si l'objet entre en collision avec un ennemi alors il est détruit
        if (other.tag == "Player")
        {
            objetRechercheScript.GameOver();
            Destroy(other.gameObject);
        }

        // Si l'objet entre en collision avec un ennemi alors il est détruit
        if (other.tag == "Bullet")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

    }

}