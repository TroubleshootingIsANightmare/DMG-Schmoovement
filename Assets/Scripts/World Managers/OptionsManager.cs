using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Entities.UniversalDelegates;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public Slider sensXSlider;
    public Slider sensYSlider;
    public Slider musicVolSlider;
    public Slider SFXVolSlider;
    public GameObject titleCanvas;
    public GameObject tutorialTimeDisplay;
    public GameObject levelOneTimeDisplay;

    private const string LevelOneKey = "LevelOneTime";
    private const string LevelTutorialKey = "LevelTutorialTime";
    private const string SensXKey = "SensX";
    private const string SensYKey = "SensY";
    private const string MusicVolKey = "MusicVol";
    private const string SFXVolKey = "SFXVol";

    public static OptionsManager instance;


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

    public void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
        {
            tutorialTimeDisplay = GameObject.FindGameObjectWithTag("Tutorial Time");
            tutorialTimeDisplay.GetComponent<TMP_Text>().text = "Best Time: " + String.Format("{0:0.00}", PlayerPrefs.GetFloat(LevelTutorialKey, 9999f));
            tutorialTimeDisplay = GameObject.FindGameObjectWithTag("Level One Time");
            tutorialTimeDisplay.GetComponent<TMP_Text>().text = "Best Time: " + String.Format("{0:0.00}", PlayerPrefs.GetFloat(LevelOneKey, 9999f));
        }
    }
    void Start()
    {
        
        DontDestroyOnLoad(gameObject);
        // Load saved sensitivity values
        float savedSensX = PlayerPrefs.GetFloat(SensXKey, 200.0f);
        float savedSensY = PlayerPrefs.GetFloat(SensYKey, 200.0f);
        float savedMusicVol = PlayerPrefs.GetFloat(MusicVolKey, 0.5f);
        float savedSFXVol = PlayerPrefs.GetFloat(SFXVolKey, 0.5f);
        float savedTutorialTime = PlayerPrefs.GetFloat(LevelTutorialKey, 9999f);
        float savedLevelOneTime = PlayerPrefs.GetFloat(LevelOneKey, 9999f);
        // Set slider values
        sensXSlider.value = savedSensX;
        sensYSlider.value = savedSensY;
        musicVolSlider.value = savedMusicVol;
        SFXVolSlider.value = savedSFXVol;
        tutorialTimeDisplay.GetComponent<TMP_Text>().text = "Best Time: " + String.Format("{0:0.00}", savedTutorialTime);
        levelOneTimeDisplay.GetComponent<TMP_Text>().text = "Best Time: " + String.Format("{0:0.00}", savedLevelOneTime); 
        //Set values at start
        OnMusicVolChanged(savedMusicVol);
        OnSensXChanged(savedSensX);
        OnSensYChanged(savedSensY);
        OnSFXVolChanged(savedSFXVol);
        SetTutorialTime(savedTutorialTime);
        SetLevelOneTime(savedLevelOneTime);
        // Add listeners to handle slider value changes
        SFXVolSlider.onValueChanged.AddListener(OnSFXVolChanged);
        sensXSlider.onValueChanged.AddListener(OnSensXChanged);
        sensYSlider.onValueChanged.AddListener(OnSensYChanged);
        musicVolSlider.onValueChanged.AddListener(OnMusicVolChanged);

        

    }


    private void OnSensXChanged(float value)
    {
        // Save the new value
        PlayerPrefs.SetFloat(SensXKey, value);
        PlayerPrefs.Save();
    }

    private void OnMusicVolChanged(float value)
    {
        PlayerPrefs.SetFloat (MusicVolKey, value);
        PlayerPrefs.Save();
    }

    private void OnSFXVolChanged(float value)
    {
        PlayerPrefs.SetFloat(SFXVolKey, value);
        PlayerPrefs.Save();
    }

    private void OnSensYChanged(float value)
    {
        // Save the new value
        PlayerPrefs.SetFloat(SensYKey, value);
        PlayerPrefs.Save();
    }

    public void SetTutorialTime(float value)
    {
        PlayerPrefs.SetFloat(LevelTutorialKey, value);
        PlayerPrefs.Save();
    }

    public void SetLevelOneTime(float value)
    {
        PlayerPrefs.SetFloat(LevelOneKey, value);
        PlayerPrefs.Save();
    }

    // Optionally, you can have a method to get the sensitivity values
    public float GetSensX()
    {
        return PlayerPrefs.GetFloat(SensXKey, 200f);
    }

    public float GetSFXVol()
    {
        return PlayerPrefs.GetFloat(SFXVolKey, 0.5f);
    }

    public float GetSensY()
    {
        return PlayerPrefs.GetFloat(SensYKey, 200f);
    }

    public float GetMusicVol()
    {
        return PlayerPrefs.GetFloat(MusicVolKey, 0.5f);
    }

    public float GetTutorialTime()
    {
        return PlayerPrefs.GetFloat(LevelTutorialKey, 9999f);
    }

    public float GetLevelOneTime()
    {
        return PlayerPrefs.GetFloat(LevelOneKey, 9999f);
    }

}
