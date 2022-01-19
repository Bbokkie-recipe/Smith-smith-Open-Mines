using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GOLD : MonoBehaviour
{
    [SerializeField] Text money_text;
    [SerializeField] Text fame_text;
    SaveData saveData;
    public void Start()
    {
        saveData = SaveNLoad.instance.saveData;
    }
    private void Update()
    {
        ResultText();
    }
    public void AddMoneyNFame(List<int> earnResult)
    {
        Debug.Log("번 돈"+earnResult[0]);
        Debug.Log("번 명성치"+earnResult[1]);
        saveData.playerGold += earnResult[0];
        saveData.playerFame += earnResult[1];
        ResultText();

    }

    void ResultText()
    {
        money_text.text = SaveNLoad.instance.saveData.playerGold.ToString() + " GOLD";
        fame_text.text = "Smith's" + "\r\n" + "fame" + "\r\n" + saveData.playerFame.ToString();
    }

}
