using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorCome : MonoBehaviour
{
    [SerializeField] private GameObject finalUI;
    [SerializeField] private GameObject warrior;
    void Update()
    {
        if(SaveNLoad.instance.saveData.week>=55)
        {
            if (SaveNLoad.instance.saveData.playerMineLv < 6)
            {
                finalUI.SetActive(true);
            }
            else
            {
                warrior.SetActive(true);
            }
        }
    }
}
