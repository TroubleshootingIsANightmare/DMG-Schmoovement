using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] GameObject controlObject;
    public static PlayerUIManager instance;
    public GameObject crosshair;
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Awake()
    {
        
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
        DisplayControls();
       
    }

    private void DisplayControls()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0) { controlObject.SetActive(true); crosshair.SetActive(true); } else { controlObject.SetActive(false); crosshair.SetActive(false); }

    }


    
}
