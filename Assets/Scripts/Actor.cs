using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    public List<Transform> colliders;
    private const float COLLISION_CONTACT_OFFSET = .02f; 
    public LayerMask wallLayer;

    // Start is called before the first frame update
    void Start()
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
    public bool MoveInDir(Vector3 dir) {
        if(!CollisionDetect(dir, wallLayer)) {
            transform.position += Utils.MOVE_SCALE * dir;
            return true;
        }
        return false;
    }

    /* 
        raycast from all 4 edge colliders in @param dir for @param dist 
        @return min dist of collision or -1 if not found 
     */
    public bool CollisionDetect(Vector3 dir, LayerMask layer)
    {
        foreach (Transform t in colliders) { // loop through all colliders
            if (Physics2D.OverlapPoint(t.position+dir*Utils.MOVE_SCALE, layer)) { // a collider detects a valid prediction
                return true;
            }
        }
        return false;
    }
}
