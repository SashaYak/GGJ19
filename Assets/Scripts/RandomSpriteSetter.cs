using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpriteSetter : MonoBehaviour {

    public GameObject[] Sprites;
	// Use this for initialization
	void Start () {
        int rnd = Random.Range(0, Sprites.Length);
        if (Sprites[rnd]==null) {
            return;
        }
        for (int i = 0; i < Sprites.Length; i++) {
            if (Sprites[i]!=null) {
                Sprites[i].SetActive(false);
            }
        }
        Sprites[rnd].SetActive(true);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
