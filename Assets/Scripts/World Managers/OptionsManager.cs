using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public Slider sensXSlider;
    public Slider sensYSlider;
    public Slider musicVolSlider;
    public GameObject titleCanvas;

    private const string SensXKey = "SensX";
    private const string SensYKey = "SensY";
    private const string MusicVolKey = "MusicVol";

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

    }
    void Start()
    {
        
        DontDestroyOnLoad(gameObject);
        // Load saved sensitivity values
        float savedSensX = PlayerPrefs.GetFloat(SensXKey, 200.0f);
        float savedSensY = PlayerPrefs.GetFloat(SensYKey, 200.0f);
        float savedMusicVol = PlayerPrefs.GetFloat(MusicVolKey, 0.5f);

        // Set slider values
        sensXSlider.value = savedSensX;
        sensYSlider.value = savedSensY;
        musicVolSlider.value = savedMusicVol;

        //Set values at start
        OnMusicVolChanged(savedMusicVol);
        OnSensXChanged(savedSensX);
        OnSensYChanged(savedSensY);

        // Add listeners to handle slider value changes
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

    private void OnSensYChanged(float value)
    {
        // Save the new value
        PlayerPrefs.SetFloat(SensYKey, value);
        PlayerPrefs.Save();
    }

    // Optionally, you can have a method to get the sensitivity values
    public float GetSensX()
    {
        return PlayerPrefs.GetFloat(SensXKey, 200f);
    }

    public float GetSensY()
    {
        return PlayerPrefs.GetFloat(SensYKey, 200f);
    }

    public float GetMusicVol()
    {
        return PlayerPrefs.GetFloat(MusicVolKey, 0.5f);
    }

    
}
