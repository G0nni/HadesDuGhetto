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
    public int damageAmount = 10;

    void Start()
    {
        // Initialisation de la sant�
        currentHealth = maxHealth;
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

    // Fonction pour infliger des d�g�ts au joueur
    public void TakeDamage(float damage)
    {
        // Si le joueur prend des d�g�ts, on les applique � sa sant�
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // action � effectuer lorsque le personnage meurt (par exemple, afficher un message Game Over)
    }
}
