using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{
    public LayerMask blockingLayer;
    public Transform child;
    // Start is called before the first frame update
    void Start()
    {
        child = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = transform.parent.position;
        RaycastHit2D hit = Physics2D.Raycast(v, transform.right, 100, blockingLayer);
        if(hit) {
            Vector3 l = child.lossyScale;
            l.y = hit.distance+.25f;
            child.localScale = l;
        }
    }
}
