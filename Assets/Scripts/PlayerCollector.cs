using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : PlayerCollidor {

    public int PlayerSign = 1;


    public override void HitCollectible(BaseCollectible collectible) {
        Player.Collect(collectible);
        Player.Mood+=PlayerSign*collectible.MoodModifier;
        collectible.Collect();
    }
}
