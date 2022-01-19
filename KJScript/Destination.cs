using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Navigation ��� ���� �ʿ�
using UnityEngine.AI;
public class Destination : MonoBehaviour
{
    //��ǥ����
    public Transform target;
    public Transform original;

    NavMeshAgent agent;

    //SpotRange�ڽ� ������Ʈ�� �����´�
    public MeshCollider spotRange;
    public Vector3 targetPos;

    //�þ߰� �� �����ݰ��� �����ϴ� EnemyFOVŬ������ ������ ����
    private EnemyFOV enemyFOV;

    Animator animator;

    float timer=0;
    float backTime=5;
    public float chasingSituation=0f;

    public GameObject walkMusic;
    public GameObject chaseMusic;
    public GameObject backMusic;

    public bool chase = false;

    void Walking()
    {
        chaseMusic.SetActive(false);
        animator.SetBool("Walk", true);
        walkMusic.SetActive(true);
    }

    void Stoping()
    {
        animator.SetBool("Walk", false);
        walkMusic.SetActive(false);
    }

    void ChasingOn()
    {
        walkMusic.SetActive(false);
        animator.SetBool("Walk", true);
        chaseMusic.SetActive(true);
    }
    void ChasingOff()
    {
        animator.SetBool("Walk", false);
        chaseMusic.SetActive(false);
    }

    void Start()
    {
        //�ش� ��ü�� NavMeshAgent�� ����
        agent = GetComponent<NavMeshAgent>();

        //�þ߰� �� �����ݰ��� �����ϴ� EnemyFOVŬ������ ����
        enemyFOV = GetComponent<EnemyFOV>();

        animator = transform.Find("EnemyModel").GetComponent<Animator>();
    }

    void OnTriggerStay(Collider other)
    {
        //Trigger�� ������Ʈ�� �±װ� Player�� ����
        if (other.gameObject.CompareTag("Player"))
        {
            //�÷��̾���� �Ÿ��� ��ֹ� ���θ� �Ǵ�
            if (enemyFOV.isViewPlayer() && enemyFOV.isTracePlayer())
            {
                Debug.Log("Enemy spotted");
                targetPos = target.position;
                agent.SetDestination(targetPos);  //��ֹ��� ������ ����
                //animator.SetBool("Walk", true);
                ChasingOn();
                agent.speed = 4.0f;

                backMusic.SetActive(true);
                chase = true;
            }
        }
    }
    
    

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && chase==true)
        {
            //������¸� ������ �Լ��� �����ؼ� ����

            Debug.Log("Enemy Lost");
            chasingSituation = 1;
        }
 
    }

    void Update()
    {
        if (chasingSituation == 1)
        {
            if (agent.velocity == Vector3.zero)
            {
                ChasingOff();
                
                chasingSituation = 2;
            }
        }
        if(chasingSituation == 2)
        {
            timer += Time.deltaTime;
            targetPos = original.position;

            if (timer > backTime)
            {
                Walking();
                chase = false;
                agent.speed = 2.5f;
                backMusic.SetActive(false);
                //���� ��ġ�� ����
                agent.SetDestination(original.position);
            }
            

            if (Vector3.Distance(transform.position, targetPos) <= 2.0f)
            {
                if (agent.velocity == Vector3.zero)
                {
                    Stoping();
                    
                    timer = 0;
                    chasingSituation = 0;
                    
                }
            }
        }

    }
}
