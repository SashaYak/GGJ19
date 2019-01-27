using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCollectible : MonoBehaviour {


    public GameObject PopUpPrefab;

    public CollectibleType Type;

    public int MoodModifier = 1;
    public float TimeModifier = 2f;


    public float MovementAcceleration=3;
    public float MovementDeceleration=2.5f;
    public float MaxMovementAcceleration=3;
    public float MinMovement = 0.5f;
    public float MinDist = 0.1f;

    public Vector3 TargetPosition;

    public float Radius = 0.1f;

    public bool TrySlide = true;

    protected NPC_FMOD_Events Sound;

    protected bool isBad = false;

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
        Sound = GetComponent<NPC_FMOD_Events>();
    }

    protected virtual void Init() {
        TargetPosition = this.transform.position;
    }


    public virtual  void Collect() {
        Debug.Log(name + " collected ");
        Sound.DeathSound();

        GameObject popUp = Instantiate(PopUpPrefab, transform.position, Quaternion.identity);
        PopUpScoreScript popp = popUp.GetComponent<PopUpScoreScript>();
        popp._typeNPC = Type.ToString();
        popp.minusScore = isBad;

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

    protected virtual int HandleMovement() {
        Vector3 newPossition = this.transform.position + new Vector3(MovementSpeed.x * Time.deltaTime, MovementSpeed.y * Time.deltaTime, 0);
        if (CheckMovement(newPossition)) {
            this.transform.position = newPossition;
            return 0;
        } else {
            //TODO make slide work!
            if (TrySlide) {
                float speed = MovementSpeed.magnitude;
                if (Mathf.Abs(MovementSpeed.x) > Mathf.Abs(MovementSpeed.y)) {
                    newPossition = this.transform.position + new Vector3(speed * Time.deltaTime, -Mathf.Sign(MovementSpeed.y)*speed/2*Time.deltaTime*0, 0);
                    if (CheckMovement(newPossition)) {
                        this.transform.position = newPossition;
                        //Debug.Log("case 1");
                        return 1;

                    } else {
                        newPossition = this.transform.position + new Vector3(-Mathf.Sign(MovementSpeed.x) * speed / 2 * Time.deltaTime*0, speed * Time.deltaTime, 0);
                        if (CheckMovement(newPossition)) {
                            this.transform.position = newPossition;
                            //Debug.Log("case 2");
                            return 1;

                        }
                    }
                } else {
                    newPossition = this.transform.position + new Vector3(-Mathf.Sign(MovementSpeed.x) * speed / 2 * Time.deltaTime*0, speed * Time.deltaTime, 0);
                    if (CheckMovement(newPossition)) {
                        this.transform.position = newPossition;
                        //Debug.Log("case 3");
                        return 1;

                    } else {
                        newPossition = this.transform.position + new Vector3(speed * Time.deltaTime, -Mathf.Sign(MovementSpeed.y) * speed / 2 * Time.deltaTime*0, 0);
                        if (CheckMovement(newPossition)) {
                            this.transform.position = newPossition;
                            //Debug.Log("case 4");
                            return 1;

                        }
                    }

                }
            }
        }
        MovementSpeed = Vector2.zero;
        return 3;
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
        } else {
            KickSpeed = Vector2.zero;
        }
    }

    public virtual void Kick(Vector2 kick) {
        KickSpeed = kick;
        kick=kick.normalized*MaxMovementAcceleration*Time.deltaTime*3;
        transform.position += new Vector3(kick.x, kick.y, 0);

    }


    // TODO Check if not going into walls
    protected bool CheckMovement(Vector3 newPosition) {


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

    protected bool CheckPoint(Vector3 pos, Vector3 dir) {
        dir.Normalize();
        dir = dir * Radius;
        return (CheckPoint(pos + dir) && CheckPoint2D(pos,dir));
    }

    bool CheckPoint2D(Vector3 pos, Vector3 dir) {
        RaycastHit2D hit = Physics2D.Raycast(pos, dir, Radius*0.75f);
        if (hit.collider != null) {
            Debug.Log(hit.collider.gameObject.name + " was hit");
            return false;
        }
        return true;
    }

    protected bool CheckPoint(Vector3 pos) {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(pos + new Vector3(0, 0, -10), new Vector3(0, 0, 1), out hit, 15)) {
            PlayerCollidor collectible = hit.collider.GetComponent<PlayerCollidor>();
            BaseCollectible col= hit.collider.GetComponent<BaseCollectible>();
            if (collectible != null || col==this) {
                return true;
            } else {
                return false;
            }
        } else {
            return true;
        }

    }

    public void SetBad(bool bad) {
        isBad = bad;
    }
}


public enum CollectibleType {
    Cup,
    Cockroach,
    Panties
}