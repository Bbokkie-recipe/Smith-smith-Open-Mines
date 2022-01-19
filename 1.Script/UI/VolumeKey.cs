using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeKey : UIKey
{
    public float volume;
    public bool bgm;

    public Color oriColor;
    private void Awake()
    {
        oriColor = GetComponent<Renderer>().material.color;
    }
    public void Update()
    {
        float getvolume;
        if (bgm)
        {
            if (SoundManager.instance.mixer.GetFloat("BgmVolume", out getvolume))
            {
                if (getvolume < volume)
                {
                    GetComponent<Renderer>().material.color = Color.gray;
                }
                else
                {
                    GetComponent<Renderer>().material.color = oriColor;
                }
            }
        }
        else
        {
            if (SoundManager.instance.mixer.GetFloat("EffectVolume", out getvolume))
            {
                if (getvolume < volume)
                {
                    GetComponent<Renderer>().material.color = Color.gray;
                }
                else
                {
                    GetComponent<Renderer>().material.color = oriColor;
                }
            }
        }
    }
    public override void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Hammer")
        {
            SoundManager.instance.SFXPlay("VolumeKey", clips[0]);
            if (bgm)
            {
                SoundManager.instance.BgmSoundVolume(volume);
            }
            else
            {
                SoundManager.instance.EffectSoundVolume(volume);
            }
        }
    }
}