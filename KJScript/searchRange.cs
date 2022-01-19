using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class searchRange : MonoBehaviour
{
    //�þ߰� �� �����ݰ��� �����ϴ� EnemyFOVŬ������ ������ ����
    private EnemyFOV enemyFOV;

    void Start()
    {
        //�þ߰� �� �����ݰ��� �����ϴ� EnemyFOVŬ������ ����
        enemyFOV = GetComponent<EnemyFOV>();
    }

    //���⿡�� Ž�������� ������ üũ�ؼ� �θ�� ����
    private void OnTriggerStay(Collider other)
    {
        //Trigger�� ������Ʈ�� �±װ� PlayerȮ��
        if (other.gameObject.CompareTag("Player"))
        {
            //�÷��̾���� �Ÿ��� ��ֹ� ���θ� �Ǵ�
            if (enemyFOV.isViewPlayer() && enemyFOV.isTracePlayer())
            {
                transform.parent.GetComponent<Monster>().isRange = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Trigger�� ������Ʈ�� �±װ� PlayerȮ��
        if (other.gameObject.CompareTag("Player"))
        {
            transform.parent.GetComponent<Monster>().isRange = false;
        }
    }
}
