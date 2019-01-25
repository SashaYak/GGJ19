using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : PlayerCollidor {

    protected override void OnCollisionEnter(Collision collision) {

        Debug.Log(collision);
        BaseCollectible collectible = collision.gameObject.GetComponent<BaseCollectible>();
        if (collectible != null) {
            float maxSpeed = Mathf.Max(Player.RotationSpeed*2, Player.MovementSpeed.magnitude*5f);
            Vector3 direction = collectible.transform.position - collision.contacts[0].point;
            direction = direction.normalized * maxSpeed;
            collectible.Kick(new Vector2(direction.x, direction.y));
        }
    }
}
