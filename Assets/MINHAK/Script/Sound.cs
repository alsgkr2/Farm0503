using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    AudioSource audioSource;

    Slider slider;
    

    float soundV;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        slider = GameObject.Find("ParentObect").transform.Find("MenuObjects").transform.Find("SoundSlider").gameObject.GetComponent<Slider>();
    }


    void Update()
    {
        soundV = slider.value;
        audioSource.volume = soundV;
    }
}