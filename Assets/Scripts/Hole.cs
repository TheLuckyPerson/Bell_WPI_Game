using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    Player_Manager players;
    BoxCollider2D boxCollider2D;
    SpriteRenderer spriteRenderer;
    public Color filledCol; // temp var
    public GameObject sketchObj;
    bool filled;
    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectWithTag("Player_Manager").GetComponent<Player_Manager>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FillHole()
    {
        gameObject.layer = 0;
        spriteRenderer.color = filledCol;
        filled = true;
        players.placer.AddBlocks(-1);
    }

    public void DestroyHole()
    {
        gameObject.layer = 10;
        spriteRenderer.color = new Color(0,0,0,.25f);
        filled = false;
        players.placer.AddBlocks(1);
        players.placer.locationQueue.Enqueue(transform.position);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(filled && col.tag == "Bullet") {
            DestroyHole();
            Destroy(col.gameObject);
            Destroy(sketchObj);
        }
    }
}
