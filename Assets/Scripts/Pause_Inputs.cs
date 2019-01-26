using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEditor;
using FMODUnity;
using UnityEngine.SceneManagement;


public class Pause_Inputs : MonoBehaviour {


    public Color highlightedColor;
    public Color normalColor;


    public EventSystem ev;
    public Image resume;
    public Image goMenu;

    private bool onResume = true;
    public ButtonSound butSound;
    public Pause pauseScript;
    public ShowPanels showpanels;
    public Menu_Script menu;


    private void Start()
    {
        onResume = true;
        resume.GetComponent<Image>().color = highlightedColor;
        goMenu.GetComponent<Image>().color = normalColor;
    }

    private void Update()
    {
        if (!menu.notInMenu)
        {

            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                if (onResume) onResume = false;
                resume.GetComponent<Image>().color = normalColor;
                goMenu.GetComponent<Image>().color = highlightedColor;
                butSound.ButtonChangeSound();

            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                if (!onResume) onResume = true;
                resume.GetComponent<Image>().color = highlightedColor;
                goMenu.GetComponent<Image>().color = normalColor;
                butSound.ButtonChangeSound();

            }

            if (Input.GetButtonDown("Submit"))
            {

                butSound.OnClickSound();

                if (onResume)
                {

                    pauseScript.UnPause();
                    showpanels.HidePausePanel();
                }
                else
                {
                    pauseScript.UnPause();
                    showpanels.HidePausePanel();
                    showpanels.ShowMenu();
                    SceneManager.LoadScene(0);
                }

            }

        }
    }




}
