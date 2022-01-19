using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coal : BoxedGrabbable, IPooledObject<Coal>
{
    public bool isInSmelter;
    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        if (!isInSmelter)
        {
            base.GrabBegin(hand, grabPoint);

            if (!GetComponent<MeshRenderer>().enabled)
            {
                GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }
    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        base.GrabEnd(linearVelocity, angularVelocity);
        GetComponent<Rigidbody>().isKinematic = false;
    }
    private void OnEnable()
    {
        Start();
    }
    public void Return()
    {
        if (isGrabbed)
            grabbedBy.ForceRelease(this);
        GetComponent<PooledObject>().Return();
    }
}
