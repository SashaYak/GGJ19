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

    public bool TrySlide = true;


    float Speed;


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
        if (Input.GetKey(KeyCode.LeftArrow)) { // Can be changed Left turn (counterclockwise)
            RotationSpeed += RotationAcceleration * Time.deltaTime;
        } else if (Input.GetKey(KeyCode.RightArrow)) { // Can be changed Right turn (Clockwise)
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

    float bumpslow = 0.9f;
    float backjump = 0.1f;
    float beforeCheck = 0f;
    float minSpeed = 0.0001f;

    int HandleMovement() {
        Vector3 Movement = new Vector3(MovementSpeed.x * Time.deltaTime, MovementSpeed.y * Time.deltaTime, 0);
        Vector3 newPossition= this.transform.position + Movement;
        if (CheckMovement(newPossition + Movement* beforeCheck)) {
            this.transform.position = newPossition;
            return 0;
        } else {
            if (canBumpSound) {
                canBumpSound = false;
                PlayerSounds.BumpSound();
                Invoke("CanBumpAgain", bumptTime);
            }
            if (TrySlide) {
                if (MovementSpeed.x< minSpeed || MovementSpeed.y < minSpeed) {
                    MovementSpeed = Vector2.zero;
                    return 3;
                }
                float speed = MovementSpeed.magnitude;
                if (Mathf.Abs(MovementSpeed.x) > Mathf.Abs(MovementSpeed.y)) {
                    Movement = new Vector3(speed * Time.deltaTime, -Mathf.Sign(MovementSpeed.y) * speed / 2 * Time.deltaTime * backjump, 0);
                    newPossition = this.transform.position+ Movement;
                    if (CheckMovement(newPossition + Movement * beforeCheck)) {
                        this.transform.position = newPossition;
                        //Debug.Log("case 1");
                        MovementSpeed = new Vector2(MovementSpeed.x * bumpslow, 0);
                        return 1;

                    } else {
                        Movement= new Vector3(-Mathf.Sign(MovementSpeed.x) * speed / 2 * Time.deltaTime * backjump, speed * Time.deltaTime, 0);
                        newPossition = this.transform.position + Movement;
                        if (CheckMovement(newPossition + Movement * beforeCheck)) {
                            this.transform.position = newPossition;
                            //Debug.Log("case 2");
                            MovementSpeed = new Vector2(0, MovementSpeed.y * bumpslow);
                            return 1;

                        } else {
                            MovementSpeed = Vector2.zero;
                            return 3;

                        }
                    }
                } else {
                    Movement = new Vector3(-Mathf.Sign(MovementSpeed.x) * speed / 2 * Time.deltaTime * backjump, speed * Time.deltaTime, 0);
                    newPossition = this.transform.position + Movement;
                    if (CheckMovement(newPossition + Movement * beforeCheck)) {
                        this.transform.position = newPossition;
                        //Debug.Log("case 3");
                        MovementSpeed = new Vector2(0, MovementSpeed.y * bumpslow);
                        return 1;

                    } else {
                        Movement = new Vector3(speed * Time.deltaTime, -Mathf.Sign(MovementSpeed.y) * speed / 2 * Time.deltaTime * backjump, 0);
                        newPossition = this.transform.position + Movement;
                        if (CheckMovement(newPossition + Movement * beforeCheck)) {
                            this.transform.position = newPossition;
                            //Debug.Log("case 4");
                            MovementSpeed = new Vector2(MovementSpeed.x* bumpslow, 0);
                            return 1;

                        } else {
                            MovementSpeed = Vector2.zero;
                            return 3;
                        }
                    }

                }
            }
            MovementSpeed = Vector2.zero;
            return 3;
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
        return (CheckPoint(pos + dir)) && CheckPoint2D(pos,dir);
    }

    bool CheckPoint2D(Vector3 pos, Vector3 dir) {
        RaycastHit2D hit = Physics2D.Raycast(pos, dir, Radius*0.75f);
        if ( hit.collider!=null) {
            //Debug.Log(hit.collider.gameObject.name + " was hit");
            return false;
        }
        return true;
    }

    bool CheckPoint(Vector3 pos) {
        RaycastHit hit;
        if (Physics.Raycast(pos + new Vector3(0,0,-10), new Vector3(0, 0, 1), out hit, 15)) {
            BaseCollectible collectible = hit.collider.GetComponent<BaseCollectible>();
            if (collectible != null) {
                return true;
            } else {
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
