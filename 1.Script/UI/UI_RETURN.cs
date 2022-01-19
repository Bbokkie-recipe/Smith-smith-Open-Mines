using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_RETURN : UIKey
{
    [SerializeField] private GameObject SettingPanel;
    [SerializeField] private GameObject InGamePanel;

    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hammer")
        {
            SoundManager.instance.SFXPlay("VolumeKey", clips[0]);
            Debug.Log("∏Æ≈œ");
            InGamePanel.SetActive(true);
            if(InGamePanel.transform.GetComponentInChildren<UI_NewGame>()!=null)
            InGamePanel.transform.GetComponentInChildren<UI_NewGame>().StartCoroutine(Vanish());
            SettingPanel.SetActive(false);

        }
    }
    IEnumerator Vanish()
    {
        InGamePanel.transform.GetChild(1).GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        yield return new WaitForSeconds(2);
        gameObject.GetComponent<BoxCollider>().enabled = true;
        InGamePanel.transform.GetChild(1).GetComponent<BoxCollider>().enabled = true;
    }
}
