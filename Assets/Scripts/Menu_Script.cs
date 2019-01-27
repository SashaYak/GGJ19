using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.SceneManagement;

public class Menu_Script : MonoBehaviour {

    public bool notInMenu = false;
    public GameObject menuImage;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;       
    }


    public void StartGame()
    {
        notInMenu = true;
        SceneManager.LoadScene(1);
        
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Ranking")
        {
            notInMenu = false;
        }
        if (scene.buildIndex == 1)
        {
            SetGame();
        }
        if (scene.buildIndex == 0)
        {
            SetMenu();
        }
    }

    public void SetMenu()
    {
        
        notInMenu = false;
        if (menuImage != null) menuImage.SetActive(true);
        GetComponent<ShowPanels>().ShowMenu();
    }

    public void SetGame()
    {
        
        notInMenu = true;
       if (menuImage!= null) menuImage.SetActive(false);      
    }
}
