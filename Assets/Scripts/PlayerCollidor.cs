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

    private void OnCollisionExit(Collision collision) {
        
    }

    protected virtual void OnCollisionEnter(Collision collision) {
        Debug.Log(collision);
        BaseCollectible collectible = collision.gameObject.GetComponent<BaseCollectible>();
        if (collectible!=null) {
            HitCollectible(collectible);
        }
    }

    public virtual void HitCollectible(BaseCollectible collectible) {

    }


}

