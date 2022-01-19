using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterTouch : MonoBehaviour
{
    public AudioClip[] clips;
    [SerializeField] private GameObject nextObj;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "PlayerFinger")
        {
            SoundManager.instance.SFXPlay("Jump", clips[0]);
            nextObj.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
