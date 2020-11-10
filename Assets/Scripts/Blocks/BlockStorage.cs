using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class BlockStorage
{
    public GameObject placableObject;
    public TextMeshProUGUI blockNumText;
    public bool notExists;
    public int blockNum = 0;

    public void AddBlocks(int amt)
    {
        blockNum += amt;
        UpdateBlockText();
    }

    public void UpdateBlockText()
    {
        blockNumText.text = blockNum.ToString();
    }
}
