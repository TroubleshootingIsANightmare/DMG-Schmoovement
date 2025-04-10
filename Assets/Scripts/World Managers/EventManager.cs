using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;
using TMPro;

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

    public bool reloaded = false;

    [Header("Timer")]
    public TMP_Text timer;
    public Timer timerScript;
    [Header("Crosshair")]
    public GameObject crosshair;

    public GameObject player;

    public string playerName;

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
        timerScript.setTime(0);
    }

    public void ReloadScene()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        reloaded = true;
        paused = false;
        died = false;
        timerScript.setTime(0);
    }

    public void SetPaused(bool pause)
    {
        paused = pause;
        pauseMenu.SetActive(pause);
    }

    private void Update()
    {
        CheckDeath();
        TogglePause();
        Time.timeScale = paused ? 0 : 1;
        if (!paused) Time.timeScale = died ? 0 : 1;
        deathMenu.SetActive(died);
        if (died) { paused = false; pauseMenu.SetActive(false); }
        timer.gameObject.SetActive(SceneManager.GetActiveScene().buildIndex != 0);
        crosshair.SetActive(SceneManager.GetActiveScene().buildIndex != 0);
        if (SceneManager.GetActiveScene().buildIndex == 0 || paused || died)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }

    private void LateUpdate()
    {
        if (!paused) { pauseMenu.SetActive(false); }
        if (SceneManager.GetActiveScene().buildIndex == 0) { died = false; }
    }

    void TogglePause()
    {
        if (Input.GetKeyUp(KeyCode.Escape) && SceneManager.GetActiveScene().buildIndex != 0 && !died)
        {
            if (paused)
            {
                pauseMenu.SetActive(false);
                paused = false;
            } else if(!paused)
            {
                pauseMenu.SetActive(true);
                paused = true;
            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 0) { pauseMenu.SetActive(false); paused = false;  }
       
    }

    void CheckDeath()
    {
        if (SceneManager.GetActiveScene().buildIndex != 0 && died) Time.timeScale = 0; Cursor.lockState = CursorLockMode.None; Cursor.visible = true;
        if (SceneManager.GetActiveScene().buildIndex == 0) { died = false; }
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    void SetSpawn()
    {

    }

}
