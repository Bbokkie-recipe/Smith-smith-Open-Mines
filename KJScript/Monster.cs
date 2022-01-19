using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    //죽으면 받아올 래그돌 오브젝트
    public GameObject deadBody;

    //추격을 하는 대상
    public Transform target;

    public bool isRange = false;

    Animator animator;
    NavMeshAgent navMesh;

    public int lifeCount;

    bool isDelay = true;

    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Weapon")
        {
            //부자연스러워보여서 제거
            //animator.SetTrigger("Hit");
            lifeCount--;
            if (lifeCount == 0)
            {
                deadBody.gameObject.SetActive(true);
                Collider[] colliders = deadBody.GetComponentsInChildren<Collider>();
                foreach (Collider hit in colliders)
                {
                    hit.gameObject.AddComponent<TestColision>();
                }
                gameObject.SetActive(false);
                FindObjectOfType<Portal>().monsterCount--;
            }
        }
    }

    private void Update()
    {
        EnemyChase();

        if (Vector3.Distance(transform.position, target.transform.position) < 2f)
        {
            animator.SetBool("Move", false);
            if (isDelay)
                StartCoroutine(EnemyAttack());
        }
    }
    public IEnumerator EnemyAttack()
    {
        Debug.Log(target.name + "가 공격범위에 들어옴");

        animator.SetBool("Move", false);
        animator.SetTrigger("Attack");

        isDelay = false;

        yield return new WaitForSeconds(3);

        isDelay = true;

        yield return null;
    }

    private void EnemyChase()
    {
        if (isRange == true)
        {
            animator.SetBool("Move", true);
            navMesh.SetDestination(target.transform.position);
        }
        else if (isRange == false)
        {
            if (navMesh.velocity == Vector3.zero)
            {
                animator.SetBool("Move", false);
            }
        }
    }
}
