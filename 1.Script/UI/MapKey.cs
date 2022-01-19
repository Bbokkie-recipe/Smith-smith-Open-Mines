using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapKey : MonoBehaviour
{
    SaveData saveData;
    public void Start()
    {
        saveData = SaveNLoad.instance.saveData;
    }
    public void VanishKey()
    {
        transform.GetChild(1).gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerFinger")
        {
            //금요일이면,
            if (saveData.day == 4)
            {
                transform.GetChild(1).gameObject.SetActive(true);
                Invoke("VanishKey",3.5f);
            }
        }
    }
}
