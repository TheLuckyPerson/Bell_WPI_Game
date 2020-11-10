using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Destroyable
{
    // Update is called once per frame
    void Update()
    {
        
    }

    public override void DestroyBlock()
    {
        player_Manager.AddKeys(1);
        Destroy(gameObject);
    }
}
