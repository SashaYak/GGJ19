using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Test : MonoBehaviour
{
    public string eventPath;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("FMOD event exist - " + FMOD_Debug.CheckFmodEvent(eventPath));
        }
    }
}
