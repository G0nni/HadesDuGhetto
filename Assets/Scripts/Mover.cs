using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    // Variables
    [Header("Player")]
    public float speed = 10;
    public Transform playerTransform;

    private Quaternion initialRotation;
    private bool directionSet = false;

    void Start()
    {
        // Récupération de l'objet joueur
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // Sauvegarde de la rotation initiale
        initialRotation = playerTransform.rotation;
    }

    void Update()
    {
        // Déplacement de l'objet
        if (!directionSet)
        {
            // Regarde le joueur
            transform.LookAt(playerTransform.position + playerTransform.forward);
            directionSet = true;
        }

        // Déplacement
        transform.rotation = initialRotation;
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
