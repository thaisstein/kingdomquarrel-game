using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MusicSlider : MonoBehaviour {

    [SerializeField] Slider musicSlider;


void Start() {
    if(!PlayerPrefs.HasKey("musicVolume")) { //checks preferences from last game, default value set to 1/2
        PlayerPrefs.SetFloat("musicVolume", 1);
        Load();
    }
    else {
        Load();
    }
}

    public void ChangeVolume() {
        AudioListener.volume = musicSlider.value;
        Save();

    }

    private void Load() {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        // set the value of the volume starter to the value stored in musicVolume

    }

    private void Save() {
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
    }
}