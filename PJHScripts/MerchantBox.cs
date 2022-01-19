using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantBox : GrabBox<Ore>
{
    public int oreLevel;

    protected override void OnTriggerEnter(Collider other)
    {
        if (SaveNLoad.instance.saveData.playerGold >= SaveNLoad.instance.orePrice[oreLevel])
        {
            base.OnTriggerEnter(other);
        }
        Ore ore = other.GetComponent<Ore>();
        if (ore.isOutBox)
        {
            SaveNLoad.instance.saveData.playerGold += SaveNLoad.instance.orePrice[oreLevel];
        }
        if (ore != null&&ore.maxValue==0)
        {
            ore.maxValue = ore.maxValue = SaveNLoad.instance.oreValue[oreLevel];
            ore.color = SaveNLoad.instance.oreColor[oreLevel];
            ore.oreName = SaveNLoad.instance.oreName[oreLevel];
        }
    }
    protected override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        Ore ore = other.GetComponent<Ore>();
        if (ore != null)
        {
            if (SaveNLoad.instance.saveData.playerGold >= SaveNLoad.instance.orePrice[oreLevel])
            {
                ore.Return();
            }
        }
    }
}
