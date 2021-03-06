﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Placer : Player
{
    [Header("Assign")]
    public Transform blockSelector;
    public int selectedId;
    public int blockNum = 5;
    public int blockNumMove = 1;
    public LayerMask holeLayer;
    public LayerMask nonPlacableLayer;
    public Queue<Vector3> locationQueue; // uses z pos as id
    public List<BlockStorage> blockTypes;

    public override void Init()
    {
        base.Init();
        locationQueue = new Queue<Vector3>();
        foreach(BlockStorage b in blockTypes) b.UpdateBlockText();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && this == player_Manager.activePl) {
            UpdateSelectedID(selectedId+1);
        }
    }

    void UpdateSelectedID(int id)
    {
        selectedId = id;
        selectedId%=blockTypes.Count;
        while(blockTypes[selectedId].notExists) {
            selectedId+=1;
            selectedId%=blockTypes.Count;
        }
        blockSelector.transform.position = blockTypes[selectedId].blockNumText.transform.position;
    }

    public override bool Action(Vector3 dir)
    {
        bool b = true;
        if(gameObject.activeSelf) {
            base.Action(dir);
            if(blockTypes[selectedId].blockNum > 0 && !CollisionDetect(dir, nonPlacableLayer)) {
                Vector3 loc = transform.position + dir * Utils.MOVE_SCALE;
                GameObject g = Instantiate(blockTypes[selectedId].placableObject, loc, Quaternion.identity);
                g.GetComponent<Destroyable>().dir = dir;
                if(player_Manager.shooter.gameObject.activeSelf)
                    player_Manager.shooter.UpdateQueue(g.transform);
                blockTypes[selectedId].AddBlocks(-1);
            } else {
                b = false;
            }
        }
        return b;
    }

    public override void AiControl()
    {
        if(gameObject.activeSelf && locationQueue.Count > 0) {
            Vector3 vAction = CheckNear(locationQueue.Peek());
            if(vAction != Vector3.zero) { // near a target
                UpdateSelectedID((int)locationQueue.Peek().z);
                if(Action(vAction))
                    locationQueue.Dequeue();
            } else {
                MoveTowards(locationQueue.Peek());
            }
        }
    }

    public override void Targeter()
    {
        if(gameObject.activeSelf) {
            if(locationQueue.Count > 0) {
                targeter.gameObject.SetActive(true);
                targeter.position = locationQueue.Peek();
            } else {
                base.Targeter();
            }
        } else {
            targeter.gameObject.SetActive(false);
        }
    }

    public void AddBlocks(int amt, int typeId)
    {
        blockNum += amt;
        blockTypes[typeId].AddBlocks(amt);
    }
}
