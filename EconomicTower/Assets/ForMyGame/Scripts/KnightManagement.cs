using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightManagement : IAlly
{

    [SerializeField] float waitTime = 5f;
    GameObject manegeSp;
    [SerializeField] float speedForward = 3;
    [SerializeField] float attackRang;
    [SerializeField] float speed;
    [SerializeField] float speedL = 1;
    Animator anim;
    //��� ���� ����� speedLeft ����� ���� ������������ � ���� � ��� ������������� �� �� ����� ��������
    float speedLeftBase;
    float speedLeft;
    public IEnemy targetEnemy;
    bool isAttack = false;
    ManegeSpawn manegeSpawn;
    float navigatorTime;
    MonsterPool monsterPool;

    public override LivesManagement livesAlly { get; protected set; }
    void Start()
    {

        speedLeftBase = Random.Range(-speedL, speedL);
        manegeSp = GameObject.FindWithTag("GameManager");
        //�����
        livesAlly = new LivesManagement(100);
        //��������� MonsterPool
        monsterPool = manegeSp.GetComponent<MonsterPool>();
        manegeSpawn = manegeSp.GetComponent<ManegeSpawn>();
        //�������� ����� ���� 
        speedLeft = speedLeftBase;
        anim = GetComponent<Animator>();
        manegeSpawn.RegistrAlly(this);
    }
    void Update()
    {
        if (targetEnemy != null && !targetEnemy.gameObject.activeInHierarchy)
        {
            targetEnemy = null;
        }
        Moving();
        if (targetEnemy == null && isAttack)
        {
            anim.SetBool("IsAttack", false);
            isAttack = false;
            speedForward = 1;
            speedLeft = speedLeftBase;
        }
        if (targetEnemy != null && !isAttack)
        {
            //����� �����. ����� �������� ��������� ����� �� ����� �����
            isAttack = true;
            StartCoroutine(WaidKnightAtack());
        }
       
    }


    IEnemy GetNearestEnemy()
    {
        IEnemy nearestEnemy = null;
        float smolestDistanse = float.PositiveInfinity;
        foreach (IEnemy item in monsterPool.poolM)
        {
            if (item.gameObject.activeInHierarchy)
            {
                if (Vector3.Distance(transform.position, item.transform.position) < smolestDistanse)
                {
                    smolestDistanse = Vector3.Distance(transform.position, item.transform.position);
                    nearestEnemy = item;
                }
            }
        }

        return nearestEnemy;
    }
    //��� ������ ����� ���� ������� ��������� � ��������
    public IEnumerator WaidKnightAtack()
    {
        Attack();

        while (targetEnemy != null)
        {
            if (targetEnemy.livesEnemy.lives > 0)
            {
                Debug.Log(targetEnemy.livesEnemy.lives);
                targetEnemy.livesEnemy.RemoveLives(25);
                yield return new WaitForSeconds(waitTime);
            }
            if (targetEnemy.livesEnemy.lives <= 0 && targetEnemy != null)
            {
                monsterPool.poolM.Remove(targetEnemy);
                targetEnemy.gameObject.SetActive(false);
                targetEnemy = null;
            }
        }

    }

    public void Attack()
    {
        anim.SetBool("IsAttack", true);
        speedForward = 0;
        speedLeft = 0;
    }

    public void Moving()
    {
        //�������� ������
        transform.Translate(Vector3.forward * speedForward * Time.deltaTime);


        //������ ���� �� �� ������ � ������ ����������� 
        if (transform.position.z < -1 && transform.position.z > -2)
        {

            transform.Rotate(Vector3.up, speedLeft * Time.deltaTime);
        }
        else
        {
            transform.Rotate(Vector3.down, speedLeft * Time.deltaTime);
        }
        navigatorTime += Time.deltaTime * speed;
        if (targetEnemy == null)
        {
            IEnemy nearestEnemy = GetNearestEnemy();
            if (nearestEnemy != null)
            {
                if (Vector3.Distance(transform.position, nearestEnemy.transform.position) <= attackRang)
                {
                    targetEnemy = nearestEnemy;
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, nearestEnemy.transform.position, navigatorTime);
                    transform.LookAt(nearestEnemy.transform);
                }

            }

        }
    }
}
