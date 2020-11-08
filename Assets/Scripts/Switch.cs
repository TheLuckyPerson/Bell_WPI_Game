using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool isOn;
    public List<Door> doors;
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
        isOn = !isOn;
            if(isOn) {
                foreach(Door d in doors) {
                    d.SetOpen();
                    sprite.color = new Color(1,1,1,.25f);
                }
            } else {
                foreach(Door d in doors) {
                    d.SetClose();
                    sprite.color = new Color(1,1,1,1f);
                }
            }
    }
}
