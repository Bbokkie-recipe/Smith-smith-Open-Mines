using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Navigation 기능 사용시 필요
using UnityEngine.AI;
public class Destination : MonoBehaviour
{
    //목표지점
    public Transform target;
    public Transform original;

    NavMeshAgent agent;

    //SpotRange자식 오브젝트를 가져온다
    public MeshCollider spotRange;
    public Vector3 targetPos;

    //시야각 및 추적반경을 제어하는 EnemyFOV클래스를 저장할 변수
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
        //해당 개체의 NavMeshAgent를 참조
        agent = GetComponent<NavMeshAgent>();

        //시야각 및 추적반경을 제어하는 EnemyFOV클래스를 추출
        enemyFOV = GetComponent<EnemyFOV>();

        animator = transform.Find("EnemyModel").GetComponent<Animator>();
    }

    void OnTriggerStay(Collider other)
    {
        //Trigger한 오브젝트의 태그가 Player면 추적
        if (other.gameObject.CompareTag("Player"))
        {
            //플레이어와의 거리에 장애물 여부를 판단
            if (enemyFOV.isViewPlayer() && enemyFOV.isTracePlayer())
            {
                Debug.Log("Enemy spotted");
                targetPos = target.position;
                agent.SetDestination(targetPos);  //장애물이 없으면 추적
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
            //멈춤상태를 임의의 함수로 지정해서 전달

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
                //원래 위치로 복귀
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
