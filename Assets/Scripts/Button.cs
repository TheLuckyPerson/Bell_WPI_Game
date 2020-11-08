using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public bool isOn;
    public List<Door> doors;
    public LayerMask activeLay;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isOn = Physics2D.OverlapPoint(transform.position, activeLay);
        if(isOn) {
            foreach(Door d in doors) {
                d.SetOpen();
            }
        } else {
            foreach(Door d in doors) {
                d.SetClose();
            }
        }
    }
}
