using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    public float speed;
    private Vector3 movementVector;
    public Vector3 direction;
    private Rigidbody2D rb2d;
    public Player_Manager player_Manager;

    private List<Vector3> fourDir;
    private SpriteRenderer sprite;
    public Transform targeter;
    
    // Start is called before the first frame update
    void Start()
    {
        fourDir = new List<Vector3>();
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        player_Manager = transform.parent.GetComponent<Player_Manager>();
        fourDir.Add(Vector3.right*Utils.MOVE_SCALE);
        fourDir.Add(Vector3.up*Utils.MOVE_SCALE); 
        fourDir.Add(Vector3.left*Utils.MOVE_SCALE);
        fourDir.Add(Vector3.down*Utils.MOVE_SCALE);
        Init();
    }

    public virtual void Init()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ActiveSprite(bool b)
    {
        if(b) {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
        } else {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, .25f);
        }
    }

    public void Movement()
    {
        if (Input.GetKeyDown("w")) {
            MoveInDir(Vector3.up);
            player_Manager.OnMoveUpdate();
        }
        else if (Input.GetKeyDown("a")) {
            MoveInDir(Vector3.left);
            player_Manager.OnMoveUpdate();
        }
        else if (Input.GetKeyDown("s")) {
            MoveInDir(Vector3.down);
            player_Manager.OnMoveUpdate();
        }
        else if (Input.GetKeyDown("d")) {
            MoveInDir(Vector3.right);
            player_Manager.OnMoveUpdate();
        }
    }


    public void ActionControl()
    {
        if (Input.GetKeyDown("up")) {
            Action(Vector3.up);
            player_Manager.OnMoveUpdate();  
        }
        else if (Input.GetKeyDown("left")) {
            Action(Vector3.left);
            player_Manager.OnMoveUpdate();
        }
        else if (Input.GetKeyDown("down")) {
            Action(Vector3.down);
            player_Manager.OnMoveUpdate();
        }
        else if (Input.GetKeyDown("right")) {
            Action(Vector3.right);
            player_Manager.OnMoveUpdate();
        }
    }

    public virtual void AiControl()
    {

    }

    public virtual void Targeter()
    {
        targeter.gameObject.SetActive(false);
    }


    /*
        attempt to move towards given position
        if can update movement 
        if wall found @return direction attempted
        @return zero vector normally
    */
    public Vector3 MoveTowards(Vector3 pos) 
    {
        Vector3 v;
        if(transform.position.y - pos.y != 0 && (pos.y-transform.position.y < pos.x-transform.position.x || pos.x-transform.position.x == 0)) {
            v = pos.y-transform.position.y > 0 ? Vector3.up : Vector3.down;
            if(!MoveInDir(v)) return v;
        } else if(pos.x-transform.position.x != 0) {
            v = pos.x-transform.position.x > 0 ? Vector3.right : Vector3.left;
            if(!MoveInDir(v)) return v;
        }
        return Vector3.zero;
    }

    public Vector3 CheckNear(Vector3 testPos)
    {
        foreach(Vector3 v in fourDir) {
            if(transform.position + v == testPos) return v;
        }
        return Vector3.zero;
    }

    public virtual void Action(Vector3 dir)
    {
        
    }
}
