using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wixard_move : IAlly
{
    Animator anim;
    [SerializeField] float speedForward = 3;
    float speedLeft;
    [SerializeField] float speedL =1;
    [SerializeField] float waitTime = 5f;
    [SerializeField] float attackRange;
    [SerializeField] GameObject fireBall;
    float zPosition = -1.3f;
    public Enemy targetEnemy;
    bool isAttack = false;
    [SerializeField] GameObject manegeSp;
    ManegeSpawn manegeSpawn;
    public override LivesManagement livesAlly { get; protected set; }
    void Start()
    {
        livesAlly = new LivesManagement(50);
        manegeSpawn = manegeSp.GetComponent<ManegeSpawn>();
        manegeSpawn.RegistrAlly(this);
        speedLeft = Random.Range(-speedL, speedL);
        anim = GetComponent<Animator>();
        
    }
    void Update()
    {
        Moving();

        if (targetEnemy == null)
        {
            Enemy nearestEnemy = GetNearestEnemy();
            if (nearestEnemy != null&& Vector3.Distance(transform.position, nearestEnemy.transform.position) <= attackRange)
            {
                targetEnemy = nearestEnemy;
                
            }
        }
        if(targetEnemy != null&& !isAttack)
        {
            //����� ����. ����� �������� ��������� ���� �� ����� �����
            isAttack = true;
            StartCoroutine(WaidMageAtack()); 
        }
        if(targetEnemy == null && isAttack)
        {
            anim.SetBool("IsAttack", false);
            isAttack = false;
            speedLeft = Random.Range(-speedL, speedL);
            speedForward = 3;
        }
    }
    private void FixedUpdate()
    {
        if (fireBall!=null&& targetEnemy!=null)
        {
            fireBall.transform.Translate((targetEnemy.transform.position - fireBall.transform.position).normalized * Time.deltaTime * 3);
        }
    }

    private List<IEnemy> GetEnemiesInRange()
    {
        List<IEnemy> enemiesInRange = new List<IEnemy>();
        foreach (IEnemy item in manegeSpawn.EnemyList)
        {
            if (item!=null)
            {
                if (Vector3.Distance(transform.position, item.transform.position) <= attackRange)
                {
                    enemiesInRange.Add(item);
                    Debug.Log(Vector3.Distance(transform.position, item.transform.position));
                }
            }
        }
        return enemiesInRange;
    }
    Enemy GetNearestEnemy()
    {
        Enemy nearestEnemy = null;
        float smolestDistanse = float.PositiveInfinity;
            foreach (Enemy item in GetEnemiesInRange())
            {
                if (Vector3.Distance(transform.position, item.transform.position) < smolestDistanse)
                {
                    smolestDistanse = Vector3.Distance(transform.position, item.transform.position);
                    nearestEnemy = item;
                }
            }
        
        return nearestEnemy;
    }
    //��� ������ ����� ���� ������� ��������� � ��������
    public IEnumerator WaidMageAtack()
    {
            speedLeft = 0;
            anim.SetBool("IsAttack", true);
            speedForward = 0;
        while (targetEnemy != null)
        {
            Attack();
                yield return new WaitForSeconds(waitTime);
        }
        
    }

    public void Attack()
    {
        GameObject fBall = Instantiate(fireBall, transform.position, fireBall.transform.rotation);
        fBall.GetComponent<FireBallMoving>().enemy = targetEnemy.gameObject;
    }

    public void Moving()
    {
        //�������� ����
        transform.Translate(Vector3.forward * speedForward * Time.deltaTime);


        //���� ���� �� �� ������ � ������ ����������� 
        if (transform.position.z < -1 && transform.position.z > -2)
        {
            
            transform.Rotate(Vector3.up, speedLeft * Time.deltaTime);
        }
        else
        {
            transform.Rotate(Vector3.down, speedLeft * Time.deltaTime);
        }
    }
}
