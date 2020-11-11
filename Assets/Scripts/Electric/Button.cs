using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Electric
{
    public bool prevState = true;
    public LayerMask activeLay;

    // Start is called before the first frame update
    public override void Init()
    {
        prevState = !state;
    }

    // Update is called once per frame
    void Update()
    {
        if(inited) {
            state = Physics2D.OverlapPoint(transform.position, activeLay);
            if(state != prevState) {
                if(state) {
                    foreach(Electric w in otherElec) {
                        w.TurnOn(true);
                    }
                    prevState = state;
                } else {
                    foreach(Electric w in otherElec) {
                        w.TurnOn(false);
                    }
                    prevState = state;
                }
            }
        }
    }

    public override void TurnOn(bool b) {}
}
