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


    public float Radius = 1.5f;

    public float Speed;

    public Player_Fmod_Events PlayerSounds;


    Vector2 movementSpeed;

    public Vector2 MovementSpeed {
        get {
            return movementSpeed;
        }

        set {
            movementSpeed = value;
            Speed = Mathf.Min(movementSpeed.magnitude / MaxMovementAcceleration,1);
            PlayerSounds.SetIdleSound(Speed);
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




    int mood;

    public int Mood {
        get {
            return mood;
        }

        set {
            //Debug.Log( "  new value is: " + value);
            GameController.Instance.Balance = value;
            mood = value;
        }
    }

    bool canBumpSound = true;
    float bumptTime = 0.2f;

    bool canTurnSound=true;

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

        if (canTurnSound && Mathf.Abs(RotationSpeed)>0.4f*MaxRotationAcceleration) {
            canTurnSound = false;
            PlayerSounds.TurningSound();
        } else if (!canTurnSound && Mathf.Abs(RotationSpeed) < 0.2f * MaxRotationAcceleration) {
            canTurnSound = true;
        }
    }

    void HandleMovement() {
        Vector3 newPossition= this.transform.position + new Vector3(MovementSpeed.x * Time.deltaTime, MovementSpeed.y * Time.deltaTime, 0);
        if (CheckMovement(newPossition)) {
            this.transform.position = newPossition;
        } else {
            if (canBumpSound) {
                canBumpSound = false;
                PlayerSounds.BumpSound();
                Invoke("CanBumpAgain", bumptTime);
            }
        }
    }

    void CanBumpAgain() {
        canBumpSound = true;
    }

    // TODO Check if not going into walls
    bool CheckMovement(Vector3 newPosition) {


        if (!CheckPoint(newPosition, new Vector3(1, 0, 0))) {
            return false;
        }
        if (!CheckPoint(newPosition, new Vector3(1, 1, 0))) {
            return false;
        }
        if (!CheckPoint(newPosition, new Vector3(0, 1, 0))) {
            return false;
        }
        if (!CheckPoint(newPosition, new Vector3(-1, 1, 0))) {
            return false;
        }
        if (!CheckPoint(newPosition, new Vector3(-1, 0, 0))) {
            return false;
        }
        if (!CheckPoint(newPosition, new Vector3(-1, -1, 0))) {
            return false;
        }
        if (!CheckPoint(newPosition, new Vector3(0, -1, 0))) {
            return false;
        }
        if (!CheckPoint(newPosition, new Vector3(1, -1, 0))) {
            return false;
        }
        return true;
        
        /*


        Vector3 direction = newPosition - this.transform.position;
        direction = direction.normalized * Radius;
        direction = this.transform.position + direction + new Vector3(0,0,-10);


        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(direction, new Vector3(0,0,1), out hit, Mathf.Infinity)) {
           BaseCollectible collectible= hit.collider.GetComponent<BaseCollectible>();
            if (collectible!=null) {
                return true;
            } else {
                Debug.Log(transform.position + " " + direction);
                MovementSpeed = Vector2.zero;
                return false;
            }
        } else {
            return true;
        }
        return true;
        */
    }

    bool CheckPoint(Vector3 pos, Vector3 dir) {
        dir.Normalize();
        dir = dir * Radius;
        return (CheckPoint(pos + dir));
    }

    bool CheckPoint(Vector3 pos) {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(pos + new Vector3(0,0,-10), new Vector3(0, 0, 1), out hit, 15)) {
            BaseCollectible collectible = hit.collider.GetComponent<BaseCollectible>();
            if (collectible != null) {
                return true;
            } else {
                MovementSpeed = Vector2.zero;
                return false;
            }
        } else {
            return true;
        }

    }


    void HandleRotation() {
        this.transform.rotation = Quaternion.Euler(0,0, Rotation + Time.deltaTime * RotationSpeed);
    }

}
