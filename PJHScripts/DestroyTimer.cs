using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    public AudioClip DestroySound;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource audioSource=
        GetComponent<AudioSource>();
        audioSource.clip = DestroySound;
        audioSource.Play();
        StartCoroutine(Destroyer());
    }

    IEnumerator Destroyer()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
