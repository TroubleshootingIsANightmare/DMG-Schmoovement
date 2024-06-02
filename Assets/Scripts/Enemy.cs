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
    public Collider[] ragdollColliders;
    public Rigidbody[] ragdollRigidbodies;

    public bool ragdolled = false;

    // Start is called before the first frame update
    void Start()
    {
        ragdollColliders = GetComponentsInChildren<Collider>();
        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        agent = this.GetComponent<NavMeshAgent>();
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.enabled = !ragdolled;
        if (!ragdolled) { DisableNav(); }
    }

    public void DisableNav()
    {
        target = GameObject.Find("Player").transform.position;
        agent.SetDestination(target);
        if (rb.velocity.magnitude > 0f)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }
        Vector3 dir = target - gameObject.transform.position;
        dir.y = 0;
        Quaternion rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, rot, 3f * Time.deltaTime);
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
