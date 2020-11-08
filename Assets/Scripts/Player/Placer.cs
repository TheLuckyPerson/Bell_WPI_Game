using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placer : Player
{
    public GameObject placableObject;
    public Queue<Vector3> locationQueue;
    public override void Init()
    {
        locationQueue = new Queue<Vector3>();

    }
    public override void Action(Vector3 dir)
    {
        if(!CollisionDetect(dir, wallLayer)) {
            Vector3 loc = transform.position + dir * Utils.MOVE_SCALE;
            GameObject g = Instantiate(placableObject, loc, Quaternion.identity);
            player_Manager.shooter.locationQueue.Enqueue(g.transform);
        }
    }

    public override void AiControl()
    {
        if(locationQueue.Count > 0) {
            Vector3 vAction = CheckNear(locationQueue.Peek());
            if(vAction != Vector3.zero) { // near a target
                Action(vAction);
                locationQueue.Dequeue();
            } else {
                MoveTowards(locationQueue.Peek());
            }
        }
    }
}
