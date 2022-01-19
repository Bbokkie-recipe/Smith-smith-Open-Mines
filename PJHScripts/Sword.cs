using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : BoxedGrabbable, IPooledObject<Sword>
{
    public Transform blade;
    public Color color;
    public float value;
    public BladeType bladeType =BladeType.None;
    public bool handleType;
    protected override void Start()
    {
        value = 0;
        isOutBox = false;
    }
    public void Return()
    {
        GetComponent<PooledObject>().Return();
    }
    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        base.GrabBegin(hand, grabPoint);
        GetComponent<MeshRenderer>().enabled = true;
        if(hand.GetComponent<GrabValue>()!=null)
            hand.GetComponent<GrabValue>().value = value;
        DontDestroyOnLoad(gameObject);
        if (value > 0)
        {
            SaveNLoad.instance.saveData.swords.Remove(GetInstanceID());
            SaveNLoad.instance.handleSword.Add(GetInstanceID(),new SwordSave(this));
        }
    }
    public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
    {
        base.GrabEnd(linearVelocity, angularVelocity);
        GetComponent<Rigidbody>().isKinematic = false;
        transform.parent = null;
        if (value > 0)
        {
            SaveNLoad.instance.handleSword.Remove(GetInstanceID());
            SaveNLoad.instance.saveData.swords.Add(GetInstanceID(), new SwordSave(this));
        }
    }
    public void GetBlade(Transform bladeTransform)
    {
        blade = bladeTransform;
        blade.parent = transform;
        blade.localPosition = Vector3.zero;
        blade.localEulerAngles = Vector3.zero;
        SaveNLoad.instance.saveData.swords.Add(GetInstanceID(), new SwordSave(this));
    }
    public void OnDestroy()
    {
        SaveNLoad.instance.saveData.swords.Remove(GetInstanceID());
    }
}
