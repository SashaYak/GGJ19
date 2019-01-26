using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    public float MoveDuration = 0.5f;
    public Vector3[] Positions;
    public bool GoBack = true;
    float progress = 0f;
    int direction = 1;
    int currentPosition = 0;
    int lastPosition = 0;

    // Use this for initialization
    void Start () {
        lastPosition = 0;
        lastPosition = 1;

    }
	
	// Update is called once per frame
	void Update () {
        if (Positions.Length < 2) {
            return;
        }
        progress += Time.deltaTime/MoveDuration;
        if (progress > 1) {
            progress = 0;
            if (currentPosition + direction >= Positions.Length || currentPosition + direction <= 0) {
                direction = -direction;
                lastPosition = currentPosition;
                currentPosition = currentPosition + direction;

            } else {
                lastPosition = currentPosition;
                currentPosition = currentPosition + direction;
            }
        }
        this.transform.localPosition = (1 - progress) * Positions[lastPosition] + progress * Positions[currentPosition];
    }
}
