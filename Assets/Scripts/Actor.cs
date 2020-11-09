using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    private const float COLLISION_CONTACT_OFFSET = .02f; 
    public LayerMask wallLayer;
    public LayerMask movableLayer;
    public Player_Manager player_Manager;

    // Start is called before the first frame update
    void Start()
    {
        player_Manager = GameObject.FindGameObjectWithTag("Player_Manager").GetComponent<Player_Manager>();
        Init();
    }

    public virtual void Init()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
        attempt to move in dir
        if can @return true
        cant @return false
    */
    public virtual bool MoveInDir(Vector3 dir) {
        player_Manager.targetList.Add(transform, transform.position+dir*Utils.MOVE_SCALE);
        if(!CollisionDetect(dir, wallLayer)) {
            transform.position += Utils.MOVE_SCALE * dir;
            return true;
        } else {
            Transform c = CollisionDetect(dir, movableLayer);
            if(c) {
                if(c.GetComponent<Movable>().MoveInDir(dir)) {
                    transform.position += Utils.MOVE_SCALE * dir;
                }
            }
        }
        return false;
    }

    /* 
        raycast from all 4 edge colliders in @param dir for @param dist 
        @return min dist of collision or -1 if not found 
     */
    public Transform CollisionDetect(Vector3 dir, LayerMask layer)
    {
        Collider2D c = Physics2D.OverlapPoint(transform.position+dir*Utils.MOVE_SCALE, layer);
        if (c) { // a collider detects a valid prediction
            return c.transform;
        }
        foreach(KeyValuePair<Transform,Vector3> t in player_Manager.targetList) {
            if(t.Value == transform.position+dir*Utils.MOVE_SCALE && t.Key != transform && layer == (layer | (1<<transform.gameObject.layer))) return t.Key;
        }
        return null;
    }

    public virtual void AutoAct()
    {

    }
}
