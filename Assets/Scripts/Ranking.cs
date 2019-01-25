using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class Ranking : MonoBehaviour {

    public TextMeshProUGUI scoreNumberText;

    private int rank;
    public float timerForAnimation = 1f;

    public Color badColor;
    public Color goodColor;
    public Color rankColor;

    private Image[] rankingImages;



	void Start () {

        PlayerPrefs.SetInt("Score", 30005);
        scoreNumberText.text = PlayerPrefs.GetInt("Score").ToString();

        rank = Mathf.Clamp(PlayerPrefs.GetInt("Score") / 100, 0, 9);
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
        }
        yield return new WaitForSeconds(time);
        rankingImages[rank].color = rankColor;
    }

}
