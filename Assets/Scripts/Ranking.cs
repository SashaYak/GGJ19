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

        rank = Mathf.Clamp((PlayerPrefs.GetInt("Score") / 100 ), 0, 9);
       
        StartCoroutine(goRanking(timerForAnimation));

        UnityEngine.Debug.Log(rank);

	}

    public IEnumerator goRanking(float time)
    {
        yield return new WaitForSeconds(1.5f);

        
        for (int i = 0; i < rank; i++)
        {
            if (rank < 10)
            {
                yield return new WaitForSeconds(time);
                heads.transform.localPosition += new Vector3(0f, 30f, 0f);
                string eventPath = "event:/SFX/RankingButtons";
                if (PlayerPrefs.GetInt("FmodOn") > 0 && FMOD_Debug.CheckFmodEvent(eventPath))
                {
                    RuntimeManager.PlayOneShot(eventPath, transform.position);
                }
            }
        }
        yield return new WaitForSeconds(time);
        _texts[rank].text = "<b>" + _texts[rank].text;


        if (rank < 4)
        {
            heads.GetComponentInChildren<Animator>().SetBool("bad", true);
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

            heads.GetComponentInChildren<Animator>().SetBool("great", true);

        }
        else
        {
            heads.GetComponentInChildren<Animator>().SetBool("great", true);
            string eventPath = "event:/SFX/RankGood";
            if (PlayerPrefs.GetInt("FmodOn") > 0 && FMOD_Debug.CheckFmodEvent(eventPath))
            {
                RuntimeManager.PlayOneShot(eventPath, transform.position);
            }
        }
    }

}
