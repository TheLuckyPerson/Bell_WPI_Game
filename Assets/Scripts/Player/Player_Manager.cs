using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class Player_Manager : MonoBehaviour
{
    public Player activePl;
    public Player nonactivePl;

    public Placer placer;
    public Shooter shooter;
    public LayerMask antiSwap;
    public List<Actor> autoAct;
    public Dictionary<Transform, Vector3> targetList;
    public TextMeshProUGUI keyText;
    public LayerMask keyDoorLayer;
    public CameraCon cam;
    public Vector3 spawn;
    public int keys;
    public bool canSwap = true;

    public bool canMove = true;

    // Start is called before the first frame update
    void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraCon>();
        targetList = new Dictionary<Transform, Vector3>();
        autoAct = new List<Actor>();

        cam.player = activePl.transform;
        spawn = activePl.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove) {
            AddKeys(0);
            activePl.Movement();
            activePl.ActionControl();
            nonactivePl.Targeter();
            Swap();
            CheckWin();
            if(Input.GetKeyDown("r")) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    public void CheckWin()
    {
        if(!activePl.swapable && (!nonactivePl.swapable || !nonactivePl.gameObject.activeSelf)) {
            Debug.Log("You Win!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }

    public void Swap()
    {
        if(Input.GetKeyDown("space") && !Physics2D.OverlapPoint(activePl.transform.position, antiSwap) 
        && !Physics2D.OverlapPoint(nonactivePl.transform.position, antiSwap) && activePl.swapable && canSwap) {
            if(activePl == placer) {
                SetActivePlayer(shooter, placer);
            } else {
                SetActivePlayer(placer, shooter);
            }
        }

        if(!activePl.swapable && nonactivePl.gameObject.activeSelf) {
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
        cam.player = activePl.transform;
    }

    public void AddKeys(int amt)
    {
        keys+= amt;
        keyText.text = keys.ToString();
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
        targetList = new Dictionary<Transform, Vector3>();
    }
}
