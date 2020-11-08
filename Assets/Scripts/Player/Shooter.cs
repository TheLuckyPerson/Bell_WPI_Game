using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : Player
{   
    public GameObject bullet;
    public Queue<Transform> locationQueue;
    // Start is called before the first frame update
    
    public override void Init()
    {
        locationQueue = new Queue<Transform>();
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Destroyable")) {
            locationQueue.Enqueue(g.transform);
        }
    }

    public override void Action(Vector3 dir)
    {
        GameObject g = Instantiate(bullet, transform.position + dir * Utils.MOVE_SCALE, Quaternion.identity);
        g.GetComponent<Bullet>().dir = dir;
    }

    public override void AiControl()
    {
        if(locationQueue.Count > 0) {
            if(!locationQueue.Peek()) {
                locationQueue.Dequeue();
            } else {
                Vector3 v = MoveTowards(locationQueue.Peek().position);
                
                if(v != Vector3.zero) { // bump into wall
                    Action(v);
                }
            }
        }
    }
}
