﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolCollectible : BaseCollectible {

    public Vector3[] Positions;
    int pos = 0;

    public int maxCollisions = 5;

    public bool RandomRoute = true;
    public float MaxDist = 3;

    protected override void Init() {
        for (int i = 0; i < Positions.Length; i++) {
            if (RandomRoute) {
                Positions[i] = new Vector3(Random.Range(-MaxDist,MaxDist), Random.Range(-MaxDist, MaxDist),0);
            }
            Positions[i] += this.transform.position;
        }
        TargetPosition = Positions[pos];
        Radius = this.transform.localScale.x / 2;
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
