using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController Instance;


    public Level[] Levels;


    int lastLevel = 0;
    bool running = false;
    public float currentTimer = 0;
    float maxTimer = 0;

    Level CurrentLevel;


    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(this.gameObject);
        }
    }

    private void Start() {
        StartLevel(0);
    }

    public void StartLevel(int level) {
        running = true;
        CurrentLevel = Levels[level];
        currentTimer = CurrentLevel.Timer;
        maxTimer = CurrentLevel.Timer;
        lastLevel = level;
    }

    public void StartNextLevel() {
        lastLevel++;
        StartLevel(lastLevel);
    }

    public void StopLevel() {
        running = false;
    }

    public void Pause() {
        running = !running;
    }

    private void Update() {
        if (running) {
            currentTimer -= Time.deltaTime;
            if (currentTimer<=0) {
                TimeOut();
            }
        }
    }

    public void IncreaseTime(float timeIncrease) {
        currentTimer += timeIncrease;
    }

    public void TimeOut() {
        StopLevel();
    }

}
