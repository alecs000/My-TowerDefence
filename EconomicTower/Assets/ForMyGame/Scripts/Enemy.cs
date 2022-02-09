using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : IEnemy
{
    GameObject manegeSp;
    
    ManegeSpawn manegeSpawn;
    public override LivesManagement livesEnemy { get; protected set; }
    [SerializeField] IAlly targetAlly;
    bool isAttack;
    [SerializeField] float speedForward;
    Animator anim;
    [SerializeField] float waitTime;
    //„тобы оставить место дл€ анимации
    [SerializeField] float attackRang;
    float navigatorTime;
    [SerializeField] float speed;
    [SerializeField] short attack = 5;
    //ƒл€ того чтобы speedLeft можно было приравнивать к нулю и при востановлении он не мен€л значени€
    float speedLeftBase;
    float speedLeft;
    [SerializeField] float speedL = 1;
    MonsterPool monsterPool;
    //в пуле enemy сперва активен а на не надо чтобы он при по€влении в пуле регестрировалс€ в основном списке
    bool TrF; 

    private void Awake()
    {
        manegeSp = GameObject.FindWithTag("GameManager");
        anim = GetComponent<Animator>();
        manegeSpawn = manegeSp.GetComponent<ManegeSpawn>();
        monsterPool = manegeSp.GetComponent<MonsterPool>();
    }
    private void OnEnable()
    {
        if (this.gameObject.activeInHierarchy)
        {
            monsterPool.poolM.Add(this);
        }
        TrF = true;
        speedLeftBase = Random.Range(-speedL, speedL);
        speedLeft = speedLeftBase;
        livesEnemy = new LivesManagement(100);
        speedForward = 1f;
    }
    private void Update()
    {
        //Debug.Log(livesEnemy.lives);
        Moving();
        if (targetAlly != null && !isAttack)
        {
            //јтака врага. ¬ызов корутины остановки врага на врем€ атаки
            isAttack = true;
            StartCoroutine(WaidEnemyAtack());
        }
        if (targetAlly == null && isAttack)
        {
            anim.SetBool("IsAttack", false);
            isAttack = false;
            speedForward = 1f;
            speedLeft = speedLeftBase;
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
                Debug.Log(targetAlly);
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
        //—толкновение с огненным шаром смерть или получение урона
        if (other.CompareTag("FireBall"))
        {
            Destroy(other.gameObject);
            livesEnemy.RemoveLives(15);
            Debug.Log(livesEnemy.lives);
            
        }
        if (livesEnemy.lives<= 0)
        {
            targetAlly = null;
            monsterPool.poolM.Remove(this);
            this.gameObject.SetActive(false);
            CoinsMangement.AddCoins(5);
        }
    }

        IAlly GetNearestAlly()
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
        //ƒвижение монстра
        transform.Translate(Vector3.forward * speedForward * Time.deltaTime);
        //ћаги идут не по пр€мой а чучуть сворачивают 
        if (transform.position.z < -1 && transform.position.z > -2)
        {
            transform.Rotate(Vector3.up, speedLeft * Time.deltaTime);
        }
        else
        {
            transform.Rotate(Vector3.down, speedLeft * Time.deltaTime);
        }
        navigatorTime += Time.deltaTime * speed;
        if (targetAlly == null)
        {
            IAlly nearestAlly = GetNearestAlly();
            if (nearestAlly != null)
            {
                if (Vector3.Distance(transform.position, nearestAlly.transform.position) <= attackRang)
                {
                    targetAlly = nearestAlly;
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, nearestAlly.transform.position, navigatorTime);
                    transform.LookAt(nearestAlly.transform);
                }

            }

        }
    }
}
