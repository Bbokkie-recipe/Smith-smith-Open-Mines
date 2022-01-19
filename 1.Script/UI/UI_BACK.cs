using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_BACK : UIKey
{
    [SerializeField] private GameObject StatusPanel;
    [SerializeField] private GameObject InGamePanel;

    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hammer")
        {
            SoundManager.instance.SFXPlay("VolumeKey", clips[0]);
            InGamePanel.SetActive(false);
            StatusPanel.SetActive(true);
        }
    }
}
