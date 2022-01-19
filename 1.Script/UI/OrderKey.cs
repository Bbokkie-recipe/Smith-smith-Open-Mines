using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderKey : UIKey
{
    public Order order;
    public int num;
    //public int value;
    [SerializeField] Text _text;
    Image image;
    public override void Start()
    {
        image = GetComponent<Image>();
        Debug.Log(num+" "+SaveNLoad.instance.saveData.questData.Count);
        if (!(SaveNLoad.instance.saveData.questData.Count > num))
        {
            SaveNLoad.instance.saveData.questData.Add(new Order());
        }
        order = SaveNLoad.instance.saveData.questData[num];
        OriImgColor();
        OriTextColor();
    }
    public void OriImgColor()
    {
        if(image!=null)
        image.color = new Color(255 / 255, 255 / 255, 255 / 255);
    }
    public void OriTextColor()
    {
        string t = "TRY?";
        _text.text = t;
    }
    public void SetImgColor()
    {
        image.color = new Color(129 / 255, 255 / 255, 221 / 255);
    }
    public void SetTextColor()
    {
        string t = "<color=#ff9797>ACCEPT</color>";
        _text.text = t;
    }
    public override void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PlayerFinger")
        {
            SoundManager.instance.SFXPlay("Jump", clips[0]);
            order.isAccept = true;
            SetImgColor();
            SetTextColor();
        }
    }
}