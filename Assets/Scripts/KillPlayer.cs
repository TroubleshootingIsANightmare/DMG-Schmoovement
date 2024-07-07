using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    EventManager eventManager;

    private void Start()
    {
        eventManager = GameObject.Find("EventManager").GetComponent<EventManager>();
    }
    
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player") { 
            eventManager.died = true;
        }
    } 
}
