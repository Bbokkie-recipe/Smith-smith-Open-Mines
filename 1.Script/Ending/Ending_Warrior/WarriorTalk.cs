using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class WarriorTalk : MonoBehaviour
{
    [SerializeField] private GameObject warrior;
    public string[] warrior_text;
    TextMeshProUGUI showingtext;

    void Start()
    {
        showingtext = GetComponent<TextMeshProUGUI>();
    }

    public void Next_Text()
    {
        showingtext.text = warrior_text[0];
        Destroy(warrior, 3f);
    }
}
