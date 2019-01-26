using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour {
    public Slider MusicSlider;
    public Slider SFXSlider;



    public void Start()
    {
        MusicSlider.value = FMOD_Volume.musicVol;
        SFXSlider.value = FMOD_Volume.sfxVol;

    }

    
}
