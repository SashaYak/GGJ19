using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using FMOD;
using FMODUnity;

public class Ranking : MonoBehaviour {

   // public int ScoreTest;

    public TextMeshProUGUI scoreNumberText;

    private int rank;
    public float timerForAnimation = 1f;

    public Color badColor;
    public Color goodColor;
    public Color rankColor;

    private Image[] rankingImages;
    private StudioEventEmitter emitter;


	void Start () {
        emitter = GetComponent<StudioEventEmitter>();
      //  PlayerPrefs.SetInt("Score", ScoreTest);
        scoreNumberText.text = PlayerPrefs.GetInt("Score").ToString();

        rank = Mathf.Clamp((PlayerPrefs.GetInt("Score") / 100 -1), 0, 9);
        rankingImages = GetComponentsInChildren<Image>();
        foreach(Image im in rankingImages)
        {
            im.color = badColor;
        }


        StartCoroutine(goRanking(timerForAnimation));



	}

    public IEnumerator goRanking(float time)
    {
        for (int i = 0; i <= rank; i++)
        {
            yield return new WaitForSeconds(time);
            rankingImages[i].color = goodColor;
            string eventPath = "event:/SFX/RankingButtons";
            if (PlayerPrefs.GetInt("FmodOn") > 0 && FMOD_Debug.CheckFmodEvent(eventPath))
            {
                RuntimeManager.PlayOneShot(eventPath, transform.position);
            }
        }
        yield return new WaitForSeconds(time);
        rankingImages[rank].color = rankColor;
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
