using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float MovementAcceleration;
    public float MovementDeceleration;
    public float MaxMovementAcceleration;




    public float RotationAcceleration;
    public float RotationDeceleration;
    public float MaxRotationAcceleration;



    Vector2 movementSpeed;

    public Vector2 MovementSpeed {
        get {
            return movementSpeed;
        }

        set {
            movementSpeed = value;
        }
    }


    float rotationSpeed;


    public float RotationSpeed {
        get {
            return rotationSpeed;
        }

        set {
            rotationSpeed = value;
        }
    }




    public float Rotation {
        get {
            return this.transform.rotation.eulerAngles.z;
        }
    }




    float mood;

    public float Mood {
        get {
            return mood;
        }

        set {
            Debug.Log( "  new value is: " + value);
            mood = value;
        }
    }



    // Update is called once per frame
    void Update () {
        HandleInput();
        HandleMovement();
        HandleRotation();
	}

    public void Collect(BaseCollectible collectible) {

    }


    void HandleInput() {
        CalculateMovement();
        CalculateRotation();
    }

    void CalculateMovement() {
        Vector2 input = Vector2.zero;

        if (MovementSpeed.magnitude > MaxMovementAcceleration) {
            MovementSpeed = MovementSpeed.normalized * (MaxMovementAcceleration- Time.deltaTime * MovementAcceleration);
        }

        if (Input.GetKey(KeyCode.W)) { // can be changed going up
            input += Vector2.up;
        }
        if (Input.GetKey(KeyCode.S)) { // can be changed going down
            input += Vector2.down;
        }
        if (Input.GetKey(KeyCode.D)) { // can be changed going right
            input += Vector2.right;
        }
        if (Input.GetKey(KeyCode.A)) { // can be changed going left
            input += Vector2.left;
        }

        if (input!=Vector2.zero) {
            input.Normalize();
            MovementSpeed += input * Time.deltaTime*MovementAcceleration;
            if (MovementSpeed.magnitude>MaxMovementAcceleration) {
                MovementSpeed = MovementSpeed.normalized * MaxMovementAcceleration;
            }
        } else {
            if (MovementSpeed.magnitude <= Time.deltaTime * MovementDeceleration * 1.1f) {
                MovementSpeed = Vector2.zero;
            } else {
                //Debug.Log(MovementSpeed);
                MovementSpeed -= (MovementSpeed / movementSpeed.magnitude) * Time.deltaTime * MovementDeceleration;
            }
        }
    }

    void CalculateRotation() {
        if (Input.GetKey(KeyCode.K)) { // Can be changed Left turn (counterclockwise)
            RotationSpeed += RotationAcceleration * Time.deltaTime;
        } else if (Input.GetKey(KeyCode.L)) { // Can be changed Right turn (Clockwise)
            RotationSpeed -= RotationAcceleration * Time.deltaTime;
        } else { // no Input
            RotationSpeed -= Mathf.Sign(RotationSpeed) * RotationDeceleration * Time.deltaTime;
            if (Mathf.Abs(RotationSpeed) <= RotationDeceleration * Time.deltaTime * 1.1f) {
                RotationSpeed = 0;
            }
        }
        RotationSpeed = Mathf.Clamp(RotationSpeed, -MaxRotationAcceleration, MaxRotationAcceleration);

    }

    void HandleMovement() {
        Vector3 newPossition= this.transform.position + new Vector3(MovementSpeed.x * Time.deltaTime, MovementSpeed.y * Time.deltaTime, 0);
        if (CheckMovement(newPossition)) {
            this.transform.position = newPossition;
        }
    }

    // TODO Check if not going into walls
    bool CheckMovement(Vector3 newPosition) {
        return true;
    }

    void HandleRotation() {
        this.transform.rotation = Quaternion.Euler(0,0, Rotation + Time.deltaTime * RotationSpeed);
    }

}
