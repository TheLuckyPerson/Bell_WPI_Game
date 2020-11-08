using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyable : MonoBehaviour
{
    Placer placer;
    // Start is called before the first frame update
    void Start()
    {
        placer = GameObject.FindGameObjectWithTag("Placer").GetComponent<Placer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Bullet") {
            placer.locationQueue.Enqueue(transform.position);
            placer.AddBlocks(1);
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }
}
