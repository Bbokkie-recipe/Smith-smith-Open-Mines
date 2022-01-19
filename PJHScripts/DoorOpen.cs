using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collide!");
        Quaternion newt = new Quaternion();
        newt.eulerAngles = (transform.rotation.eulerAngles + Vector3.up * 90);
        Debug.Log(newt.eulerAngles);
        transform.rotation = newt;
    }
}
