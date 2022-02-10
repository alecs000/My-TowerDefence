using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wixard_move : IAlly
{
    Animator anim;
    [SerializeField] float speedForward = 1;
    //Для того чтобы speedLeft можно было приравнивать к нулю и при востановлении он не менял значения
    float speedLeftBase;
    float speedLeft;
    [SerializeField] float speedL =1;
    [SerializeField] float waitTime = 5f;
    [SerializeField] float attackRange;
    [SerializeField] GameObject fireBall;
    public Enemy targetEnemy;
    bool isAttack = false;
    GameObject manegeSp;
    MonsterPool monsterPool;
    public PoolMono<IEnemy> poolMonster;
    ManegeSpawn manegeSpawn;

    public override LivesManagement livesAlly { get; protected set; }
    void Start()
    {
        livesAlly = new LivesManagement(50);
        manegeSp = GameObject.FindWithTag("GameManager");
        speedLeftBase = Random.Range(-speedL, speedL);
        speedLeft = speedLeftBase;
        anim = GetComponent<Animator>();
        monsterPool = manegeSp.GetComponent<MonsterPool>();
        manegeSpawn = manegeSp.GetComponent<ManegeSpawn>();
        manegeSpawn.RegistrAlly(this);
    }
    private List<IEnemy> GetEnemiesInRange()
    {
        List<IEnemy> enemiesInRange = new List<IEnemy>();
        foreach (IEnemy item in monsterPool.poolM)
        {
            if (item.gameObject.activeInHierarchy)
            {
                if (Vector3.Distance(transform.position, item.transform.position) <= attackRange)
                {
                    enemiesInRange.Add(item);
                }
            }
        }
        return enemiesInRange;
    }
    public IEnemy GetNearestEnemy()
    {
        IEnemy nearestEnemy = null;
        float smolestDistanse = float.PositiveInfinity;
        foreach (IEnemy item in GetEnemiesInRange())
        {
            if (Vector3.Distance(transform.position, item.transform.position) < smolestDistanse)
            {
                smolestDistanse = Vector3.Distance(transform.position, item.transform.position);
                nearestEnemy = item;
            }
        }
        return nearestEnemy;
    }
    void Update()
    {
        if (targetEnemy!=null&&!targetEnemy.gameObject.activeInHierarchy)
        {
            targetEnemy = null;
        }
        Moving();
        if (targetEnemy == null)
        {
            Enemy nearestEnemy = (Enemy)GetNearestEnemy();
            if (nearestEnemy != null&& Vector3.Distance(transform.position, nearestEnemy.transform.position) <= attackRange)
            {
                targetEnemy = nearestEnemy;
                
            }
        }
        if(targetEnemy != null&& !isAttack)
        {
            //Атака мага. Вызов корутины остановки мага на время атаки
            isAttack = true;
            StartCoroutine(WaidMageAtack()); 
        }
        if(targetEnemy == null && isAttack)
        {
            //чтобы маг шел снова после убийства врага
            anim.SetBool("IsAttack", false);
            isAttack = false;
            speedLeft = speedLeftBase;
            speedForward = 1;
        }
    }

    private void FixedUpdate()
    {
        if (fireBall!=null&& targetEnemy!=null)
        {
            fireBall.transform.Translate((targetEnemy.transform.position - fireBall.transform.position).normalized * Time.deltaTime * 3);
        }
    }

   
    //Вся логика атаки мага включая остановку и анимацию
    public IEnumerator WaidMageAtack()
    {
        anim.SetBool("IsAttack", true);
        speedLeft = 0;
            speedForward = 0;
        yield return new WaitForSeconds(0.6f);
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
        //Движение мага
        transform.Translate(Vector3.forward * speedForward * Time.deltaTime);


        //Маги идут не по прямой а чучуть сворачивают 
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
