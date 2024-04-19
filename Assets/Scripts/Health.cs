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

    void Start()
    {
        // Initialisation de la santé
        currentHealth = maxHealth;
    }

    private void Update()
    {
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
        GameObject.FindGameObjectWithTag("Event").GetComponent<PlayerFeedBack>().GameOver();
        Destroy(gameObject);
    }
}
