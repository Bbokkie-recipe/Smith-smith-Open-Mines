using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningSet : MonoBehaviour
{
    void Start()
    {
        GameObject obj1 = GameObject.Find("LOAD");
        obj1.SetActive(false);
    }

