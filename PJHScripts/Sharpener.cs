using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sharpener : MonoBehaviour
{
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
            animator.Play("Play0");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            animator.Play("Play1");
    }

}
