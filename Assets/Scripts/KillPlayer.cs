using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    EventManager eventManager;
    PlayerMovement playerMovement;
    private void Start()
    {
        eventManager = GameObject.Find("EventManager").GetComponent<EventManager>();
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player") { 
            eventManager.died = true;
            playerMovement.spawned = false;
        }
    } 

}
