using System.Collections;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float lifeTime = 30.0f; // Temps de vie en secondes
    private float timer;

    void Start()
    {
        timer = 0.0f;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}