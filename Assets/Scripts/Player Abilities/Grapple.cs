using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    // Components and variables used in grappling mechanics
    private LineRenderer rope;  // LineRenderer for visualizing the grappling rope
    private Camera cam;         // Camera used for aiming the grappling hook
    private float maxDistance = 100f; // Maximum distance for grappling
    private Vector3 gPoint;     // Temporary variable for the grappling point animation
    private Vector3 targetPoint;// Actual point where grappling happens
    private GameObject player;  // Reference to the player object
    public SpringJoint joint;   // SpringJoint for grappling physics
    public LayerMask canGrapple; // LayerMask to specify which objects can be grappled
    public GameObject grappleSphere;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = grappleSphere.AddComponent<Rigidbody>();
        rb.isKinematic = true;
        grappleSphere.SetActive(false);
        // Initialize the rope LineRenderer and camera
        rope = GetComponent<LineRenderer>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        // Disable the rope initially, and set the player's position as the default grappling point
        rope.enabled = false;
        gPoint = transform.position;

        // Locate the player GameObject in the scene
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // If grappling is active, continuously update the rope's position
        if (rope.enabled)
        {
            DrawRope();
        }

        // Start grappling when the middle mouse button or 'Q' key is pressed
        if (Input.GetKeyDown(KeyCode.Mouse2) || Input.GetKeyDown(KeyCode.Q))
        {
            StartGrapple();
        }
        // Stop grappling when the middle mouse button or 'Q' key is released
        if (Input.GetKeyUp(KeyCode.Mouse2) || Input.GetKeyUp(KeyCode.Q))
        {
            StopGrapple();
        }

        

        // Check for objects within grappling range
        RaycastHit hit;
        if (Physics.Raycast(cam.gameObject.transform.position, cam.gameObject.transform.forward, out hit, maxDistance, canGrapple))
        {
            
            // Create a sphere to indicate the grappling point  
            grappleSphere.SetActive (true);
            if (!rope.enabled) { grappleSphere.transform.position = hit.point; }// Place it at the detected point
            grappleSphere.transform.localScale = Vector3.one * 0.5f; // Adjust the sphere's size
            grappleSphere.GetComponent<Renderer>().material.color = Color.green; // Green indicates in range

            // Add a Rigidbody and set it to kinematic to prevent physical interactions
            rb = grappleSphere.GetComponent<Rigidbody>();
            rb.isKinematic = true;

            // If grappling is active, change the sphere's color to red
            if (rope.enabled)
            {
                grappleSphere.GetComponent<Renderer>().material.color = Color.red;
                grappleSphere.transform.position = targetPoint;
            }
        }
        else
        {
            grappleSphere.SetActive(false);
        }
    }

    // Method to render the rope and animate its movement
    void DrawRope()
    {
        // Gradually move the grappling point towards the target for a smooth animation effect
        gPoint = Vector3.Lerp(gPoint, targetPoint, Time.deltaTime * 8f);

        // Update the rope's start and end positions
        rope.SetPosition(0, transform.position);
        rope.SetPosition(1, gPoint);
    }

    // Method to stop the grappling mechanic
    void StopGrapple()
    {
        rope.enabled = false; // Disable the rope visuals

        Destroy(joint); // Remove the SpringJoint from the player
    }

    // Method to start the grappling mechanic
    void StartGrapple()
    {
        RaycastHit hit;

        // Reset grappling point to player's current position
        gPoint = transform.position;

        // Cast a ray forward from the camera to check for objects in range
        if (Physics.Raycast(cam.gameObject.transform.position, cam.gameObject.transform.forward, out hit, maxDistance, canGrapple))
        {
            // Store the detected point as the grappling target
            targetPoint = hit.point;

            // Add a SpringJoint to the player for grappling mechanics
            joint = player.gameObject.AddComponent<SpringJoint>();

            // Disable auto-configuration to set custom properties
            joint.autoConfigureConnectedAnchor = false;

            // Set the SpringJoint anchor to the grappling point
            joint.connectedAnchor = targetPoint;

            // Calculate the distance between the player and the grappling target
            float distance = Vector3.Distance(player.transform.position, targetPoint);

            // Configure SpringJoint properties for grappling behavior
            joint.maxDistance = distance * 0.8f; // Maximum rope length
            joint.minDistance = distance * 0.25f; // Minimum rope length
            joint.spring = 4.5f;                 // Spring force for pulling effect
            joint.damper = 7f;                   // Damping effect for smooth transitions
            joint.massScale = 5.0f;              // Scale for mass interaction with the joint
        }

        // Set up the rope LineRenderer for visualization
        rope.SetPosition(0, transform.position);
        joint.connectedAnchor = targetPoint; // Connect rope to the target
        rope.SetPosition(1, targetPoint);
        rope.enabled = true; // Enable the rope visuals
    }
}