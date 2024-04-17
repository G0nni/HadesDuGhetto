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
        // D�finit le timer � 0
        timer = 0.0f;
    }

    void Update()
    {
        // Incr�mente le timer
        timer += Time.deltaTime;

        // Si le timer est sup�rieur ou �gal � la dur�e de vie
        if (timer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Si l'objet entre en collision avec un ennemi alors il est d�truit
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}