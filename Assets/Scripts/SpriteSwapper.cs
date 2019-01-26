using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSwapper : MonoBehaviour {

    public SpriteRenderer Renderer;

    public float timePerSprite = 0.1f;

    public bool GoBack = true;
    public Sprite[] Sprites;

    float currentTime = 0f;
    int direction = 1;
    int currentSprite = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Sprites.Length<2) {
            return;
        }
        currentTime += Time.deltaTime;
        if (currentTime>timePerSprite) {
            currentTime = 0;
            if (currentSprite+direction>=Sprites.Length || currentSprite+direction<=0) {
                if (GoBack) {
                    direction = -direction;
                    currentSprite = currentSprite + direction;
                } else {
                    currentSprite = 0;
                }
                Renderer.sprite = Sprites[currentSprite];

            } else {
                currentSprite = currentSprite + direction;
                Renderer.sprite = Sprites[currentSprite];
            }
        }
	}
}
