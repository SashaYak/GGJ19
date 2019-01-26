using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Player_Fmod_Events : MonoBehaviour {

    StudioEventEmitter _emitter;
    private bool fmodOn = false;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("FmodOn") > 0) fmodOn = true;

        _emitter = GetComponent<StudioEventEmitter>();
        if (fmodOn)
        {
            _emitter.SetParameter("speed", 0f);
            string movementPath = "event:/SFX/Player_movement";
            if (FMOD_Debug.CheckFmodEvent(movementPath))
            {
                _emitter.Event = movementPath;
                _emitter.Play();
            }
        }       
    }

    public void SetIdleSound(float speed)
    {
        if (fmodOn)
        {
            _emitter.SetParameter("speed", speed);
        }       
    }

    public void TurningSound()
    {
        if (fmodOn)
        {
            string eventhPath = "event:/SFX/Player_turn";
            if (FMOD_Debug.CheckFmodEvent(eventhPath))
            {
                RuntimeManager.PlayOneShot(eventhPath, transform.position);
            }
        }
    }

    public void CatchSound(bool fish)
    {
        if (fmodOn)
        {
            if (fish)
            {
                string eventhPath = "event:/SFX/Fish_catch";
                if (FMOD_Debug.CheckFmodEvent(eventhPath))
                {
                    RuntimeManager.PlayOneShot(eventhPath, transform.position);
                }
            }
            else
            {
                string eventhPath = "event:/SFX/Wolf_catch";
                if (FMOD_Debug.CheckFmodEvent(eventhPath))
                {
                    RuntimeManager.PlayOneShot(eventhPath, transform.position);
                }
            }
        }
    }

    public void BumpSound()
    {
        if (fmodOn)
        {          
                string eventhPath = "event:/SFX/bump";
                if (FMOD_Debug.CheckFmodEvent(eventhPath))
                {
                    RuntimeManager.PlayOneShot(eventhPath, transform.position);
                }         
        }
    }

    public void ComplainSound(bool fish)
    {
        if (fmodOn)
        {
            if (fish)
            {
                string eventhPath = "event:/SFX/Fish_complain";
                if (FMOD_Debug.CheckFmodEvent(eventhPath))
                {
                    RuntimeManager.PlayOneShot(eventhPath, transform.position);
                }
            }
            else
            {
                string eventhPath = "event:/SFX/Wolf_complain";
                if (FMOD_Debug.CheckFmodEvent(eventhPath))
                {
                    RuntimeManager.PlayOneShot(eventhPath, transform.position);
                }
            }
        }
    }

    public void DeathSound()
    {
        if (fmodOn)
        {
            string eventhPath = "event:/SFX/Player_death";
            if (FMOD_Debug.CheckFmodEvent(eventhPath))
            {
                RuntimeManager.PlayOneShot(eventhPath, transform.position);
            }
        }
    }




    private void OnDisable()
    {
        if (fmodOn)
        {
            _emitter.Stop();
        }
    }
}
