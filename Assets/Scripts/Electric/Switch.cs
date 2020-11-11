using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : Electric
{
    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D col) {
        if(col.tag == "Placer" || col.tag == "Shooter")
        {
            OnTrigger();
        } else if(col.tag == "Bullet") {
            OnTrigger();
            Destroy(col.gameObject);
        }
    }

    private void OnTrigger()
    {
        state = !state;
        if(state) {
            foreach(Electric w in otherElec) {
            w.TurnOn(state);
            }
        } else {
            foreach(Electric w in otherElec) {
                w.TurnOn(state);
            }
        }
    }

    public override void TurnOn(bool b) {}
}
