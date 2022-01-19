using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ore : BoxedGrabbable, IPooledObject<Ore>
{
    public OreBox oreBox;
    public Rigidbody rbody;
    public string oreName;
    public float maxValue;
    public float minValue;
    public int oreLevel=-1;
    public Color color;
    public bool isInSmelter;

    private Renderer render;
    // Start is called before the first frame update

    protected override void Start()
    {
        rbody = GetComponent<Rigidbody>();
        base.Start();
        render = GetComponent<Renderer>();
        color = render.material.color;
    }
    public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
    {
        if (!isInSmelter)
        {
            base.GrabBegin(hand, grabPoint);
            hand.GetComponent<GrabValue>().value = maxValue;
        }
    }
    public void BackToStash()
    {
        transform.position = transform.parent.position+ Vector3.zero + Vector3.up;
        rbody.velocity = Vector3.zero;
    }
    public void Return()
    {
        maxValue = 0;
        minValue = 0;
        oreName = "";
        oreLevel = -1;
        GetComponent<PooledObject>().Return();
    }
    public void Return(bool isUsed)
    {
        if (isGrabbed)
            grabbedBy.ForceRelease(this);
        if (isUsed)
        {
            SaveNLoad.instance.saveData.currentOre[oreBox.oreLevel] -= 1;
            oreBox.currentOre -= 1;
        }
        Return();
    }
}
