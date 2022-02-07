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
    Animator anim;
    float speedLeft;
    float zPosition = -1.3f;
    public IEnemy targetEnemy;
    bool isAttack = false;
    ManegeSpawn manegeSpawn;
    float navigatorTime;
    MonsterPool monsterPool;

    public override LivesManagement livesAlly { get; protected set; }
    void Start()
    {
        manegeSp = GameObject.FindWithTag("GameManager");
        //жизни
        livesAlly = new LivesManagement(100);
        //Компонент MonsterPool
        monsterPool = manegeSp.GetComponent<MonsterPool>();
        //Скорость вверз вниз 
        speedLeft = Random.Range(-0.3f, 0.3f);
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
            speedLeft = Random.Range(-0.3f, 0.3f);
        }
    }


    IEnemy GetNearestEnemy()
    {
        IEnemy nearestEnemy = null;
        float smolestDistanse = float.PositiveInfinity;
        foreach (IEnemy item in monsterPool.poolM.pool)
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
    //Вся логика атаки мага включая остановку и анимацию
    public IEnumerator WaidKnightAtack()
    {
        Attack();

        while (targetEnemy != null)
        {
            if (targetEnemy.livesEnemy.lives > 0)
            {
                targetEnemy.livesEnemy.RemoveLives(25);
                yield return new WaitForSeconds(waitTime);
            }
            else if (targetEnemy.livesEnemy.lives <= 0)
            {
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
        //Движение рыцаря
        transform.Translate(Vector3.forward * speedForward * Time.deltaTime);


        //Рыцари идут не по прямой а чучуть сворачивают 
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
