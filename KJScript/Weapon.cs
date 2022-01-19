using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    //Vector3 pos;
    Rigidbody weaponRigid;

    void Update()
    {
        //pos = transform.position;
        weaponRigid = GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Stone")
        {
            Vector3 dir1 = (transform.position - collision.gameObject.transform.position).normalized; // 단위 방향벡터
            Vector3 dir2 = -dir1; // 방향을 뒤집음
            weaponRigid.AddForce(dir2 * 10000);
        }
    }
}
