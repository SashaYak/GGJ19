using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using FMOD;
using FMODUnity;
using UnityEngine.UI;

public class Ranking : MonoBehaviour {

   // public int ScoreTest;
   

    public TextMeshProUGUI scoreNumberText;
    public GameObject Texts;

    private TextMeshProUGUI[] _texts;
    private int rank;
    public float timerForAnimation = 1f;

    public RectTransform heads;

    private Image[] rankingImages;
    private StudioEventEmitter emitter;


	void Start () {

        _texts = Texts.GetComponentsInChildren<TextMeshProUGUI>();

        emitter = GetComponent<StudioEventEmitter>();
      //  PlayerPrefs.SetInt("Score", ScoreTest);
        scoreNumberText.text = PlayerPrefs.GetInt("Score").ToString();

        rank = Mathf.Clamp((PlayerPrefs.GetInt("Score") / 100 -1), 0, 9);
       
        StartCoroutine(goRanking(timerForAnimation));



	}

    public IEnumerator goRanking(float time)
    {
        yield return new WaitForSeconds(1.5f);

        for (int i = 0; i <= rank; i++)
        {
            yield return new WaitForSeconds(time);
            heads.transform.localPosition += new Vector3(0f, 30f, 0f);
            string eventPath = "event:/SFX/RankingButtons";
            if (PlayerPrefs.GetInt("FmodOn") > 0 && FMOD_Debug.CheckFmodEvent(eventPath))
            {
                RuntimeManager.PlayOneShot(eventPath, transform.position);
            }
        }
        yield return new WaitForSeconds(time);
        _texts[rank+1].text = "<b>" + _texts[rank+1].text;


        if (rank < 4)
        {
            string eventPath = "event:/SFX/RankBad";
            if (PlayerPrefs.GetInt("FmodOn") > 0 && FMOD_Debug.CheckFmodEvent(eventPath))
            {
                RuntimeManager.PlayOneShot(eventPath, transform.position);
            }
        }
        else if (rank > 8)
        {
            string eventPath = "event:/SFX/RankAmazing";
            if (PlayerPrefs.GetInt("FmodOn") > 0 && FMOD_Debug.CheckFmodEvent(eventPath))
            {
                RuntimeManager.PlayOneShot(eventPath, transform.position);
            }
        }
        else
        {
            string eventPath = "event:/SFX/RankGood";
            if (PlayerPrefs.GetInt("FmodOn") > 0 && FMOD_Debug.CheckFmodEvent(eventPath))
            {
                RuntimeManager.PlayOneShot(eventPath, transform.position);
            }
        }
    }

}
