using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Momentum : Destroyable
{
    // Start is called before the first frame update
    public override void Init()
    {
        base.Init();
        player_Manager.autoAct.Add(this);
        transform.GetChild(0).rotation = Quaternion.Euler(0,0,Mathf.Atan2(-dir.x,dir.y) * Mathf.Rad2Deg);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void AutoAct()
    {
        if(gameObject.activeSelf) {
            MoveInDir(dir);
            TestHole();
        }
    }

    public override void DestroyBlock()
    {
        if(!beingDestroyed) {
            beingDestroyed = true;
            Vector3 v = transform.position;
            v.z = typeId; // store type id in queue
            player_Manager.placer.locationQueue.Enqueue(v);
            player_Manager.placer.AddBlocks(1, typeId);
            Destroy(gameObject);
        }
    }
}
