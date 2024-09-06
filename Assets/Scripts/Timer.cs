using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    public TMP_Text _time;
    public float i;
    PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        _time = GetComponent<TMP_Text>();
        i = 0f;
        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        if (!playerMovement.spawned)
        {
            i = 0f;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        i += 1f/50f;
        _time.text = "Time: " + String.Format("{0:0.00}", i);
    }


    public float returnTime()
    {
        return i;
    }
}
