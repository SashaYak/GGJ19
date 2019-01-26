using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using UnityEngine.SceneManagement;

public class Menu_Script : MonoBehaviour {

    public bool notInMenu = false;
   

    public void StartGame()
    {
        notInMenu = true;
        SceneManager.LoadScene(1);
    }


}
