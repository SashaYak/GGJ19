using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using FMOD.Studio;
using FMODUnity;


public static class FMOD_Debug  {


#if UNITY_EDITOR


    [MenuItem("Debug/Fmod On")]
    private static void NewMenuOption()
    {
      
        if (PlayerPrefs.GetInt("FmodOn") > 0)
        {
            PlayerPrefs.SetInt("FmodOn", 0); //0 for no fmod
        }
        else
        {
            PlayerPrefs.SetInt("FmodOn", 1);    // 1 for fmod
        }

        Debug.Log("FMOD is " + PlayerPrefs.GetInt("FmodOn"));
    }

#endif

    public static bool CheckFmodEvent(string eventPath)
    {     
            FMOD.Studio.EventDescription ed;
            RuntimeManager.StudioSystem.getEvent(eventPath, out ed);
        if (ed.isValid())
        {
            return true;
        }
        else
        {
            Debug.Log("FMOD Debug " + eventPath + " doesn't exist");
            return false;
            
        }
    }
}




