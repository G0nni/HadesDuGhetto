using System.CodeDom;
using System.Collections;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class DragonController : MonoBehaviour
{
    //Variables
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

    [Header("Score")]
    [SerializeField] public Text scoreText;
    [SerializeField] private int score;
    [SerializeField] private GameObject test;
    // Private
    private float lastAttackTime = 0;
    private int currentHealth;
    private MouseMovement mouseMovement;
    private Animator animator;

    private void Start()
    {
        // Récupération des références aux objets
        player = GameObject.FindGameObjectWithTag("Player");

        // Récupération de la référence du score
        scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();

        // Affichage du score
        scoreText.text = "Score: " + PlayerPrefs.GetInt("PlayerScore", 0);

        // Initialisation du script
        Initialize();
    }

    private void Update()
    {
        // Si la santé du dragon est inférieure ou égale à 0, on le tue
        if (currentHealth <= 0)
        {
            StartCoroutine(Die()); 
        }

        // Si le joueur n'est pas défini, que le dragon est mort ou qu'une action est en cours, on ne fait rien
        if (player == null || currentHealth <= 0 || IsAnimatingAttackOrDeath()) return; 

        // Mise à jour de la position du dragon
        UpdatePosition();
    }

    // Initialisation du script
    private void Initialize()
    {
        // Vérification de la référence au joueur
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

        // Vérification de la référence au script MouseMovement
        if (mouseMovement == null)
        {
            UnityEngine.Debug.LogError("Missing MouseMovement component on the player object.", player);
        }
    }

    // Mise à jour de la position du dragon
    private void UpdatePosition()
    {
        // Calcul de la distance entre le dragon et le joueur
        Vector3 targetPosition = player.transform.position;
        float distance = Vector3.Distance(transform.position, targetPosition);

        // Si le joueur est à portée d'attaque et que le temps écoulé depuis la dernière attaque est supérieur au temps de récupération, on attaque
        if (distance <= attackRange && Time.time - lastAttackTime > attackCooldown)
        {
            AttackPlayer();
            animator.SetBool("isWalking", false);
            return;
        }

        // Calcul de la vitesse de déplacement du dragon
        float adjustedSpeed = CalculateSpeed(distance);
        Vector3 movementDirection = (targetPosition - transform.position).normalized;

        // Rotation du dragon pour faire face à la direction du mouvement
        if (movementDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.15f);
            transform.Translate(movementDirection * adjustedSpeed * 2 * Time.deltaTime, Space.World);
        }

        // Mise à jour des états d'animation
        UpdateAnimationStates(distance);
    }

    // Calcul de la vitesse de déplacement du dragon
    private float CalculateSpeed(float distance)
    {
        return (distance < slowingDistance) ? Mathf.Lerp(0, speed, distance / slowingDistance) : speed;
    }

    // Mise à jour des états d'animation du dragon
    private void UpdateAnimationStates(float distance)
    {
        bool isMovingFast = distance > slowingDistance;
        animator.SetBool("isRunning", isMovingFast);
        animator.SetBool("isWalking", !isMovingFast);

    }

    // Méthode pour infliger des dégâts au dragon
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UnityEngine.Debug.Log("Dragon lost " + damage + " health. Current health: " + currentHealth);
        StartCoroutine(Hit());
    }

    // Méthode pour gérer les collisions avec les balles
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            UnityEngine.Debug.Log("Dragon hit by bullet!");
            TakeDamage(damagePerHit);
            Destroy(other.gameObject);
        }
    }

    // Méthode pour gérer la mort du dragon
    private IEnumerator Die()
    {
        animator.SetBool("isDie", true);
        yield return new WaitForSecondsRealtime(4);
        IncrementScore(10);
        Destroy(gameObject);
    }

    // Méthode pour gérer l'animation du hit
    private IEnumerator Hit()
    {
        animator.SetBool("isHit", true);
        yield return new WaitForSecondsRealtime(1);
        animator.SetBool("isHit", false);
    }

    // Méthode pour vérifier si le dragon est en train d'attaquer ou de mourir
    private bool IsAnimatingAttackOrDeath()
    {
        return animator.GetBool("isHit") || animator.GetBool("isDie");
    }

    // Méthode pour attaquer le joueur
    private void AttackPlayer()
    {
        animator.SetBool("isAttack", true);
        lastAttackTime = Time.time;
    }

    // Méthode pour l'incrémentation du score
    private void IncrementScore(int amount)
    {
        score = PlayerPrefs.GetInt("PlayerScore", 0);
        score += amount;
        PlayerPrefs.SetInt("PlayerScore", score);

        scoreText.text = "Score: " + score;  // Mettez à jour l'affichage du score ici

        UnityEngine.Debug.Log("Score updated: " + score);
    }
}
