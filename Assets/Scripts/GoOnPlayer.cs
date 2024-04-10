using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class GoOnPlayer : MonoBehaviour
{
    [Header("Player")]
    public GameObject player;

    [Header("Movement")]
    public float speed = 3f;
    public float slowingDistance = 10f;

    [Header("Health")]
    public int maxHealth = 100;

    private int currentHealth;
    private MouseMovement mouseMovement;
    private Animator animator;


    void Start()
    {
        Initialize();
    }

    void Update()
    {
        UpdatePosition();
        UpdateAnimator();
        CheckHealth();
    }

    void Initialize()
    {
        if (player != null)
        {
            currentHealth = maxHealth;
            animator = GetComponent<Animator>();
            mouseMovement = player.GetComponent<MouseMovement>();
        }
        else
        {
            UnityEngine.Debug.LogError("Player reference not set in GoOnPlayer script!");
            Destroy(gameObject);
        }
    }

    void UpdatePosition()
    {
        if (player != null)
        {
            Vector3 targetPosition = player.transform.position;
            transform.LookAt(targetPosition);

            float distance = Vector3.Distance(transform.position, targetPosition);
            float currentSpeed = (distance < slowingDistance) ? distance / slowingDistance * speed : speed;

            Vector3 movement = targetPosition - transform.position;
            if (movement.magnitude > 0)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
                transform.Translate(movement.normalized * currentSpeed * Time.deltaTime, Space.World);
            }
        }
    }

    void UpdateAnimator()
    {
        if (animator != null)
        {
            bool isWalking = (player != null && mouseMovement != null);
            animator.SetBool("isWalking", isWalking);
        }
    }

    void CheckHealth()
    {
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
