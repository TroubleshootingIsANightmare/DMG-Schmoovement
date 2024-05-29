using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    public Slider sensXSlider;
    public Slider sensYSlider;

    private const string SensXKey = "SensX";
    private const string SensYKey = "SensY";

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
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        // Load saved sensitivity values
        float savedSensX = PlayerPrefs.GetFloat(SensXKey, 200.0f);
        float savedSensY = PlayerPrefs.GetFloat(SensYKey, 200.0f);

        // Set slider values
        sensXSlider.value = savedSensX;
        sensYSlider.value = savedSensY;

        // Add listeners to handle slider value changes
        sensXSlider.onValueChanged.AddListener(OnSensXChanged);
        sensYSlider.onValueChanged.AddListener(OnSensYChanged);
    }

    private void OnSensXChanged(float value)
    {
        // Save the new value
        PlayerPrefs.SetFloat(SensXKey, value);
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
}
