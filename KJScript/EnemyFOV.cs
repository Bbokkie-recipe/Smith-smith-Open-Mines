using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOV : MonoBehaviour
{
    //�� ĳ������ ���� ���� �Ÿ��� ����
    public float viewRange = 10.0f;

    //�� ĳ������ �þ߰�
    public float viewAngle = 120.0f;

    private Transform enemyTr;
    private Transform playerTr;
    private int playerLayer;
    private int obstacleLayer;
    private int layerMask;

    void Start()
    {
        //������Ʈ ����
        enemyTr = GetComponent<Transform>();
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;

        //���̾��ũ �� ���
        playerLayer = LayerMask.NameToLayer("Player");
        obstacleLayer = LayerMask.NameToLayer("Obstacle");
        layerMask = 1 << playerLayer | 1 << obstacleLayer;
    }
    public Vector3 CirclePoint(float angle)
    {
        //���� ��ǥ�� �������� �����ϱ� ���� ĳ������ Yȸ������ ����
        angle += transform.eulerAngles.y;
        return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad), 0, Mathf.Cos(angle * Mathf.Deg2Rad));
    }

    public bool isTracePlayer()
    {
        bool isTrace = false;

        //���� �ݰ� ���� �ȿ��� �÷��̾� ĳ���� ����
        Collider[] colls = Physics.OverlapSphere(enemyTr.position, viewRange, 1 << playerLayer);

        //�迭�� ������1�϶� ���ΰ��� ���� �ȿ� �ִٰ� �Ǵ�
        if (colls.Length == 1)
        {
            //�� ĳ���Ϳ� ���ΰ� ������ ���� ���͸� ���
            Vector3 dir = (playerTr.position - enemyTr.position).normalized;

            //�� ĳ������ �þ߰��� ���Դ��� �Ǵ�
            if(Vector3.Angle(enemyTr.forward,dir) < viewAngle*0.5f && Vector3.Angle(enemyTr.forward, dir) > -(viewAngle * 0.5f))
            {
                isTrace = true;
            }
        }

        return isTrace;
    }

    public bool isViewPlayer()
    {
        bool isView = false;
        RaycastHit hit;

        //�� ĳ���Ϳ� ���ΰ� ������ ���� ���͸� ���
        Vector3 dir = (playerTr.position - enemyTr.position).normalized;
        Debug.DrawRay(enemyTr.position, dir * viewRange);

        //����ĳ��Ʈ�� �����ؼ� ��ֹ��� �ִ��� ���θ� �Ǵ�
        if(Physics.Raycast(enemyTr.position, dir, out hit, viewRange, layerMask))
        {
            isView = (hit.collider.CompareTag("Player"));
        }
        return isView;
    }
}
