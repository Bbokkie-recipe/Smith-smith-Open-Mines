using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupShow : MonoBehaviour
{

    void Start()
    {
        if (PlayerPrefs.GetInt("hasCup", 1) == 1)
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("hasCup", 2) == 1)
        {
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("hasCup", 3) == 1)
        {
            transform.GetChild(2).gameObject.SetActive(true);
        }
    }
}
