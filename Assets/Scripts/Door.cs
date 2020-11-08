using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetOpen()
    {
        gameObject.layer = 12;
        sprite.color = new Color(1,1,1,.25f);
    }

    public void SetClose()
    {
        gameObject.layer = 8;
        sprite.color = new Color(1,1,1,1f);
    }
}
