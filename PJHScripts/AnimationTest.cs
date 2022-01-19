using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    public Animator smelter;
    public Animator molding;

    // Update is called once per frame
    void Update()
    {
        if (molding.GetCurrentAnimatorStateInfo(0).IsName("Play0"))
            Debug.Log("Play0");
        if (molding.GetCurrentAnimatorStateInfo(0).IsName("Play1"))
            Debug.Log("Play1");
        if (smelter.GetCurrentAnimatorStateInfo(0).IsName("Iron_Smelter_Idle"))
            Debug.Log("Iron_Smelter_Idle");
    }
}
