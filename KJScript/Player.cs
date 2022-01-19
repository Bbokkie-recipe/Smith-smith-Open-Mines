using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float lifeCount=5;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="MonsterWeapon")
        {
            Debug.Log("아파!");
            lifeCount--;
        }
    }

    private void Update()
    {
        if(lifeCount <= 0)
        {
            Debug.Log("사망했다");
        }
    }
}
