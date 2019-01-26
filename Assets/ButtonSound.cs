using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.
using FMODUnity;
using FMOD;
using UnityEditor;

public class ButtonSound : MonoBehaviour
{

    private Menu_Script menu;

  /*  //Do this when the selectable UI object is selected.
    public void OnSelect(BaseEventData eventData)
    {
        
            if (EditorPrefs.GetBool("FmodOn"))
            {
                string eventPath = "event:/SFX/MenuButtonsChange";
                if (EditorPrefs.GetBool("FmodOn") && FMOD_Debug.CheckFmodEvent(eventPath))
                {
                    RuntimeManager.PlayOneShot(eventPath, transform.position);
                }
            }

    }*/
    private void Start()
    {
        menu = GetComponent<Menu_Script>();
    }

    public void OnClickSound()
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

    public void ButtonChangeSound()
    {
        if (PlayerPrefs.GetInt("FmodOn") > 0)
        {
            string eventPath = "event:/SFX/MenuButtonsChange";
            if (FMOD_Debug.CheckFmodEvent(eventPath))
            {
                RuntimeManager.PlayOneShot(eventPath, transform.position);
            }
        }
    }


    private void Update()
    {
        if ( !menu.notInMenu && ( Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow)
            || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow)
            || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)))
        {
            ButtonChangeSound();
        }
    }



}
