using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;
using FMODUnity;
using FMOD;

public static class FMOD_Volume{

    public static float musicVol;
    public static float sfxVol;

    public static void SetMusicVolume(float vol)
    {
        string musicBusString = "bus:/Master/Music";
        FMOD.Studio.Bus musicBus;

        musicBus = FMODUnity.RuntimeManager.GetBus(musicBusString);
        musicBus.setVolume(vol);
    }

    public static void SetSFXVolume(float vol)
    {
        string sfxBusString = "bus:/Master/SFX";
        FMOD.Studio.Bus sfxBus;

        sfxBus = FMODUnity.RuntimeManager.GetBus(sfxBusString);
        sfxBus.setVolume(vol);

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
	}
}
