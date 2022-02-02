using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightManagement : IAlly
{

    [SerializeField] float waitTime = 5f;
    [SerializeField] GameObject manegeSp;
    [SerializeField] float speedForward = 3;
    [SerializeField] float attackRang;
    [SerializeField] float speed;
    Animator anim;
    float speedLeft;
    float zPosition = -1.3f;
    public IEnemy targetEnemy;
    bool isAttack = false;
    ManegeSpawn manegeSpawn;
    float navigatorTime;
    
    public override LivesManagement livesAlly { get; protected set; }
    void Start()
    {
        //�����
        livesAlly = new LivesManagement(100);
        //��������� ManegeSpawn
        manegeSpawn = manegeSp.GetComponent<ManegeSpawn>();
        //������� � ������ ���������
        manegeSpawn.RegistrAlly(this);
        //�������� ����� ���� 
        speedLeft = Random.Range(-0.3f, 0.3f);
        anim = GetComponent<Animator>();

    }
    void Update()
    {
        Moving();

        if (targetEnemy != null && !isAttack)
        {
            //����� �����. ����� �������� ��������� ����� �� ����� �����
            isAttack = true;
            StartCoroutine(WaidKnightAtack());
        }
        if (targetEnemy == null && isAttack)
        {
            anim.SetBool("IsAttack", false);
            isAttack = false;
            speedForward = 3;
            speedLeft = Random.Range(-0.3f, 0.3f);
        }
    }


    Enemy GetNearestEnemy()
    {
        Enemy nearestEnemy = null;
        float smolestDistanse = float.PositiveInfinity;
        foreach (Enemy item in manegeSpawn.EnemyList)
        {
            if (item != null)
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
                targetEnemy.livesEnemy.RemoveLives(3);
                yield return new WaitForSeconds(waitTime);
            }
            else
            {
                Destroy(targetEnemy.gameObject);
                manegeSpawn.RemoveEnemy(targetEnemy);
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
        if (zPosition < 0 && zPosition > -3)
        {
                transform.Translate(Vector3.left * speedLeft * Time.deltaTime);
        }
        navigatorTime += Time.deltaTime * speed;
        if (targetEnemy == null)
        {
            IEnemy nearestKnight = GetNearestEnemy();
            if (nearestKnight != null)
            {
                if (Vector3.Distance(transform.position, nearestKnight.transform.position) <= attackRang)
                {
                    targetEnemy = nearestKnight;
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, nearestKnight.transform.position, navigatorTime);
                }

            }

        }
    }
}
