using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxedGrabbable: OVRGrabbable
{
    public bool isOutBox=false;
    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        base.GrabEnd(linearVelocity, angularVelocity);
        GetComponent<Rigidbody>().isKinematic = false;
        transform.parent = null;
    }
}
public class GrabBox<T> : MonoBehaviour where T : BoxedGrabbable, IPooledObject<T>
{
    public ObjectPoolBase<T> pool;
    public T poolObject;
    public int maxPoolSize;
    public int initialPoolSize;
    public int additionalPoolSize;
    public bool hasNew;
    public bool isPull;
    public void Start()
    {
        pool = new ObjectPoolBase<T>(this, poolObject, maxPoolSize, initialPoolSize, additionalPoolSize);
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!hasNew)
        {
            OVRGrabber grabber = other.GetComponentInParent<OVRGrabber>();
            if (grabber != null)
            {
                Transform transform = pool.GetObject().transform;
                if (transform != null)
                {
                    transform.GetComponent<MeshRenderer>().enabled = false;
                    transform.GetComponent<Rigidbody>().isKinematic = true;
                    transform.parent = grabber.transform;
                    transform.localPosition = grabber.GripTransform.localPosition;
                    hasNew = true;
                }
                else
                {
                    Debug.Log("grabBox Transform Null");
                }

            }
        }
        T newObject = other.GetComponent<T>();
        if (newObject != null && !newObject.isGrabbed)
        {
            if (newObject.isOutBox)
            {
                if (isPull)
                    Destroy(newObject.gameObject);
                else
                {
                    newObject.Return();
                    hasNew = false;
                }
            }
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        T newObject = other.GetComponent<T>();
        if (newObject != null)
        {
            if (!newObject.isGrabbed)
                newObject.Return();
            if (!newObject.GetComponent<OVRGrabbable>().isGrabbed)
            {
                newObject.Return();
            }
            else
            {
                newObject.isOutBox = true;
                if(isPull)
                    newObject.GetComponent<PooledObject>().PullObject();
            }
            hasNew = false;
        }
    }
}
