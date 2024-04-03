using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public Animator anim;
    public float animSpeed = 1.5f;
    private float h;
    private float v;
    private int locoState;
    private AnimatorStateInfo currentBaseState;
    public float VitesseDéplacement = 2;
    int m_HashMeleeAttack = Animator.StringToHash("MeleeAttack");
    int m_InputDetected = Animator.StringToHash("InputDetected");

    public GameObject shot;
    public Transform shotspawn;

    public float fireRate = 1;
    private float nextFireTime = 0;

    void Start()
    {
        anim.SetBool("Grounded", true);
        anim.Update(0);
        
        
    }

    void Update()
    {
        agent.speed = VitesseDéplacement; // x est la vitesse souhaitée en m/s

        anim.ResetTrigger(m_HashMeleeAttack);
        anim.SetBool(m_InputDetected, false);

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

       

        float jumpInput = Input.GetAxis("Jump");
        if (jumpInput > 0)
        {
            anim.SetBool("Grounded", false);
            anim.Update(0);
            anim.SetBool("Grounded", true);
            anim.Update(0);



        }

        float distanceToDestination = Vector3.Distance(transform.position, agent.destination);
        float speed = agent.velocity.magnitude / agent.speed;
        

        if (distanceToDestination > 0.1f)
        {
            
            anim.SetFloat("ForwardSpeed", speed);
           // anim.SetBool("Walking", true);
            
        }
        else if (!agent.isStopped)
        {
           // anim.SetBool("Walking", false);
            anim.SetFloat("ForwardSpeed", 0);
        }


    }
    
}





