using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccountSword : MonoBehaviour
{
    public AudioClip[] clips;
    OrderControl orderControlSc;
    public List<OrderKey> acceptOrderSc;
    public List<Sword> sortedSwords;
    public int earn_Gold;
    public int earn_Fame;
    int tempGold;
    int tempFame;
    int bonusGold;
    int bonusFame;

    float[] qualityBonus;
    enum QUALITY
    {
        DEFALT, BONUS, SPECIAL_BONUS
    }
    //일요일 밤 9시 일괄 검토
    //팔려고 내놓은 검 중에서도
    //1) 수락한 미션에 해당하는 검만을 팔아야함
    //2) 수락한 미션 중에서 가장 합산이 높은 순서대로 팔아야함
    UI_GOLD uI_GOLDSc;
    public void Awake()
    {
        qualityBonus = new float[3];
        uI_GOLDSc = FindObjectOfType<UI_GOLD>();
        orderControlSc = FindObjectOfType<OrderControl>();
        qualityBonus[(int)QUALITY.DEFALT] = 1f;
        qualityBonus[(int)QUALITY.BONUS] = 1.2f;
        qualityBonus[(int)QUALITY.SPECIAL_BONUS] = 1.5f;

    }
    public void GetSword()
    {
        SearchSword();
        SortSword();
        CalculateSword();
    }


    public void CalculateSword()
    {
        if (orderControlSc != null)
        {
            for (int i = 0; i < orderControlSc.sortedOrderSc.Count; i++)
            {
                int tempCount = orderControlSc.sortedOrderSc[i].order.requireCount;
                int BonusSword = 0;
                int SpecialBonusSword = 0;
                float score = 1;
                for (int j = 0; j < orderControlSc.sortedOrderSc[i].order.requireCount; j++)
                {
                    if (orderControlSc.sortedOrderSc[i].order.requireQuality <= sortedSwords[j].value)
                    {
                        foreach(float qualityValue in qualityBonus)
                        {
                            if(sortedSwords[j].value >= orderControlSc.sortedOrderSc[i].order.requireQuality * qualityValue)
                                score *= qualityValue;
                        }

                        Debug.Log("1");
                        tempCount--;
                        if (tempCount <= 0)
                        {
                            MissionClear(orderControlSc.sortedOrderSc[i], SpecialBonusSword, BonusSword);
                            SoundManager.instance.SFXPlay("sellsound", clips[0]); 
                            for (int k = 0; k < orderControlSc.sortedOrderSc[i].order.requireCount; k++)
                            {
                                Debug.Log("조건에 충족하여 팔린 검 : " + sortedSwords[k].gameObject.name);
                                //sortedSwords[k].gameObject.SetActive(false);
                                Destroy(sortedSwords[k].gameObject);
                                sortedSwords.RemoveAt(k);
                            }
                        }
                    }
                    else j = orderControlSc.acceptOrderSc[i].order.requireCount + 1;
                }
            }
        }
    }

    public void MissionClear(OrderKey clearOrders, int specialBonus, int normalBonus)
    {
        List<int> result = new List<int>();
        earn_Gold = RewardGold(clearOrders.order.requireQuality, clearOrders.order.requireCount, specialBonus, normalBonus);
        earn_Fame = RewardFame(clearOrders.order.requireQuality, clearOrders.order.requireCount, specialBonus, normalBonus);
        result.Add(earn_Gold);
        result.Add(earn_Fame);
        uI_GOLDSc.AddMoneyNFame(result);
    }
    public int RewardGold(int _Quality, int _Count, int _specialBonus=0, int _normalBonus=0)
    {
        int perGold = (int)(_Quality * 0.4f);
        Debug.Log("개당 이익 : " + perGold);
        int gold = perGold * _Count;
        Debug.Log("보너스 추가 전 총 이익 : " + gold);
        int n_Bonus = (int)(perGold * 0.2f) * _normalBonus;
        Debug.Log("이익 노말 보너스 : " + n_Bonus);
        int s_Bonus = (int)(perGold * 0.5f) * _specialBonus;
        Debug.Log("이익 스페셜 보너스 : " + s_Bonus);
        int result_Bonus = n_Bonus + s_Bonus;
        Debug.Log("총 이익 보너스 : " + result_Bonus);
        gold += result_Bonus;
        Debug.Log("총 이익 : " + gold);
        return gold;
    }
    public int RewardFame(int _Quality, int _Count, int _specialBonus = 0, int _normalBonus = 0)
    {
        int perFame = (int)(_Quality * 0.01f);
        Debug.Log("개당 명성 : " + perFame);
        int fame = perFame * _Count;
        Debug.Log("보너스 추가 전 총 명성 : " + fame);
        int n_fBonus = (int)(fame * 0.01f) * _normalBonus;
        Debug.Log("명성 노말 보너스 : " + n_fBonus);
        int s_fBonus = (int)(fame * 0.02f) * _specialBonus;
        Debug.Log("명성 스페셜 보너스 : " + s_fBonus);
        int result_fBonus = n_fBonus + s_fBonus;
        Debug.Log("총 명성 보너스 : " + result_fBonus);
        fame += result_fBonus;
        Debug.Log("보너스 추가 후 총 명성이익 : " + fame);
        return fame;
    }
    public void SearchSword()
    {
        //10번 레이어 Sword로 설정해야됨
        BoxCollider box = GetComponent<BoxCollider>();
        Collider[] colls = Physics.OverlapBox(box.center, box.size / 2);
        for (int i = 0; i < colls.Length; i++)
        {
            if (colls[i].transform.GetComponent<Sword>() != null)
            {
                        if (sortedSwords.Contains(colls[i].gameObject.GetComponent<Sword>())) continue;
                        else sortedSwords.Add(colls[i].gameObject.GetComponent<Sword>());
            }
        }
    }
    public void SortSword()
    {
        sortedSwords.Sort((o1, o2) => o1.value.CompareTo(o2.value));
        for (int i = 0; i < sortedSwords.Count; i++)
        {
            Debug.Log(i + "번째 검 : " + sortedSwords[i].value);
        }
    }

}
