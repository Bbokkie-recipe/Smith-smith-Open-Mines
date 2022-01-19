using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public enum BladeType
{
    Broad,
    Fuller,
    Simple,
    None
}
public class Blade : MonoBehaviour
{
    public float value;
    public BladeType bladeType;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Handle")
        {
            Transform ingot = transform.parent;
            if (ingot.GetComponent<Ingot>().grabbedBy != null)
            {
                ingot.GetComponent<Ingot>().grabbedBy.ForceRelease(ingot.GetComponent<Ingot>());
                Sword sword = other.GetComponent<Sword>();
                sword.color = GetComponent<Renderer>().material.color;
                sword.value = value;
                sword.bladeType = bladeType;
                sword.GetBlade(transform);
                StartCoroutine(IngotDestroy(other));
            }
        }
    }
    IEnumerator IngotDestroy(Collider other)
    {
        Ingot ingot = transform.parent.GetComponent<Ingot>();
        Destroy(ingot.gameObject);
        Destroy(this);
        yield break;
    }
}
