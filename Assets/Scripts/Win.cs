using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Win : MonoBehaviour
{
    public bool win = false;
    public Timer timer;

    private void FixedUpdate()
    {
        timer = GameObject.Find("/Player GUI/Timer").GetComponent<Timer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            FinishLevel();
        }
    }

    public void FinishLevel()
    {
        win = true; 
    }
}
