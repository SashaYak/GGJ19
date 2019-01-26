using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;
using FMODUnity;
using FMOD;
using UnityEditor;

public static class FMOD_Volume{

    public static float musicVol = 1f;
    public static float sfxVol = 1f;


    public static void SetMusicVolume(float vol)
    {
        if (PlayerPrefs.GetInt("FmodOn") > 0)
        {
            string musicBusString = "bus:/Master/Music";
            FMOD.Studio.Bus musicBus;

            musicBus = FMODUnity.RuntimeManager.GetBus(musicBusString);
            musicBus.setVolume(vol);
            musicVol = vol;
        }
    }

    public static void SetSFXVolume(float vol)
    {
        if (PlayerPrefs.GetInt("FmodOn") > 0)
        {
            string sfxBusString = "bus:/Master/SFX";
            FMOD.Studio.Bus sfxBus;

            sfxBus = FMODUnity.RuntimeManager.GetBus(sfxBusString);
            sfxBus.setVolume(vol);
            sfxVol = vol;
        }

    }



}





public class SetAudioLevels : MonoBehaviour {

	public AudioMixer mainMixer;					//Used to hold a reference to the AudioMixer mainMixer


	//Call this function and pass in the float parameter musicLvl to set the volume of the AudioMixerGroup Music in mainMixer
	public void SetMusicLevel(float musicLvl)
	{
        FMOD_Volume.SetMusicVolume(musicLvl);       
    }

	//Call this function and pass in the float parameter sfxLevel to set the volume of the AudioMixerGroup SoundFx in mainMixer
	public void SetSfxLevel(float sfxLevel)
	{
		mainMixer.SetFloat("sfxVol", sfxLevel);
        FMOD_Volume.SetSFXVolume(sfxLevel);
    }
}
