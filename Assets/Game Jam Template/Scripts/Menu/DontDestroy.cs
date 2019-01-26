using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using FMODUnity;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour {

    static DontDestroy instance = null;


    void Awake()

    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

          if (PlayerPrefs.GetInt("FmodOn") > 0)
            {
                GetComponent<StudioEventEmitter>().Play();
            }

        }     
    }
 

}
