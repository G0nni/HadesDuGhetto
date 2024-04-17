using System.Collections;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    //Variables
    [Header("Vie")]
    public float lifeTime = 30.0f;
    private float timer;

    void Start()
    {
        // Définit le timer à 0
        timer = 0.0f;
    }

    void Update()
    {
        // Incrémente le timer
        timer += Time.deltaTime;

        // Si le timer est supérieur ou égal à la durée de vie
        if (timer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Si l'objet entre en collision avec un ennemi alors il est détruit
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}