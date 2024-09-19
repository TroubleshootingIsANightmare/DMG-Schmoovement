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
    public TMP_InputField username;
    public int hasName = 0;
    public GameObject titleCanvas;

    private const string SensXKey = "SensX";
    private const string SensYKey = "SensY";
    private const string MusicVolKey = "MusicVol";
    private const string usernameKey = "username";
    private const string hasNameKey = "hasName";

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
        username = GameObject.Find("/LoginCanvas/InputField (TMP)").GetComponent<TMP_InputField>();

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

        //Check for name 
        hasName = PlayerPrefs.GetInt("hasName", 0);
        if(hasName > 0)
        {
            GameObject.Find("LoginCanvas").SetActive(false);
            titleCanvas.SetActive(true);
            Debug.Log(PlayerPrefs.GetString("username"));
        }

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

    public void SetUsername()
    {
        PlayerPrefs.SetString("username", username.text);
        hasName = 1;
        PlayerPrefs.SetInt("hasName", hasName);
        PlayerPrefs.Save();
    }
}
