using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : Actor
{
    public LayerMask holeLayer;
    public int typeId;
    public Vector3 dir;
    // Start is called before the first frame update
    public override void Init()
    {
        TestHole();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void DestroyBlock()
    {
        Vector3 v = transform.position;
        v.z = typeId; // store type id in queue
        player_Manager.placer.locationQueue.Enqueue(v);
        player_Manager.placer.AddBlocks(1, typeId);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Bullet") {
            DestroyBlock();
            Destroy(col.gameObject);
        }
    }

    protected void TestHole()
    {
        Transform hole = CollisionDetect(Vector3.zero, holeLayer);
        if(hole) {
            Hole h = hole.GetComponent<Hole>();
            h.typeId = typeId;
            gameObject.SetActive(false); // DOING SOME REALLY SKETCH STUFF
            h.FillHole();
            h.sketchObj = gameObject;
        }
    }
}
