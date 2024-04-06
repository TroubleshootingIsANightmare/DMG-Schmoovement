using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    [Header("Pause Menu")]
    public GameObject pauseMenu;
    [SerializeField] bool paused = false;
    private void Start() {
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

    public void ChangeScene(int i)
    {
        SceneManager.LoadScene(i);
    }

    private void Update()
    {
        TogglePause();
    }

    void TogglePause()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (paused)
            {
                pauseMenu.SetActive(false);
                paused = false;
            } else
            {
                pauseMenu.SetActive(true);
                paused = true;
            }
        } 
        if(SceneManager.GetActiveScene().buildIndex == 0) pauseMenu.SetActive(false); paused = false;
    }
}
