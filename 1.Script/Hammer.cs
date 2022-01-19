using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : OVRGrabbable
{
    private Vector3 startPos;
    private Vector3 startRot;
    protected override void Start()
    {
        base.Start();
        startPos = transform.position;
        startRot = transform.eulerAngles;
    }
    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        base.GrabBegin(hand, grabPoint);
    }
    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        base.GrabEnd(linearVelocity, angularVelocity);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.position = startPos;
        transform.eulerAngles = startRot;
    }
}
