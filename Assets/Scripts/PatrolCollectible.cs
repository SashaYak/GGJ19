using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolCollectible : BaseCollectible {

    public Vector3[] Positions;
    int pos = 0;

    public int maxCollisions = 5;

    protected override void Init() {
        TargetPosition = Positions[pos];
    }

    protected override void CalculateTarget() {
        if ((TargetPosition-this.transform.position).magnitude<MinDist) {
            NextTarget();
        }

    }

    public void NextTarget() {
        pos++;
        pos = pos % Positions.Length;
        TargetPosition = Positions[pos];
        collisionCount = 0;
    }

    int collisionCount = 0;
    protected override int HandleMovement() {
        int returnVal= base.HandleMovement();
        if (returnVal==0) {
            collisionCount = 0;
            return 0;
        } else {
            if (collisionCount==0) {
                Sound.BumpSound();
            }
            collisionCount+= returnVal;
            //Debug.Log(collisionCount);
            if (collisionCount > maxCollisions*3) {
                NextTarget();
            }
            return returnVal;

        }
        Vector3 newPossition = this.transform.position + new Vector3(MovementSpeed.x * Time.deltaTime, MovementSpeed.y * Time.deltaTime, 0);
        if (CheckMovement(newPossition)) {
            this.transform.position = newPossition;
            collisionCount = 0;
        } else {
            collisionCount++;
            if (collisionCount>maxCollisions) {
                NextTarget();
            }
        }
    }

}
