using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCoal : MonoBehaviour
{
    public Smelter smelter;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Coal")
        {
            Coal coal = other.GetComponent<Coal>();
            if (!coal.isInSmelter && !coal.isGrabbed)
            {
                if (smelter.coals.Count < smelter.coalPosition.Length)
                {
                    coal.GetComponent<Rigidbody>().isKinematic = true;
                    coal.transform.position = smelter.coalPosition[smelter.coals.Count].position;
                    coal.transform.rotation= smelter.coalPosition[smelter.coals.Count].rotation;
                    smelter.coals.Push(coal);
                    coal.isInSmelter = true;
                    smelter.UpdateText();
                }
                else
                {
                    coal.Return();
                }
            }
        }
    }
}
