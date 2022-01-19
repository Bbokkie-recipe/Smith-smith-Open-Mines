using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetIron : MonoBehaviour
{
    public Smelter smelter;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Ore")
        {
            Ore ore = other.GetComponent<Ore>();
            if (!ore.isInSmelter&&!ore.isGrabbed)
            {
                if (smelter.ores.Count < smelter.maxOre)
                {
                    smelter.AddOre(ore);
                    ore.GetComponent<MeshRenderer>().enabled = false;
                }
                else
                {
                    ore.BackToStash();
                }
            }
        }
    }
}
