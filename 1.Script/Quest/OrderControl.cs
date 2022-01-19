using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderControl : MonoBehaviour
{
    public SaveData saveData;
    public OrderKey[] OrderSc;
    public List<OrderKey> acceptOrderSc;
    public List<OrderKey> sortedOrderSc;
    //public 
    Dictionary<int, string[]> questData;
    [SerializeField] Text[] quest_Owner;
    [SerializeField] Text[] quest_Contents;
    [SerializeField] Text[] request_Quality;
    [SerializeField] Text[] amount;
    GameObject tempQuest;
    private bool isChecked;
    int questCount = 0;

    private void Start()
    {
        saveData = SaveNLoad.instance.saveData;
    }
    private void Update()
    {
        if (saveData.day == 6 && saveData.time > 12 && !isChecked)
        {
            Debug.Log("체크함");
            CheckAcceptOrder();
            isChecked = true;
        }
        if (saveData.day == 0 && isChecked)
        {
            Debug.Log("업뎃함");
            UpdateOrderBoard();
            isChecked = false;
        }
    }
    public void UpdateOrderBoard()
    {
        questData = new Dictionary<int, string[]>();
        questCount = OrderCount(SaveNLoad.instance.saveData.playerMineLv);
        sortedOrderSc.Sort((o1,o2)=>o1.order.Engagement.CompareTo(o2.order.Engagement));
        UpdateQuestBoard();
        RandAmount();
        Randquest_Contents();
        Randquest_Quality();
    }

    //수락한 퀘스트만 리스트에 넣음
    public void CheckAcceptOrder()
    {
        for (int i = 0; i < 5; i++)
        {
            if (OrderSc[i].order.isAccept == true)
            {
                if (acceptOrderSc.Contains(OrderSc[i])) continue;
                else acceptOrderSc.Add(OrderSc[i]);
            }
        }
    }

    public void RandAmount()
    {
        for (int i = 0; i < amount.Length; i++)
        {
            int temp = Random.Range(1, 4);//테스트 해본 결과, 최대치가 3
            amount[i].text = temp.ToString() + "개";
            saveData.questData[i].requireCount = temp;
        }
    }
    /*
    <퀘스트 칼 품질 요구조건 랜덤치>, 광산 개방별
    1: 100~500
    2: 400~1000
    3: 800~1500
    4: 1300~ 2000
    5: 1800~3000
    6: 2500~3500
     */
    public void Randquest_Quality()
    {
        for (int i = 0; i < request_Quality.Length; i++)
        {
            int temp = 0;
            switch (SaveNLoad.instance.saveData.playerMineLv)
            {
                case 1:
                    if(SaveNLoad.instance.saveData.week==2) temp = Random.Range(10, 50);
                    else temp = Random.Range(100, 501);
                    break;
                case 2: temp = Random.Range(400, 1001); 
                    break;
                case 3: temp = Random.Range(800, 1501); 
                    break;
                case 4: temp = Random.Range(1300, 2001); 
                    break;
                case 5: temp = Random.Range(1800, 3001); 
                    break;
                case 6: temp = Random.Range(2500, 3501); 
                    break;
            }
            request_Quality[i].text = "Quality : " + temp.ToString() + " 이상";
            saveData.questData[i].requireQuality = temp;
            quest_Owner[i].text = saveData.questData[i].quest_Owner;
            quest_Contents[i].text = saveData.questData[i].quest_Contents;
        }
    }

    public void Randquest_Contents()
    {
        switch (SaveNLoad.instance.saveData.playerMineLv)
        {
            case 1: questData.Clear(); OpenedMine_1(); break;
            case 2: questData.Clear(); OpenedMine_2(); break;
            case 3: questData.Clear(); OpenedMine_3(); break;
            case 4: questData.Clear(); OpenedMine_4(); break;
            case 5: questData.Clear(); OpenedMine_5(); break;
            case 6: questData.Clear(); OpenedMine_6(); break;
        }
        int[] temp = { 1, 2, 3, 4, 5,6,7,8 };
        NumShuffle(temp);

        for (int i = 0; i < questCount; i++)
        {
            saveData.questData[i].quest_Owner = questData[temp[i]][0];
            saveData.questData[i].quest_Contents = questData[temp[i]][1];
        }
    }

    public int OrderCount(int OpenedMine)
    {
        int quset = 0;
        switch (OpenedMine)
        {
            case 0: quset = 0; break;
            case 1: 
                if(SaveNLoad.instance.saveData.week == 2) quset =1;
                else quset = 2; break;
            case 2: quset = 3; break;
            case 3: quset = 3; break;
            case 4: quset = 4; break;
            case 5: quset = 4; break;
            case 6: quset = 5; break;
        }
        return quset;
    }
    public void UpdateQuestBoard()
    {
        if (questCount != 0)
        {
            for (int i = 0; i < questCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
    /*첫 광산 해금시*/
    void OpenedMine_1()
    {
        questData.Add(1, new string[] { "Mrs.Jung", "아들이 가지고 놀만한 가벼운 검이 필요하다네" });
        questData.Add(2, new string[] { "James", "가볍고 값싼 훈련용 검을 장만할 계획입니다." });
        questData.Add(3, new string[] { "중앙 교회", "봉사자들에게 선물할 가벼운 검을 준비해주게." });
        questData.Add(4, new string[] { "KATE 대장간", "경쟁자 Smith의 검이 쓸만한지 궁금한걸?" });
        questData.Add(5, new string[] { "경비대", "초급 경비대원들이 사용할 만한 검을 보내주게." });
        questData.Add(6, new string[] { "Lux", "Smith씨의 아버지가 운영할 때부터 단골이었습니다." });
        questData.Add(7, new string[] { "Adam", "광산에 스켈레톤이 있다고 하네... 칼을 들고가야겠어요." });
        questData.Add(8, new string[] { "Mr.Gang", "사촌 여동생이 장난감처럼 쓸만한 칼을 좀 주게." });
    }

    /*두번째 광산 해금시*/
    void OpenedMine_2()
    {
        questData.Add(1, new string[] { "Mrs.Lee", "딸이 두번째 광산에 가고싶다네요. 빈손으로 보낼 수 없죠." });
        questData.Add(2, new string[] { "Lucas", "광산에 가서 쓸만한 칼을 장만할 계획입니다." });
        questData.Add(3, new string[] { "중앙 교회", "교회에 봉사하는 전사들에게 하사할 검이 필요하네." });
        questData.Add(4, new string[] { "LOMI 대장간", "경쟁자 Smith대장간 검은 장난감 수준아닌가?" });
        questData.Add(5, new string[] { "경비대", "초급 경비대원들이 더 좋은 검을 달라고 아우성이야." });
        questData.Add(6, new string[] { "Mrs.Jung", "아들이 가지고 놀던 검이 녹슬어서 부서졌어." });
        questData.Add(7, new string[] { "Daniel", "모험가가 되고 싶은데.. 일단 검부터 사두려고." });
        questData.Add(8, new string[] { "Adam", "두번째 광산이 열렸다고 하니 새 검이 필요해." });
    }

    /*세번째 광산 해금시*/
    void OpenedMine_3()
    {
        questData.Add(1, new string[] { "Mrs.Kim", "아들이 세번째 광산에 가고싶다네요. 잘 만들어주세요." });
        questData.Add(2, new string[] { "Mason", "세번째 광산에 있는 스켈레톤을 만나러 가야겠어." });
        questData.Add(3, new string[] { "중앙 교회", "성직자들도 검이 필요하긴 마련이지." });
        questData.Add(4, new string[] { "CASS 대장간", "경쟁자 Smith 검이 슬슬 좋다는 소문이 퍼지던데..." });
        questData.Add(5, new string[] { "경비대", "세번째 광산이 열린 뒤로 경비대원들의 눈이 높아졌어." });
        questData.Add(6, new string[] { "Mrs.Lee", "딸이 요즘 전투 준비에 한창이야." });
        questData.Add(7, new string[] { "Henry", "사람들이 자꾸 광산 전리품을 자랑해서 나도 가보려고." });
        questData.Add(8, new string[] { "Lux", "Smith씨의 아버님이 잘나가는 Smith씨를 보면 기뻐할텐데요." });
    }

    /*네번째 광산 해금시*/
    void OpenedMine_4()
    {
        questData.Add(1, new string[] { "Mrs.Gang", "조카가 네번째 광산에 간다네요. 강한 칼을 주세요." });
        questData.Add(2, new string[] { "Henry", "네번째 광산에 있는 스켈레톤을 만나러 가야겠어." });
        questData.Add(3, new string[] { "중앙 교회", "마을에 좋은 광산들이 많이 열려서 요즘 교회 분위기가 좋아." });
        questData.Add(4, new string[] { "RAYMOND 대장간", "경쟁자 Smith 검이 슬슬 좋다는 소문이 퍼지던데..." });
        questData.Add(5, new string[] { "경비대", "네번째 광산이 열린 뒤로 경비대원들이 바빠졌어." });
        questData.Add(6, new string[] { "Mrs.Kim", "아들이 더 좋은 칼을 가지고 싶다고 자꾸 보채네. "});
        questData.Add(7, new string[] { "기사단장", "기사단 내의 기사들에게 Smith씨의 검을 줘볼까 해." });
        questData.Add(8, new string[] { "Lux", "Smith씨의 아버님이 잘나가는 Smith씨를 보면 기뻐할텐데요." });
    }

    /*다섯번째 광산 해금시*/
    void OpenedMine_5()
    {
        questData.Add(1, new string[] { "Mrs.Park", "남편이 다섯번째 광산에 간다네요. 좋은 칼을 주세요." });
        questData.Add(2, new string[] { "Daniel", "다섯번째 광산에 있는 스켈레톤을 만나러 가야겠어." });
        questData.Add(3, new string[] { "중앙 교회", "우수한 품질의 광석으로 만든 Simth씨의 검이라면 믿을 수 있어." });
        questData.Add(4, new string[] { "JAYCOB 대장간", "경쟁자 Smith 검이 슬슬 콘테스트 우승 후보로 꼽히고 있다며?" });
        questData.Add(5, new string[] { "경비대", "다섯번째 광산이 열린 뒤로 더 강한 칼들이 필요해졌어." });
        questData.Add(6, new string[] { "Miss.im", "올해 전투 페스티벌에서 좋은 성적을 내고 싶어. 좋은 칼을 부탁해." });
        questData.Add(7, new string[] { "기사단장", "기사단 내의 기사들이 Smith의 검을 고정적으로 쓰고싶다고 했어." });
        questData.Add(8, new string[] { "Lux", "Smith씨의 아버님이 잘나가는 Smith씨를 보면 기뻐할텐데요." });
    }
    /*마지막 광산 해금시*/
    void OpenedMine_6()
    {
        questData.Add(1, new string[] { "Miss.Ryu", "아버지가 마지막 광산에 간다네요. 으뜸인 칼을 주세요." });
        questData.Add(2, new string[] { "Adam", "마지막 광산에 있는 스켈레톤을 만나러 가야겠어." });
        questData.Add(3, new string[] { "중앙 교회", "중앙교회는 Simth씨의 검만을 쓰고 있어. 믿음직스러워." });
        questData.Add(4, new string[] { "JAYCOB 대장간", "경쟁자 Smith씨가 올해의 대장장이가 될 수도 있다는데..." });
        questData.Add(5, new string[] { "경비대", "마지막 광산이 열린 뒤로 더 강한 칼들이 필요해졌어." });
        questData.Add(6, new string[] { "Mrs.Song", "시민들도 전부 광산에 놀러간다는데 나도 가보려고" });
        questData.Add(7, new string[] { "Miss.Choi", " 길드 사람들 모두 Simth씨의 검에 감탄하던걸." });
        questData.Add(8, new string[] { "Lux", "Smith씨의 아버님이 잘나가는 Smith씨를 보면 기뻐할텐데요." });
    }

    //숫자 섞는 함수
    int[] NumShuffle(int[] x)
    {
        int rand1;
        int rand2;
        int tmp;
        for (int i = 0; i < 25; i++)
        {
            rand1 = Random.Range(0, x.Length);
            rand2 = Random.Range(0, x.Length);
            tmp = x[rand1];
            x[rand1] = x[rand2];
            x[rand2] = tmp;
        }
        return x;
        }

}
