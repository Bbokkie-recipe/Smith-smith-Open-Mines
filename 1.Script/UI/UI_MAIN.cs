using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UI_MAIN : UIKey
{
    [SerializeField] private GameObject TitlePanel;
    [SerializeField] private GameObject SettingPanel;
    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hammer")
        {
            SoundManager.instance.SFXPlay("VolumeKey", clips[0]);
            Debug.Log("메인으로");
            TitlePanel.SetActive(true);
            SettingPanel.SetActive(false);
        }
    }
}
