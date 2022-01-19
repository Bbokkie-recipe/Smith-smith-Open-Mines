using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineMapKey : UIKey
{
    //광산 해금의 정도를 받는 변수
    public int openMines =1;
    SaveData saveData;
    public GameObject tableKey;
    public GameObject sellKart;
    //Awake Start onEnable 고민 
    public override void Start()
    {
        saveData = SaveNLoad.instance.saveData;
        //광산 해금만 만큼만 쪽지 등장
        UpdateBoard();
    }
    public void UpdateBoard()
    {
        if (openMines != 0)
        {
            int openedMines = SaveNLoad.instance.saveData.playerMineLv;
            if (openedMines != 6) openedMines++;
            for (int i = 1; i <= openedMines; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                tableKey.gameObject.transform.GetChild(i-1).gameObject.SetActive(true);
            }
        }
    }

    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "PlayerFinger")
        {
            //SoundManager.instance.SFXPlay("Jump", clips[0]);
            //금요일이 아니면,
            if (saveData.day != 4)
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

