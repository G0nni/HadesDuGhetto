using System.CodeDom;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DragonController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private GameObject player;

    [Header("Movement")]
    [SerializeField] private float speed = 5.6f;
    [SerializeField] private float slowingDistance = 500f;

    [Header("Health")]
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int damagePerHit = 50;

    [Header("Attack")]
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackCooldown = 1.5f;

    private float lastAttackTime = 0;
    private int currentHealth;
    private MouseMovement mouseMovement;
    private Animator animator;

    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            UnityEngine.Debug.Log("Dragon is dead!");
            StartCoroutine(Die()); 
        }

        if (player == null || currentHealth <= 0 || IsAnimatingAttackOrDeath()) return; 

        UpdatePosition();
    }

    private void Initialize()
    {
        if (player == null)
        {
            UnityEngine.Debug.LogError("Player reference not set in GoOnPlayer script!", this);
            Destroy(gameObject);
            return;
        }

        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        mouseMovement = player.GetComponent<MouseMovement>();

        if (mouseMovement == null)
        {
            UnityEngine.Debug.LogError("Missing MouseMovement component on the player object.", player);
        }
    }

    private void UpdatePosition()
    {
        Vector3 targetPosition = player.transform.position;
        float distance = Vector3.Distance(transform.position, targetPosition);
        if (distance <= attackRange && Time.time - lastAttackTime > attackCooldown)
        {
            AttackPlayer();
            animator.SetBool("isWalking", false);
            return;
        }

        float adjustedSpeed = CalculateSpeed(distance);
        Vector3 movementDirection = (targetPosition - transform.position).normalized;

        if (movementDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.15f);
            transform.Translate(movementDirection * adjustedSpeed * Time.deltaTime, Space.World);
        }

        UpdateAnimationStates(distance);
    }

    private float CalculateSpeed(float distance)
    {
        return (distance < slowingDistance) ? Mathf.Lerp(0, speed, distance / slowingDistance) : speed;
    }

    private void UpdateAnimationStates(float distance)
    {
        bool isMovingFast = distance > slowingDistance;
        animator.SetBool("isRunning", isMovingFast);
        animator.SetBool("isWalking", !isMovingFast);

    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UnityEngine.Debug.Log("Dragon lost " + damage + " health. Current health: " + currentHealth);
        StartCoroutine(Hit());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            UnityEngine.Debug.Log("Dragon hit by bullet!");
            TakeDamage(damagePerHit);
            Destroy(other.gameObject);
        }
    }

    private IEnumerator Die()
    {
        animator.SetBool("isDie", true);
        yield return new WaitForSecondsRealtime(4);
        Destroy(gameObject);
    }

    private IEnumerator Hit()
    {
        animator.SetBool("isHit", true);
        yield return new WaitForSecondsRealtime(1);
        animator.SetBool("isHit", false);
    }

    private bool IsAnimatingAttackOrDeath()
    {
        return animator.GetBool("isHit") || animator.GetBool("isDie");
    }

    private void AttackPlayer()
    {
        animator.SetBool("isAttack", true);
        lastAttackTime = Time.time;
    }
}
