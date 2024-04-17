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

    // Private
    private float lastAttackTime = 0;
    private int currentHealth;
    private MouseMovement mouseMovement;
    private Animator animator;

    // Start is called before the first frame update
    private void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    private void Update()
    {
        // Si la sant� du dragon est inf�rieure ou �gale � 0, on le tue
        if (currentHealth <= 0)
        {
            UnityEngine.Debug.Log("Dragon is dead!");
            StartCoroutine(Die()); 
        }

        // Si le joueur n'est pas d�fini, que le dragon est mort ou qu'une action est en cours, on ne fait rien
        if (player == null || currentHealth <= 0 || IsAnimatingAttackOrDeath()) return; 

        // Mise � jour de la position du dragon
        UpdatePosition();
    }

    // Initialisation du script
    private void Initialize()
    {
        // V�rification de la r�f�rence au joueur
        if (player == null)
        {
            UnityEngine.Debug.LogError("Player reference not set in GoOnPlayer script!", this);
            Destroy(gameObject);
            return;
        }

        // Initialisation des variables
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        mouseMovement = player.GetComponent<MouseMovement>();

        // V�rification de la r�f�rence au script MouseMovement
        if (mouseMovement == null)
        {
            UnityEngine.Debug.LogError("Missing MouseMovement component on the player object.", player);
        }
    }

    // Mise � jour de la position du dragon
    private void UpdatePosition()
    {
        // Calcul de la distance entre le dragon et le joueur
        Vector3 targetPosition = player.transform.position;
        float distance = Vector3.Distance(transform.position, targetPosition);

        // Si le joueur est � port�e d'attaque et que le temps �coul� depuis la derni�re attaque est sup�rieur au temps de r�cup�ration, on attaque
        if (distance <= attackRange && Time.time - lastAttackTime > attackCooldown)
        {
            AttackPlayer();
            animator.SetBool("isWalking", false);
            return;
        }

        // Calcul de la vitesse de d�placement du dragon
        float adjustedSpeed = CalculateSpeed(distance);
        Vector3 movementDirection = (targetPosition - transform.position).normalized;

        // Rotation du dragon pour faire face � la direction du mouvement
        if (movementDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.15f);
            transform.Translate(movementDirection * adjustedSpeed * 2 * Time.deltaTime, Space.World);
        }

        // Mise � jour des �tats d'animation
        UpdateAnimationStates(distance);
    }

    // Calcul de la vitesse de d�placement du dragon
    private float CalculateSpeed(float distance)
    {
        return (distance < slowingDistance) ? Mathf.Lerp(0, speed, distance / slowingDistance) : speed;
    }

    // Mise � jour des �tats d'animation du dragon
    private void UpdateAnimationStates(float distance)
    {
        bool isMovingFast = distance > slowingDistance;
        animator.SetBool("isRunning", isMovingFast);
        animator.SetBool("isWalking", !isMovingFast);

    }

    // M�thode pour infliger des d�g�ts au dragon
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UnityEngine.Debug.Log("Dragon lost " + damage + " health. Current health: " + currentHealth);
        StartCoroutine(Hit());
    }

    // M�thode pour g�rer les collisions avec les balles
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            UnityEngine.Debug.Log("Dragon hit by bullet!");
            TakeDamage(damagePerHit);
            Destroy(other.gameObject);
        }
    }

    // M�thode pour g�rer la mort du dragon
    private IEnumerator Die()
    {
        animator.SetBool("isDie", true);
        yield return new WaitForSecondsRealtime(4);
        Destroy(gameObject);
    }

    // M�thode pour g�rer l'animation du hit
    private IEnumerator Hit()
    {
        animator.SetBool("isHit", true);
        yield return new WaitForSecondsRealtime(1);
        animator.SetBool("isHit", false);
    }

    // M�thode pour v�rifier si le dragon est en train d'attaquer ou de mourir
    private bool IsAnimatingAttackOrDeath()
    {
        return animator.GetBool("isHit") || animator.GetBool("isDie");
    }

    // M�thode pour attaquer le joueur
    private void AttackPlayer()
    {
        animator.SetBool("isAttack", true);
        lastAttackTime = Time.time;
    }
}
