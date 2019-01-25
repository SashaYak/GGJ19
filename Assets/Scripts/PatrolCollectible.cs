using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolCollectible : BaseCollectible {

    public Vector3[] Positions;
    int pos = 0;

    protected override void Init() {
        TargetPosition = Positions[pos];
    }

    protected override void CalculateTarget() {
        if ((TargetPosition-this.transform.position).magnitude<MinDist) {
            pos++;
            pos = pos % Positions.Length;
            TargetPosition = Positions[pos];
        }

    }

}
