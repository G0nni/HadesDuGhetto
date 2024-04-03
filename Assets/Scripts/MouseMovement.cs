using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    [Header("Agent")]
    public UnityEngine.AI.NavMeshAgent agent;

    [Header("Animation")]
    public Animator anim;
    public float animSpeed = 1.5f;

    [Header("Movement")]
    public float rotationSpeed = 5.0f;
    public float vitesseDeplacement = 2;
    public float jumpForce = 700f;
    public Rigidbody rb;

    [Header("Attaque")]
    public GameObject shot;
    public Transform shotspawn;
    public float fireRate = 1;

    // Private
    private float h;
    private float v;
    private int locoState;
    private AnimatorStateInfo currentBaseState;
    private float nextFireTime = 0;
    private int m_HashMeleeAttack = Animator.StringToHash("MeleeAttack");
    private int m_InputDetected = Animator.StringToHash("InputDetected");
    private bool isGrounded;


    void Start()
    {
        // Set sur le sol true
        anim.SetBool("Grounded", true);
        anim.Update(0);
    }

    void Update()
    {
        agent.speed = vitesseDeplacement;

        anim.ResetTrigger(m_HashMeleeAttack);
        anim.SetBool(m_InputDetected, false);

        // Déplacement
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
                transform.LookAt(hit.point);
            }
        }

        // Attaque
        if (Input.GetMouseButtonDown(0) && Time.time >= nextFireTime)
        {
            RaycastHit hit2;
            Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray2, out hit2))
            {
                transform.LookAt(hit2.point);
            }
            
            anim.SetBool(m_InputDetected, true);
            anim.SetTrigger(m_HashMeleeAttack);

            Vector3 direction = (hit2.point - transform.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction);

            GameObject bullet = Instantiate(shot, shotspawn.position, rotation) as GameObject;
            bullet.GetComponent<Mover>().playerTransform = transform;

            nextFireTime = Time.time + fireRate;
        }

        // Rotation
        if (agent.hasPath)
        {
            Quaternion targetRotation = Quaternion.LookRotation(agent.steeringTarget - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        float distanceToDestination = Vector3.Distance(transform.position, agent.destination);
        float speed = agent.velocity.magnitude / agent.speed;

        if (distanceToDestination > 0.1f)
        {
            anim.SetFloat("ForwardSpeed", speed);   
        }
        else if (!agent.isStopped)
        {
            anim.SetFloat("ForwardSpeed", 0);
        }


    }
    
}





