using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance;
    public PlayerMovement moveScript;

    void Start()
    {
        moveScript = GetComponent<PlayerMovement>();
        DontDestroyOnLoad(gameObject);

    }

    private void Awake()
    {
        //THERE CAN ONLY BE ONE OF THE WORLD SAVE GAME MANAGERS IN THE SCENE
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0) moveScript.rb.isKinematic = false; else moveScript.rb.isKinematic = true;
    }

    
}
