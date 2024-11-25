using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Authentication;
using TMPro;
using System.Threading.Tasks;
using Unity.Services.Core;
public class Greeting : MonoBehaviour
{
    public string wonk;
    public TMP_InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        wonk = inputField.text;
        gameObject.GetComponent<TMP_Text>().SetText("Welcome to my game, " + wonk + ". Have a fun time!");
    }

}
