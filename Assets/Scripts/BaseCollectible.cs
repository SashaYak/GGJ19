using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCollectible : MonoBehaviour {

    public float MovementAcceleration=3;
    public float MovementDeceleration=2.5f;
    public float MaxMovementAcceleration=3;
    public float MinMovement = 0.5f;
    public float MinDist = 0.1f;

    public Vector3 TargetPosition;

    Vector2 movementSpeed;

    public Vector2 MovementSpeed {
        get {
            return movementSpeed;
        }

        set {
            movementSpeed = value;
        }
    }

    Vector2 kickSpeed;

    public Vector2 KickSpeed {
        get {
            return kickSpeed;
        }

        set {
            kickSpeed = value;
        }
    }

    private void Awake() {
        Init();
    }

    protected virtual void Init() {
        TargetPosition = this.transform.position;
    }

    public float MoodModifier = 1;

    public virtual  void Collect() {
        Debug.Log(name + " collected ");
        Destroy(this.gameObject);
    }

    private void Update() {
        CalculateTarget();
        CalculateMovement();
        HandleMovement();
        CalculateKick();
        HandleKick();
    }

    protected virtual void CalculateTarget() {

    }


    protected virtual void CalculateMovement() {
        Vector3 direction = TargetPosition - this.transform.position;

        if ( MovementSpeed.magnitude > direction.magnitude || movementSpeed.magnitude> MaxMovementAcceleration) {
            MovementSpeed = MovementSpeed.normalized * Mathf.Min( Mathf.Max(MinMovement, direction.magnitude), MaxMovementAcceleration);
        }
        Vector2 input = new Vector2(direction.x, direction.y).normalized;
        MovementSpeed += input * Time.deltaTime * MovementAcceleration;

    }

    protected virtual void HandleMovement() {
        Vector3 newPossition = this.transform.position + new Vector3(MovementSpeed.x * Time.deltaTime, MovementSpeed.y * Time.deltaTime, 0);
        if (CheckMovement(newPossition)) {
            this.transform.position = newPossition;
        }
    }


    protected virtual void CalculateKick() {
        if (KickSpeed.magnitude > 0) {
            KickSpeed -= KickSpeed.normalized * Time.deltaTime * MovementDeceleration;
            if ((KickSpeed.magnitude) <= Time.deltaTime * MovementDeceleration) {
                KickSpeed = Vector2.zero;
            }
        }
    }

    protected virtual void HandleKick() {
        Vector3 newPossition = this.transform.position + new Vector3(KickSpeed.x * Time.deltaTime, KickSpeed.y * Time.deltaTime, 0);
        if (CheckMovement(newPossition)) {
            this.transform.position = newPossition;
        }
    }

    public virtual void Kick(Vector2 kick) {
        KickSpeed = kick;
        kick=kick.normalized*MaxMovementAcceleration*Time.deltaTime*3;
        transform.position += new Vector3(kick.x, kick.y, 0);

    }


    // TODO Check if not going into walls
    protected virtual bool CheckMovement(Vector3 newPosition) {
        return true;
    }
}
