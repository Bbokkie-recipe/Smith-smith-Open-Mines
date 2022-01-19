using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestColision : MonoBehaviour
{
    public Transform ragdoll;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Weapon")
        {
            Debug.Log(gameObject.name + "ºÎÀ§ ºÎµúÈû");
            GetComponent<Rigidbody>().AddForce(-(collision.contacts[0].point - transform.position) * 100f, ForceMode.Impulse);
        }
    }
}
