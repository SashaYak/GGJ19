using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class BG_animation : MonoBehaviour {

	public void EndGame() {

        GameController.Instance.StartCoroutine(GameController.Instance.EndBGAnimation());

     }

    public void ClickSound()
    {
        if (PlayerPrefs.GetInt("FmodOn") > 0)
        {
            string eventPath = "event:/SFX/MenuButtonsClick";
            if (FMOD_Debug.CheckFmodEvent(eventPath))
            {
                RuntimeManager.PlayOneShot(eventPath, transform.position);
            }
        }
    }
}
