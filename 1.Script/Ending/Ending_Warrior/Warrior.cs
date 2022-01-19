using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : MonoBehaviour
{
    //public AudioClip[] clips;
    [SerializeField] private GameObject warriorSet;
    [SerializeField] private GameObject finalUI;
    Sword swordSc;
    int cupNum;
    private void OnTriggerEnter(Collider other)
    {
        finalUI.SetActive(true);
        if (other.gameObject.GetComponent<Sword>() != null)
        {
            //SoundManager.instance.SFXPlay("VolumeKey", clips[0]);
            swordSc = other.gameObject.GetComponent<Sword>();
            if (swordSc.value > 5000)
            {
                cupNum = 1;//금메달
                SaveNLoad.instance.saveData.playerFame += 5000;
                PlayerPrefs.SetInt("hasCup", cupNum);
            }
            else if (swordSc.value > 4500)
            {
                cupNum =2;//은메달
                SaveNLoad.instance.saveData.playerFame += 4500;
                PlayerPrefs.SetInt("hasCup", cupNum);
            }
            else if (swordSc.value > 4000)
            {
                cupNum = 3;//동메달
                SaveNLoad.instance.saveData.playerFame += 4000;
                PlayerPrefs.SetInt("hasCup", cupNum);
            }
            else
            {
                PlayerPrefs.SetInt("hasCup", 0);
                cupNum = 0;
            }
            gameObject.SetActive(false);
        }
    }
}
