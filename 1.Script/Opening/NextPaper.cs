using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPaper : MonoBehaviour
{
    public AudioClip[] clips;
    [SerializeField] private GameObject letter;
    [SerializeField] private GameObject clickBut;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerFinger")
        {
            SoundManager.instance.SFXPlay("VolumeKey", clips[0]);
            letter.SetActive(true);
            clickBut.SetActive(true);
            gameObject.SetActive(false);
        }
    }

}
