using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player_Manager : MonoBehaviour
{
    public Player activePl;
    public Player nonactivePl;

    public Placer placer;
    public Shooter shooter;
    public LayerMask antiSwap;
    public List<Actor> autoAct;

    // Start is called before the first frame update
    void Awake()
    {
        autoAct = new List<Actor>();
        activePl = placer;
        nonactivePl = shooter;
    }

    // Update is called once per frame
    void Update()
    {
        activePl.Movement();
        activePl.ActionControl();
        nonactivePl.Targeter();
        Swap();
        if(Input.GetKeyDown("r")) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void Swap()
    {
        if(Input.GetKeyDown("space") && !Physics2D.OverlapPoint(activePl.transform.position, antiSwap) && !Physics2D.OverlapPoint(nonactivePl.transform.position, antiSwap) && activePl.swapable) {
            if(activePl == placer) {
                SetActivePlayer(shooter, placer);
            } else {
                SetActivePlayer(placer, shooter);
            }
        }

        if(!activePl.swapable) {
            SetActivePlayer(nonactivePl, activePl);
        }
    }

    private void SetActivePlayer(Player p, Player np)
    {
        activePl = p;
        nonactivePl = np;
        activePl.arrow.gameObject.SetActive(false);
        activePl.ActiveSprite(true);
        nonactivePl.ActiveSprite(false);
    }

    public void OnMoveUpdate()
    {
        if(nonactivePl.swapable) {
            nonactivePl.AiControl();
        }
        for(int i = 0; i < autoAct.Count; i++)
        {
            if(autoAct[i])  
                autoAct[i].AutoAct();
            else
                autoAct.RemoveAt(i);
        }
    }
}
