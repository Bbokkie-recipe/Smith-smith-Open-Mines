using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class searchRange : MonoBehaviour
{
    //시야각 및 추적반경을 제어하는 EnemyFOV클래스를 저장할 변수
    private EnemyFOV enemyFOV;

    void Start()
    {
        //시야각 및 추적반경을 제어하는 EnemyFOV클래스를 추출
        enemyFOV = GetComponent<EnemyFOV>();
    }

    //여기에서 탐지범위내 진입을 체크해서 부모로 보냄
    private void OnTriggerStay(Collider other)
    {
        //Trigger한 오브젝트의 태그가 Player확인
        if (other.gameObject.CompareTag("Player"))
        {
            //플레이어와의 거리에 장애물 여부를 판단
            if (enemyFOV.isViewPlayer() && enemyFOV.isTracePlayer())
            {
                transform.parent.GetComponent<Monster>().isRange = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Trigger한 오브젝트의 태그가 Player확인
        if (other.gameObject.CompareTag("Player"))
        {
            transform.parent.GetComponent<Monster>().isRange = false;
        }
    }
}
