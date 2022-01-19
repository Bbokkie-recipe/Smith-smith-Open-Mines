using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SETTING : UIKey
{
    [SerializeField] private GameObject InGamePanel;
    [SerializeField] private GameObject SettingPanel;
    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hammer")
        {
            SoundManager.instance.SFXPlay("VolumeKey", clips[0]);
            Debug.Log("¼¼ÆÃ");
            InGamePanel.SetActive(false);
            SettingPanel.SetActive(true);
            StartCoroutine(Vanish());
        }
    }
    IEnumerator Vanish()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(2);
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }
}
