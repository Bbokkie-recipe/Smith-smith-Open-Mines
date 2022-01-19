using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LetterText : MonoBehaviour
{
    public int pageCnt = 5;
    int count = 0;
    public string[] letter_text;
    TextMeshProUGUI showingtext;
    [SerializeField] private GameObject clickBut;
    [SerializeField] private GameObject hammer;
    [SerializeField] private GameObject gameStartUI;
    void Start()
    {
        showingtext = GetComponent<TextMeshProUGUI>();
        showingtext.text = letter_text[count];
    }

    public void Next_Text()
    {
        if (count >= pageCnt-1)
        {
            clickBut.SetActive(false);
            gameObject.transform.parent.transform.parent.gameObject.SetActive(false);
            hammer.SetActive(true);
            gameStartUI.SetActive(true);
            return;
        }
        count++;
        showingtext.text = letter_text[count];
    }


}
