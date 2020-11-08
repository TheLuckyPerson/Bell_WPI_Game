using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Placer : Player
{
    public GameObject placableObject;
    public Queue<Vector3> locationQueue;
    public TextMeshProUGUI blockNumText;
    public int blockNum = 5;
    public LayerMask holeLayer;
    public LayerMask nonPlacableLayer;
    public override void Init()
    {
        locationQueue = new Queue<Vector3>();

    }
    public override void Action(Vector3 dir)
    {
        if(blockNum > 0) {
            Transform hole = CollisionDetect(dir, holeLayer);
            if(!hole && !CollisionDetect(dir, nonPlacableLayer)) {
                Vector3 loc = transform.position + dir * Utils.MOVE_SCALE;
                GameObject g = Instantiate(placableObject, loc, Quaternion.identity);
                player_Manager.shooter.locationQueue.Enqueue(g.transform);
                AddBlocks(-1);
            } else if(hole) {
                Hole h = hole.GetComponent<Hole>();
                GameObject g = Instantiate(placableObject, h.transform.position, Quaternion.identity);
                g.SetActive(false); // DOING SOME REALLY SKETCH STUFF
                h.FillHole();
                h.sketchObj = g;
                player_Manager.shooter.locationQueue.Enqueue(g.transform);
            }
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

    public void AddBlocks(int amt)
    {
        blockNum += amt;
        blockNumText.text = blockNum.ToString();
    }
}
