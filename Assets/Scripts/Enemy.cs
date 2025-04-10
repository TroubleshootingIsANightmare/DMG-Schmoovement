using System.Collections;
using System.Collections.Generic;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;

    public Rigidbody rb;
    public Collider[] ragdollColliders;
    public Rigidbody[] ragdollRigidbodies;

    public Transform target, body, shootPoint;

    public bool ragdolled = false;

    public LayerMask whatIsGround, whatIsPlayer;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet = false;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;


    //Sound
    private AudioSource audioSource;

    //Particle Effects
    public GameObject shootFX;
    // Start is called before the first frame update
    void Start()
    {
        ragdollColliders = GetComponentsInChildren<Collider>();
        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        agent = this.GetComponent<NavMeshAgent>();
        rb = this.GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.enabled = !ragdolled;
        if (!ragdolled) { DisableNav();  }

        //Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
        if (!ragdolled)
        {
            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) ChasePlayer();
            if (playerInAttackRange && playerInSightRange) AttackPlayer(); agent.SetDestination(target.position);
            
        }
        
    }


    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if (distanceToWalkPoint.magnitude < 10f)
            walkPointSet = false;

    }
    private void SearchWalkPoint()
    {
        //Calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;



    }

    private void ChasePlayer()
    {
        agent.SetDestination(target.position);
    }

    private void AttackPlayer()
    {


        if (!alreadyAttacked)
        {
            ///Attack code here
            Rigidbody rb = Instantiate(projectile, shootPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            Instantiate(shootFX, shootPoint.position, shootPoint.rotation);
            audioSource.Play();
            Vector3 dir = target.position - shootPoint.position;
            rb.AddForce(dir.normalized * 32f, ForceMode.Impulse);
            rb.gameObject.transform.localScale = this.gameObject.transform.localScale * 20f;

            ///End of attack code

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }
    private void ResetAttack()
    {
        alreadyAttacked = false;
    }
    private void LateUpdate()
    {
        if (!ragdolled) { AimBody(); }
    }

    public void DisableNav()
    {
        //Get target and point gun
        target = GameObject.FindGameObjectWithTag("Target").transform;
        //Make run when far and not run when close
        animator.SetBool("Running", agent.remainingDistance > agent.stoppingDistance);
        
    }

    private void AimBody()
    {
        // Get the direction from this object to the target
        Vector3 direction = target.position - body.position;
        // Project the direction onto the Y plane (ignore horizontal movement)
        direction.x = 0; direction.z = 0;
        //Trig that does the thing!
        float angle = Mathf.Atan2(direction.y, Vector3.Distance(new Vector3(target.position.x, 0, target.position.z), new Vector3(body.position.x, 0, body.position.z))) * Mathf.Rad2Deg;

        body.localRotation = Quaternion.Euler(-180, 0, -angle);

        //Rotate entire object to look at player
        Vector3 dir = agent.destination - gameObject.transform.position;
        dir.y = 0;
        Quaternion rot = Quaternion.LookRotation(dir);
        transform.rotation = rot;

    }

    public void EnableRagdoll(bool enabled)
    {
        animator.enabled = !enabled;
        foreach(Rigidbody item in ragdollRigidbodies)
        {
            item.useGravity = enabled;
            item.isKinematic = !enabled;
        }
        rb.useGravity = !enabled;
        rb.isKinematic = enabled;
        ragdolled = enabled;
    }
}
