using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SpriteSwapper : MonoBehaviour {

    public SpriteRenderer Renderer;
    public Image TargetImage;

    public float timePerSprite = 0.1f;

    public bool GoBack = true;
    public Sprite[] Sprites;

    public AnimEvent[] Events;

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
            if (currentSprite+direction>=Sprites.Length || currentSprite+direction<0) {
                if (GoBack) {
                    direction = -direction;
                    currentSprite = currentSprite + direction;
                } else {
                    currentSprite = 0;
                }
                if (Renderer!=null) {
                    Renderer.sprite = Sprites[currentSprite];
                }
                if (TargetImage!=null) {
                    TargetImage.sprite= Sprites[currentSprite];
                }

            } else {
                currentSprite = currentSprite + direction;
                if (Renderer != null) {
                    Renderer.sprite = Sprites[currentSprite];
                }
                if (TargetImage != null) {
                    TargetImage.sprite = Sprites[currentSprite];
                }
            }
            if (Events!=null && Events.Length>0) {
                for (int i = 0; i < Events.Length; i++) {
                    if (Events[i].Position==currentSprite) {
                        Events[i].Event.Invoke();
                    }
                }
            }
        }
	}
}

[System.Serializable]
public struct AnimEvent {
    public int Position;
    public UnityEvent Event;
}