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
        Time.timeScale = paused ? 0 : 1;
        TogglePause();
        if(!paused && pauseMenu.activeInHierarchy == false) { cameraManager.TurnCam(); cameraManager.LockCursor();  }
        if(!paused && pauseMenu.activeInHierarchy == true) paused = true;
        if (paused) Cursor.lockState = CursorLockMode.None; Cursor.visible = true;
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


    public void CloseGame()
    {
        Application.Quit();
    }


}
