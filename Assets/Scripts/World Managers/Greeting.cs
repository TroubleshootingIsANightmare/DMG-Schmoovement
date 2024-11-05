using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Authentication;
using TMPro;
using Unity.Services.Core;
public class Greeting : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
           
    }

    async void Awake()
    {
        await UnityServices.InitializeAsync();
    }
    // Update is called once per frame
    void Update()
    {
       
    }
}
