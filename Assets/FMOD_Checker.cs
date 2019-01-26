using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMOD_Checker : MonoBehaviour {
    public bool FMOD_On;

	// Use this for initialization
	void Awake () {
		if (FMOD_On)
        {
            PlayerPrefs.SetInt("FmodOn", 1);
        }
	}

}
