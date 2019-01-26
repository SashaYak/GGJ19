using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuObject : MonoBehaviour {

    //Drag the object which you want to be automatically selected by the keyboard or gamepad when this panel becomes active
    public GameObject firstSelectedObject;
    public EventSystem ev;


    public void OnEnable()
    {
        //Check if we have an event system present
        
        
            SetFirstSelected();
         //   Debug.Log(firstSelectedObject + " " + ev.gameObject.name);
        

    }

    public void SetFirstSelected()
    {
        //Tell the EventSystem to select this object
        // EventSystem ev = GetComponentInParent<EventSystem>();
       ev.firstSelectedGameObject = firstSelectedObject;
        ev.SetSelectedGameObject(firstSelectedObject);

       
    }

  

}
