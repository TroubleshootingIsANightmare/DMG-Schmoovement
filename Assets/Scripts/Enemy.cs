using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent agent;
    public Animator animator;

    public Rigidbody rb;
    public Collider[] ragdollColliders;
    public Rigidbody[] ragdollRigidbodies;

    public Transform target, body;

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
        if (!ragdolled) { DisableNav();  }
    }
    private void LateUpdate()
    {
        if (!ragdolled) { AimBody(); }
    }

    public void DisableNav()
    {
        //Get target and point gun
        target = GameObject.FindGameObjectWithTag("Target").transform;
        //Set destination so enemy moves to target
        agent.SetDestination(target.position);
        //Make run when far and not run when close
        animator.SetBool("Running", agent.remainingDistance > agent.stoppingDistance);
        Debug.Log(animator.GetBool("Running"));
        //Rotate entire object to look at player
        Vector3 dir = target.position - gameObject.transform.position;
        dir.y = 0;
        Quaternion rot = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, rot, 3f * Time.deltaTime);
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
