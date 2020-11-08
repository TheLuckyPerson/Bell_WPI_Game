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

    // Start is called before the first frame update
    void Start()
    {
        activePl = placer;
        nonactivePl = shooter;
    }

    // Update is called once per frame
    void Update()
    {
        activePl.Movement();
        activePl.ActionControl();
        Swap();
        if(Input.GetKeyDown("r")) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void Swap()
    {
        if(Input.GetKeyDown("space")) {
            if(activePl == placer) {
                SetActivePlayer(shooter, placer);
            } else {
                SetActivePlayer(placer, shooter);
            }
        }
    }

    private void SetActivePlayer(Player p, Player np)
    {
        activePl = p;
        nonactivePl = np;
        activePl.ActiveSprite(true);
        nonactivePl.ActiveSprite(false);
    }
}
