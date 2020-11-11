using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Electric
{
    Transform child;
    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Init()
    {
        child = transform.GetChild(0);
    }

    public void SetOpen()
    {
        child.gameObject.layer = 12;
        gameObject.tag = "Untagged";
        child.gameObject.SetActive(false);
        sprite.color = new Color(1,1,1,.25f);
    }

    public void SetClose()
    {
        gameObject.tag = "Door";
        child.gameObject.layer = 8;
        child.gameObject.SetActive(true);
        sprite.color = new Color(1,1,1,1f);
    }

    public override void TurnOn(bool b)
    {
        if(b!=state) {
            sprite.color = b ? onColor : offColor;
            state = b;
            if(b) SetOpen();
            else SetClose();
            foreach(Electric w in otherElec) {
                if(w is Door && w.state != b)
                    w.TurnOn(b);
            }
        }
    }
}
