using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    [Header("Pause Menu")]
    public GameObject pauseMenu;
    public bool paused = false;
    public CameraManager cameraManager;

    [Header("Death Menu")]
    public GameObject deathMenu;
    public bool died = false;
    


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
        died = false;
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        died = false;
        paused = false;
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        Debug.Log("Died = " + died + " Paused = " + paused);
        CheckDeath();
        TogglePause();
        Time.timeScale = paused ? 0 : 1;
        if (!paused) Time.timeScale = died ? 0 : 1;
        deathMenu.SetActive(died);
        if (died) { paused = false; pauseMenu.SetActive(false); Debug.Log("DIED SETTING IT TO FALSE FOR NO REASON"); }
        if (!paused && pauseMenu.activeInHierarchy == false && SceneManager.GetActiveScene().buildIndex != 0 && !died) { cameraManager.TurnCam(); cameraManager.LockCursor();  }
        if(!paused && pauseMenu.activeInHierarchy == true) paused = true;
        if (paused) Cursor.lockState = CursorLockMode.None; Cursor.visible = true;
    }

    void TogglePause()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex != 0 && !died)
        {
            Debug.Log("TRIED TO PAUSE");
            if (paused)
            {
                pauseMenu.SetActive(false);
                paused = false;
                Debug.Log("UNPAUSE!");
            } else if(!paused)
            {
                Debug.Log("PAUSE!");
                pauseMenu.SetActive(true);
                paused = true;
            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 0) { pauseMenu.SetActive(false); paused = false; Debug.Log("THIS IS THE ISSUE. BUILD INDEX THING!"); }
    }

    void CheckDeath()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0 && died) Time.timeScale = 0; Cursor.lockState = CursorLockMode.None; Cursor.visible = true; 
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    void SetSpawn()
    {

    }

}
