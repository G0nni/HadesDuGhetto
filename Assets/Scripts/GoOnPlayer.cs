using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoOnPlayer : MonoBehaviour
{
    public GameObject player;

    Vector3 targetPosition = new Vector3(0, 0, 0);

    float slowingDistance = 10f;

    public float speed = 3f;

    public int maxHealth = 100;
    private int currentHealth;

    private MouseMovement mouseMovement;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");

        currentHealth = maxHealth;

        targetPosition = player.transform.position;

        mouseMovement = player.GetComponent<MouseMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        targetPosition = player.transform.position;

        transform.LookAt(player.transform);

        // Calculer la distance entre l'ennemi et sa position cible
        float distance = Vector3.Distance(transform.position, targetPosition);

        // Calculer la vitesse de déplacement en fonction de la distance restante
        float currentSpeed = speed;
        if (distance < slowingDistance)
        {
            currentSpeed = distance / slowingDistance * speed;
        }

        // Appliquer la vitesse de déplacement à la position de l'ennemi
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, currentSpeed * Time.deltaTime);

        if (currentHealth <= 0)
        {

            
            
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    


}
