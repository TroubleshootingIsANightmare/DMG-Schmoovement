using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Vector3 target;
    public Animator animator;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        agent = this.GetComponent<NavMeshAgent>();
        rb = this.GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.Find("Player").transform.position;
        agent.SetDestination(target);
        if(rb.velocity.magnitude > 0f)
        {
            animator.SetBool("Running", true);
        } else
        {
            animator.SetBool("Running", false);
        }
        this.gameObject.transform.LookAt(target);
    }
}
