using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightManagement : IAlly
{
    Animator anim;
    [SerializeField] float speedForward = 3;
    float speedLeft;
    [SerializeField] float waitTime = 5f;
    [SerializeField] float attackRange;
    [SerializeField] GameObject fireBall;
    float zPosition = -1.3f;
    byte downOrFloat;
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
        speedLeft = Random.Range(0, 0.3f);
        downOrFloat = (byte)Random.Range(0, 2);
        anim = GetComponent<Animator>();

    }
    void Update()
    {
        Moving();

        if (targetEnemy != null && !isAttack)
        {
            //Атака врага. Вызов корутины остановки врага на время атаки
            isAttack = true;
            StartCoroutine(WaidKnightAtack());
        }
        if (targetEnemy == null && isAttack)
        {
            anim.SetBool("IsAttack", false);
            isAttack = false;
            speedForward = 3;
        }
    }

    private List<Enemy> GetEnemiesInRange()
    {
        List<Enemy> enemiesInRange = new List<Enemy>();
        foreach (Enemy item in manegeSpawn.EnemyList)
        {
            if (item != null)
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
    //Вся логика атаки мага включая остановку и анимацию
    public IEnumerator WaidKnightAtack()
    {
        Attack();
        while (targetEnemy != null)
        {
            if (targetEnemy.livesEnemy.lives > 0)
            {
                targetEnemy.livesEnemy.RemoveLives(5);
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
    }

    public void Moving()
    {
        //Движение мага
        transform.Translate(Vector3.forward * speedForward * Time.deltaTime);


        //Маги идут не по прямой а чучуть сворачивают 
        if (zPosition < 0 && zPosition > -3)
        {
            if (downOrFloat == 1)
            {
                transform.Translate(Vector3.left * speedLeft * Time.deltaTime);
            }
            else if (downOrFloat == 0)
            {
                transform.Translate(Vector3.left * -speedLeft * Time.deltaTime);
            }
        }
    }
}
