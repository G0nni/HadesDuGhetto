using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class DragonController_test : MonoBehaviour
{
    public float speed = 5.0f;

    public float attackCooldown = 0.5f; // Temps de récupération entre les attaques

    private Animator animator;
    private float lastAttackTime = -1f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            UnityEngine.Debug.LogError("Animator not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Normalisation pour s'assurer que le personnage se déplace à vitesse constante dans toutes les directions
        if (movement.magnitude > 1 && !Input.GetKey(KeyCode.LeftShift))
        {
            movement.Normalize();
        }

        // Walking
        bool isWalking = movement.magnitude > 0;
        animator.SetBool("isWalking", isWalking);

        if (isWalking)
        {
            // Rotation du personnage pour faire face à la direction du mouvement
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15F);

            // Déplacement du personnage
            transform.Translate(movement * speed * Time.deltaTime, Space.World);
        }

        // Running
        bool isRunning = Input.GetKey(KeyCode.LeftShift) && movement.magnitude > 0;
        animator.SetBool("isRunning", isRunning);

        if (isRunning)
        {
            // Augmenter la vitesse de déplacement si le personnage court
            transform.Translate(movement * speed * 4 * Time.deltaTime, Space.World);
        }

        // Is Attacked
        bool isHit = Input.GetKey(KeyCode.Space);
        animator.SetBool("isHit", isHit);

        if (isHit)
        {
            // Augmenter la vitesse de déplacement si le personnage court
            UnityEngine.Debug.Log("Hit");
            bool isDie = Input.GetKey(KeyCode.RightShift);
            animator.SetBool("isDie", isDie);

            if (isDie)
            {
                // Augmenter la vitesse de déplacement si le personnage court
                UnityEngine.Debug.Log("Die");
            }
        }


        // Attack
        bool isAttack = Input.GetButtonDown("Fire1") && Time.time > lastAttackTime + attackCooldown;
        animator.SetBool("isAttack", isAttack);

        if (isAttack)
        {
            // Augmenter la vitesse de déplacement si le personnage court
            lastAttackTime = Time.time;
            UnityEngine.Debug.Log("Attack");
        }
    }
}
