using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSlider : MonoBehaviour {

    [SerializeField] Slider volumeSlider;


void Start() {
    if(!PlayerPrefs.HasKey("soundVolume")) { //checks preferences from last game, default value set to 1/2
        PlayerPrefs.SetFloat("soundVolume", 1);
        Load();
    }
    else {
        Load();
    }
}

    public void ChangeVolume() {
        AudioListener.volume = volumeSlider.value;
        Save();

    }

    private void Load() {
        volumeSlider.value = PlayerPrefs.GetFloat("soundVolume");
        // set the value of the volume starter to the value stored in musicVolume

    }

    private void Save() {
        PlayerPrefs.SetFloat("soundVolume", volumeSlider.value);
    }
}