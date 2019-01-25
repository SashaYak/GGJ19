using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollidor : MonoBehaviour {


    public Player Player;
    public int ColliderName;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnTriggerEnter(Collider other) {
        BaseCollectible collectible = other.GetComponent<BaseCollectible>();
        if (collectible!=null) {
            HitCollectible(collectible);
        }
    }

    public virtual void HitCollectible(BaseCollectible collectible) {

    }


}

