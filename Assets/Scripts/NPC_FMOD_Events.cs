using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class NPC_FMOD_Events : MonoBehaviour {
    StudioEventEmitter _emitter;
    private bool fmodOn = false;
    private CollectibleType _type;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("FmodOn") > 0) fmodOn = true;

        if (GetComponent<PatrolCollectible>() != null)
        {
            _type = GetComponent<PatrolCollectible>().Type;
        }
        else
        {
            _type = GetComponent<BaseCollectible>().Type;
        }

        if (GetComponent<StudioEventEmitter>() == null)
        {
            _emitter = gameObject.AddComponent(typeof(StudioEventEmitter)) as StudioEventEmitter;
        }
        else _emitter = GetComponent<StudioEventEmitter>();


        if (fmodOn)
        {

            string idleSoundPath = "event:/SFX/" + _type.ToString() + "_idle";          
            if (FMOD_Debug.CheckFmodEvent(idleSoundPath))
            {
                _emitter.Event = idleSoundPath;
                _emitter.Play();
            }
        }
    }


    public void BumpSound()
    {
        if (fmodOn)
        {

            string eventPath = "event:/SFX/" + _type.ToString() + "_bump";
            if (FMOD_Debug.CheckFmodEvent(eventPath))
            {
                RuntimeManager.PlayOneShot(eventPath, transform.position);
            }
        }
    }

    public void DeathSound()
    {
        if (fmodOn)
        {

            string eventPath = "event:/SFX/" + _type.ToString() + "_death";
            if (FMOD_Debug.CheckFmodEvent(eventPath))
            {
                RuntimeManager.PlayOneShot(eventPath, transform.position);
            }
        }
    }

    public void FartSound()
    {
        if (fmodOn)
        {
            string eventPath = "event:/SFX/Panties_fart";
            if (FMOD_Debug.CheckFmodEvent(eventPath))
            {
                RuntimeManager.PlayOneShot(eventPath, transform.position);
            }
        }
    }

    private void OnDestroy()
    {
        _emitter.Stop();
    }
}
