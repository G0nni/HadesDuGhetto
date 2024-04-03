using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public int scoreValue = 50;


    private GameObject objetRecherche;//pour stocker la ref de mon gamecontroller
    private PlayerFeedBack objetRechercheScript;//pour stocker la ref du scripf associé
    // Start is called before the first frame update
    void Start()
    {
        objetRecherche = GameObject.FindWithTag("GameController");
        objetRechercheScript = objetRecherche.GetComponent<PlayerFeedBack>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player")
        {
            objetRechercheScript.GameOver();
            Destroy(other.gameObject);
        }
        if (other.tag == "Bullet")
        {
            objetRechercheScript.scoreUpdate(scoreValue);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

    }

}