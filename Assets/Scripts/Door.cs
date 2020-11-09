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
        gameObject.tag = "Untagged";
        transform.GetChild(0).gameObject.SetActive(false);
        sprite.color = new Color(1,1,1,.25f);
    }

    public void SetClose()
    {
        gameObject.tag = "Door";
        gameObject.layer = 8;
        transform.GetChild(0).gameObject.SetActive(true);
        sprite.color = new Color(1,1,1,1f);
    }
}
