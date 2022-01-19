using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIKey : MonoBehaviour
{
    public AudioClip[] clips;

    public virtual void Start()
    {
        originColor = GetComponent<MeshRenderer>().materials[0].color;
    }
    public virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hammer")
        {
            SoundManager.instance.SFXPlay("VolumeKey", clips[0]);
        }
    }

}
