using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: update sprite for each hole type
public class Hole : MonoBehaviour
{
    Player_Manager players;
    BoxCollider2D boxCollider2D;
    SpriteRenderer spriteRenderer;
    public Color filledCol; // temp var
    public GameObject sketchObj;
    public int typeId;
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
        transform.GetChild(0).gameObject.SetActive(false);
        spriteRenderer.color = filledCol;
        filled = true;
    }

    public void DestroyHole()
    {
        gameObject.layer = 10;
        transform.GetChild(0).gameObject.SetActive(true);
        spriteRenderer.color = new Color(0,0,0,.25f);
        filled = false;
        players.placer.AddBlocks(1, typeId);
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
