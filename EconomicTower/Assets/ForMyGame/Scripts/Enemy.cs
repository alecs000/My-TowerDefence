using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : IEnemy
{
    [SerializeField] GameObject manegeSp;
    
    ManegeSpawn manegeSpawn;
    public override LivesManagement livesEnemy { get; protected set; }
    [SerializeField] IAlly targetAlly;
    bool isAttack;
    [SerializeField] float speedForward;
    Animator anim;
    [SerializeField] float waitTime;
    //Чтобы оставить место для анимации
    [SerializeField] float attackRang;
    float navigatorTime;
    [SerializeField] float speed;
    [SerializeField] short attack = 5;

    private void Start()
    {
        livesEnemy = new LivesManagement(100);
        anim = GetComponent<Animator>();
        manegeSpawn = manegeSp.GetComponent<ManegeSpawn>();
        manegeSpawn.RegistrEnemy(this);
    }
    private void Update()
    {
        Moving();
        if (targetAlly != null && !isAttack)
        {
            //Атака врага. Вызов корутины остановки врага на время атаки
            isAttack = true;
            StartCoroutine(WaidEnemyAtack());
        }
        if (targetAlly == null && isAttack)
        {
            anim.SetBool("IsAttack", false);
            isAttack = false;
            speedForward = 3;
        }
    }
    public IEnumerator WaidEnemyAtack()
    {
        Attack();
        while (targetAlly != null)
        {
            if (targetAlly.livesAlly.lives>0)
            {
                targetAlly.livesAlly.RemoveLives(attack);
                yield return new WaitForSeconds(waitTime);
            }
            else
            {
                Destroy(targetAlly.gameObject);
                manegeSpawn.RemoveAlly(targetAlly);
                targetAlly = null;
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        //Столкновение с огненным шаром смерть или получение урона
        if (other.CompareTag("FireBall"))
        {
            Destroy(other.gameObject);
            livesEnemy.RemoveLives(15);
            
        }
        if (livesEnemy.lives<= 0)
        {
            manegeSpawn.RemoveEnemy(this);
            CoinsMangement.AddCoins(5);
        }
    }
    IAlly GetNearestWizard()
    {
        IAlly nearestAlly = null;
        float smolestDistanse = float.PositiveInfinity;
        if (manegeSpawn.AllyList.Count !=0)
        {
            foreach (IAlly item in manegeSpawn.AllyList)
            {
                if (item != null)
                {
                    if (Vector3.Distance(transform.position, item.transform.position) < smolestDistanse)
                    {
                        smolestDistanse = Vector3.Distance(transform.position, item.transform.position);
                        nearestAlly = item;
                    }
                }
            }
        }
        return nearestAlly;
    }

    public void Attack()
    {
        anim.SetBool("IsAttack", true);
        speedForward = 0;
    }

    public void Moving()
    {
        //Движение монстра
        transform.Translate(Vector3.forward * speedForward * Time.deltaTime);
        navigatorTime += Time.deltaTime * speed;
        if (targetAlly == null)
        {
            IAlly nearestWizard = GetNearestWizard();
            if (nearestWizard != null)
            {
                if (Vector3.Distance(transform.position, nearestWizard.transform.position) <= attackRang)
                {
                    targetAlly = nearestWizard;
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, nearestWizard.transform.position, navigatorTime);
                }

            }

        }
    }
}
