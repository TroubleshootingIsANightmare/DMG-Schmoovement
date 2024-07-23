using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderBehavior : MonoBehaviour
{
    public TMP_Text sliderText;
    public Slider slider;
    public string words = string.Empty;
    

    // Update is called once per frame
    void Update()
    {
        sliderText.text = words + ": " + slider.value.ToString("F2");        
    }
}
