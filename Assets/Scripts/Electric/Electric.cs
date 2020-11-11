using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electric : MonoBehaviour
{
    public Color offColor;
    public Color onColor;
    protected SpriteRenderer sprite;
    private List<Vector3> fourDir;
    protected List<Electric> otherElec;
    public LayerMask wireLayer;
    public bool state;
    public bool inited;
    // Start is called before the first frame update
    
    void Start()
    {
        StartCoroutine(lateStart());
    }

    public IEnumerator lateStart()
    {
        yield return new WaitForSeconds(.5f);
        sprite = GetComponent<SpriteRenderer>();
        otherElec = new List<Electric>();
        fourDir = new List<Vector3>();
        fourDir.Add(Vector3.right*Utils.MOVE_SCALE);
        fourDir.Add(Vector3.up*Utils.MOVE_SCALE); 
        fourDir.Add(Vector3.left*Utils.MOVE_SCALE);
        fourDir.Add(Vector3.down*Utils.MOVE_SCALE);
        CheckNear();
        Init();
        inited = true;
    }

    public virtual void Init()
    {

    }

    public virtual void TurnOn(bool b)
    {
        if(b!=state) {
            sprite.color = b ? onColor : offColor;
            state = b;
            foreach(Electric w in otherElec) {
                if(w.state != b)
                    w.TurnOn(b);
            }
        }
    }

    public void CheckNear()
    {
        foreach(Vector3 v in fourDir) {
            Collider2D c = Physics2D.OverlapPoint(transform.position + v, wireLayer);
            if(c)
                otherElec.Add(c.GetComponent<Electric>());
        }
    }
}
