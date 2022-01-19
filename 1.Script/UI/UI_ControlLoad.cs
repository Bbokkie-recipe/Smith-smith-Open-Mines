using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ControlLoad : MonoBehaviour
{
    public bool nonLookLoad;
    [SerializeField] private GameObject LoadPanel;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hammer")
        {
            if(nonLookLoad) LoadPanel.SetActive(false);
            else LoadPanel.SetActive(true);
        }
    }
}
