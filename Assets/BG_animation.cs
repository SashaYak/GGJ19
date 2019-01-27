using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG_animation : MonoBehaviour {

	public void EndGame() {

        GameController.Instance.StartCoroutine(GameController.Instance.EndBGAnimation());

     }
}
