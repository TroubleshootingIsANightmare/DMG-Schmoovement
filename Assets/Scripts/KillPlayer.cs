using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillPlayer : MonoBehaviour
{
    EventManager eventManager;
    
    PlayerMovement playerMovement;
    Grapple grapple;
    private void Start()
    {
        eventManager = GameObject.Find("EventManager").GetComponent<EventManager>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>(); 
        grapple = FindFirstObjectByType<Grapple>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Destroy(grapple.joint);
            playerMovement.spawned = false;
            Timer time = eventManager.timer.gameObject.GetComponent<Timer>();
            time.i = 0f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player") { 
            eventManager.died = true;
            playerMovement.spawned = false;
            Timer time = eventManager.timer.gameObject.GetComponent<Timer>();
            time.i = 0f;    
        }
    } 
    
}
