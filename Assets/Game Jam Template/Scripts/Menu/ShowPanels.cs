using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using FMODUnity;
using UnityEditor;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ShowPanels : MonoBehaviour {

	public GameObject optionsPanel;							//Store a reference to the Game Object OptionsPanel 
	public GameObject optionsTint;							//Store a reference to the Game Object OptionsTint 
	public GameObject menuPanel;							//Store a reference to the Game Object MenuPanel 
	public GameObject pausePanel;                           //Store a reference to the Game Object PausePanel 
    public GameObject creditsPanel;



    private GameObject activePanel;




    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Menu" && Input.GetButtonDown("Cancel"))
        {
            if (activePanel == optionsPanel)
            {
                HideOptionsPanel();
                ShowMenu();
            }

            if (activePanel == creditsPanel)
            {
                HideCreditsPanel();
                ShowMenu();
            }
        }
    }









    private void SetSelection(GameObject panelToSetSelected)
    {
        
        activePanel = panelToSetSelected;
    }

    public void Start()
    {
        SetSelection(menuPanel);
    }

    //Call this function to activate and display the Options panel during the main menu
    public void ShowOptionsPanel()
	{

        // menuPanel.SetActive(false);

        menuPanel.GetComponent<EventSystem>().enabled = false;
        Button[] b = menuPanel.GetComponentsInChildren<Button>();
        foreach(Button i in b)
        {
            i.interactable = false;
        }
        optionsPanel.SetActive(true);
        optionsTint.SetActive(true);
        SetSelection(optionsPanel);

    }

	//Call this function to deactivate and hide the Options panel during the main menu
	public void HideOptionsPanel()
	{
        //menuPanel.SetActive(true);
        optionsPanel.SetActive(false);
		optionsTint.SetActive(false);
        Button[] b = menuPanel.GetComponentsInChildren<Button>();
        foreach (Button i in b)
        {
            i.interactable = true;
        }
        menuPanel.GetComponent<EventSystem>().enabled = true;
       

    }

	//Call this function to activate and display the main menu panel during the main menu
	public void ShowMenu()
	{
		menuPanel.SetActive (true);
        SetSelection(menuPanel);
    }

	//Call this function to deactivate and hide the main menu panel during the main menu
	public void HideMenu()
	{
		menuPanel.SetActive (false);

	}
	
	//Call this function to activate and display the Pause panel during game play
	public void ShowPausePanel()
	{
		pausePanel.SetActive (true);
		optionsTint.SetActive(true);
        SetSelection(pausePanel);
    }

	//Call this function to deactivate and hide the Pause panel during game play
	public void HidePausePanel()
	{
		pausePanel.SetActive (false);
		optionsTint.SetActive(false);

	}

    public void ShowCreditsPanel()
    {
        menuPanel.GetComponent<EventSystem>().enabled = false;
        Button[] b = menuPanel.GetComponentsInChildren<Button>();
        foreach (Button i in b)
        {
            i.interactable = false;
        }



        creditsPanel.SetActive(true);
        optionsTint.SetActive(true);
        
        SetSelection(creditsPanel);
    }
    public void HideCreditsPanel()
    {
        
        creditsPanel.SetActive(false);
        optionsTint.SetActive(false);
        Button[] b = menuPanel.GetComponentsInChildren<Button>();
        foreach (Button i in b)
        {
            i.interactable = true;
        }
        menuPanel.GetComponent<EventSystem>().enabled = true;
    }
}
