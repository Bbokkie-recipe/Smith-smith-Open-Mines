using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bellows : MonoBehaviour
{
    public Smelter smelter;
    public Animator animator;
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<OVRGrabber>() != null)
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Play0"))
            {
                animator.Play("Play0");
                smelter.AddFire();
            }
        }
    }
}
