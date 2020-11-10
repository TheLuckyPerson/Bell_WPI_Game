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
        base.Init();
        locationQueue = new Queue<Transform>();
        StartCoroutine(LateStart());
    }
    
    IEnumerator LateStart()
    {
        player_Manager.canMove = false;
        yield return new WaitForSeconds(.1f);
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("Destroyable")) {
            locationQueue.Enqueue(g.transform);
        }
        player_Manager.canMove = true;
    }

    public override void Action(Vector3 dir)
    {
        if(gameObject.activeSelf) {
            base.Action(dir);
            GameObject g = Instantiate(bullet, transform.position + dir * Utils.MOVE_SCALE, Quaternion.identity);
            g.GetComponent<Bullet>().dir = dir;
        }
    }

    public override void AiControl()
    {
        if(gameObject.activeSelf && locationQueue.Count > 0) {
            if(!locationQueue.Peek()) {
                locationQueue.Dequeue();
            } else {
                Vector3 vAction = CheckNear(locationQueue.Peek().position);
                if(vAction != Vector3.zero) { // found target
                    Action(vAction);
                } else {
                    MoveTowards(locationQueue.Peek().position);
                }
            }
        }
    }

    public override void Targeter()
    {
        if(gameObject.activeSelf) {
            if(locationQueue.Count > 0 && locationQueue.Peek()) {
                targeter.gameObject.SetActive(true);
                targeter.position = locationQueue.Peek().position;
            } else {
                base.Targeter();
            }
        } else {
            targeter.gameObject.SetActive(false);
        }
    }
}
