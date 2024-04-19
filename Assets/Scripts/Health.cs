using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    // Variables
    [Header("Vie")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("Damage")]
    public int damageAmount = 5;

    PlayerFeedBack script;

    void Start()
    {
        // Initialisation de la santé
        currentHealth = maxHealth;
    }

    private void Update()
    {
        // Si la santé du joueur est inférieure ou égale à 0, le joueur meurt
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Si le joueur entre en collision avec un ennemi, il perd de la vie
        if (other.gameObject.CompareTag("Enemy"))
        {
            currentHealth -= damageAmount;
            Debug.Log("Player lost " + damageAmount + " health. Current health: " + currentHealth);
        }
    }

    void Die()
    {
        // Le joueur meurt
        UnityEngine.Debug.Log("Player is dead");
        script = FindObjectOfType<PlayerFeedBack>();
        script.GameOver();
        Destroy(gameObject);
    }
}
