using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopUpScoreScript : MonoBehaviour {
    public float secondsToDestroy;
    private int scorePoints;
    private TextMeshPro text;
    public NPC_Score_Settings scoreSettings;
    public string _typeNPC;
    public bool minusScore = false;

    private void Start()
    {

        text = GetComponentInChildren<TextMeshPro>();

        switch (_typeNPC)
        {
            case "Cup":
                scorePoints = scoreSettings.scoreForCup;
                break;
            case "Cockroach":
                scorePoints = scoreSettings.scoreForCockroach;
                break;
            case "Panties":
                scorePoints = scoreSettings.scoreForPanties;
                break;
        }


        if (minusScore) {
            text.text = "-" + scorePoints.ToString();
            scorePoints = -scorePoints;
        } else {
            text.text = "+" + scorePoints.ToString();
        }
        GameController.Instance.SetNewScore(scorePoints);
        StartCoroutine(waitToDestroy());
    }

    IEnumerator waitToDestroy()
    {
        yield return new WaitForSeconds(secondsToDestroy);
        Destroy(this.gameObject);
    }
}
