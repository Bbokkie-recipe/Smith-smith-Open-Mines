using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Merchant : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ore")
        {
            SaveNLoad.instance.saveData.playerGold += (int)other.GetComponent<Ore>().maxValue/4;
            other.GetComponent<Ore>().Return(true);
        }
        else if(other.tag=="Handle")
        {
            SaveNLoad.instance.saveData.playerGold += (int)other.GetComponent<Sword>().value / 2;
            Destroy( other.GetComponent<Sword>().gameObject);
        }
        else if (other.tag == "Ingot")
        {
            SaveNLoad.instance.saveData.playerGold += (int)other.GetComponent<Ingot>().value / 3;
            Destroy(other.GetComponent<Ingot>().gameObject);
        }
    }
}
