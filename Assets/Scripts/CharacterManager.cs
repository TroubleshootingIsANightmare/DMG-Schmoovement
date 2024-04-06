using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance;
    void Start()
    {
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
}
