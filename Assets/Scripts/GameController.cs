using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public static GameController Instance;

    public Animator BG_Animator;
    public Level[] Levels;

    public float PointMultiplier = 1;
    int lastLevel = 0;
    bool running = false;
    float currentTimer = 0;

    float maxTimer = 0;

    Level CurrentLevel;

    public int MaxBalance = 20;
    public int BalanceChangeStart = 5;
    public int BalanceChangeHard = 15;

    public Text ScoreUI;
    public int CurrentScore;
    private StudioEventEmitter _emitter;

    [SerializeField]
    int balance = 0;

    public int Balance {
        get {
            return balance;
        }
        set {
            balance = value;
            AdjustBalance();
        }
    }

    public float CurrentTimer {
        get {
            return currentTimer;
        }

        set {
            currentTimer = value;
            AdjustTime();
        }
    }

    public Color FishColor;
    public Color WolfColor;
    public Color RedColor;

    public Image TopImage;
    public Image BotImage;

    public GameObject Fish;
    public GameObject FishAngry;

    public GameObject Wolf;
    public GameObject WolfAngry;

    public GameObject MoodIndicator;
    public Transform MoodTop;
    public Transform MoodMid;
    public Transform MoodBot;

    public Image FillImage;
    public GameObject TimeIndicator;
    public Transform TimeTop;
    public Transform TimeMid;
    public Transform TimeBot;

    public Transform BotLeft;
    public Transform TopRight;

    public BaseCollectible Unterhose;
    public BaseCollectible Kakerlake;
    public BaseCollectible Tasse;


    private void Awake() {
        if (Instance == null) {
            Instance = this;
            _emitter = GetComponent<StudioEventEmitter>();
        } else {
            Destroy(this.gameObject);
        }
    }

    private void Start() {
        StartLevel(0);
        Debug.Log("Start spawning");
        Spawn(Unterhose);
        Spawn(Kakerlake);
        Spawn(Tasse);

        _emitter.Play();
    }

    public void StartLevel(int level) {
        running = true;
        CurrentLevel = Levels[level];
        maxTimer = CurrentLevel.Timer;
        CurrentTimer = CurrentLevel.Timer;
        lastLevel = level;
        StartCoroutine(doSpawn(CurrentLevel.UnterhosenDelay, Unterhose));
        StartCoroutine(doSpawn(CurrentLevel.KakerlakenDelay, Kakerlake));
        StartCoroutine(doSpawn(CurrentLevel.TassenDelay, Tasse));

    }

    IEnumerator doSpawn(float time, BaseCollectible col) {
        while (running) {

            yield return new WaitForSeconds(time);
            Spawn(col);
        }
    }

    public void StartNextLevel() {
        lastLevel++;
        StartLevel(lastLevel);
    }

    public void StopLevel() {
        running = false;
        Invoke("EndLevel", 0.5f);
    }

    public void EndLevel() {
        BG_Animator.SetBool("End", true);
    }

    public IEnumerator EndBGAnimation()
    {
        yield return new WaitForSeconds(2f);
        PlayerPrefs.SetInt("Score", CurrentScore);
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }




    public void Pause() {
        running = !running;
    }

    private void Update() {
        if (running) {
            CurrentTimer -= Time.deltaTime;
            if (CurrentTimer<=0) {
                TimeOut();
            }
        }
    }

    public void IncreaseTime(float timeIncrease) {
        CurrentTimer += timeIncrease*PointMultiplier;
    }

    public void TimeOut() {
        StopLevel();
    }

    public void Explode(bool isFish=true) {
        StopLevel();
    }

    void AdjustBalance() {
        if (!running) {
            return;
        }
        Debug.Log(Balance);
        int bal = Balance;
        if (Balance>0) {
            if (bal > MaxBalance) {
                Explode();
                bal = MaxBalance;
            }
            if (bal< BalanceChangeStart) {
                TopImage.color = FishColor;
            } else {
                TopImage.color = Color.Lerp(FishColor, RedColor, (float)(bal - BalanceChangeStart) / (float)(MaxBalance - BalanceChangeStart));
            }
            if (bal>=BalanceChangeHard) {
                FishAngry.SetActive(true);
                Fish.SetActive(false);
            } else {
                FishAngry.SetActive(false);
                Fish.SetActive(true);
            }
            MoodIndicator.transform.position = (float)(bal) / MaxBalance * MoodTop.position + (float)(MaxBalance-bal) / MaxBalance * MoodMid.position;
        } else {
            bal = -bal;
            if (bal > MaxBalance) {
                Explode();
                bal = MaxBalance;
            }
            if (bal < BalanceChangeStart) {
                BotImage.color = WolfColor;
            } else {
                BotImage.color = Color.Lerp(WolfColor, RedColor, (float)(bal - BalanceChangeStart) / (float)(MaxBalance - BalanceChangeStart));
            }
            if (bal >= BalanceChangeHard) {
                WolfAngry.SetActive(true);
                Wolf.SetActive(false);
            } else {
                WolfAngry.SetActive(false);
                Wolf.SetActive(true);
            }
            MoodIndicator.transform.position = (float)(bal) / MaxBalance * MoodBot.position + (float)(MaxBalance - bal) / MaxBalance * MoodMid.position;
        }
        
    }

    void AdjustTime() {
        if (!running) {
            return;
        }
        float ratio = (currentTimer / maxTimer);
        FillImage.fillAmount = 1 - ratio;
        TimeIndicator.transform.position = ratio * TimeBot.position + (1 - ratio) * TimeTop.position;

        _emitter.SetParameter("DirtLevel", 1f -ratio);
        

    }

    public void SetNewScore(int sc) {
        CurrentScore += sc;
        ScoreUI.text = CurrentScore.ToString();
    }

    int maxSpawns = 1000;

    public void Spawn(BaseCollectible spawn) {
        float x = Random.Range(0, 1f);
        float y = Random.Range(0, 1f);

        x = x * BotLeft.position.x + (1 - x) * TopRight.position.x;
        y= y * BotLeft.position.y + (1 - y) * TopRight.position.y;
        int count = 0;
        while (!spawn.CheckMovement(new Vector3(x,y,0)) && count< maxSpawns) {
            x = Random.Range(0, 1f);
            y = Random.Range(0, 1f);

            x = x * BotLeft.position.x + (1 - x) * TopRight.position.x;
            y = y * BotLeft.position.y + (1 - y) * TopRight.position.y;
            count++;

        }
        if (count< maxSpawns) {
            Debug.Log("spawned " + count);
            Instantiate(spawn, new Vector3(x, y, 0), Quaternion.identity);
        } else {
            Debug.Log("No spawn");
        }

    }

    private void OnDisable()
    {
        _emitter.Stop();
    }



}
