using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movable : Destroyable
{
    /*
        attempt to move in dir
        if can @return true
        cant @return false
    */
    public override bool MoveInDir(Vector3 dir) {
        if(!CollisionDetect(dir, wallLayer)) {
            transform.position += Utils.MOVE_SCALE * dir;
            TestHole();
            return true;
        }
        return false;
    }
}
