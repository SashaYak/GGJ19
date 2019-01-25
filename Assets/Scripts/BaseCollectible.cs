using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCollectible : MonoBehaviour {

    public float MoodModifier = 1;

    public virtual  void Collect() {
        Debug.Log(name + " collected ");
        Destroy(this.gameObject);
    }
}
