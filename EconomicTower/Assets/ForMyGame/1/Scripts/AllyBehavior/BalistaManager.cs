using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalistaManager : IAlly
{
    [SerializeField] short lives = 50;
    public Animator anim;
    [SerializeField] float speedForward = 1;
    //??? ???? ????? speedLeft ????? ???? ???????????? ? ???? ? ??? ????????????? ?? ?? ????? ????????
    float speedLeftBase;
    float speedLeft;
    [SerializeField] float speedL = 1;
    [SerializeField] float waitTime = 5f;
    [SerializeField] float attackRange;
    [SerializeField] float attack;
    [SerializeField] GameObject fireBall;
    [SerializeField] AudioClip clip;
    AudioSource audioSource;
    public Enemy targetEnemy;
    bool isAttack = false;
    GameObject manegeSp;
    MonsterPool monsterPool;
    public PoolMono<IEnemy> poolMonster;
    ManegeSpawn manegeSpawn;
    public override LivesManagement livesAlly { get; protected set; }
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        livesAlly = new LivesManagement(lives);
        manegeSp = GameObject.FindWithTag("GameManager");
        speedLeftBase = Random.Range(-speedL, speedL);
        speedLeft = speedLeftBase;
        anim = GetComponent<Animator>();
        monsterPool = manegeSp.GetComponent<MonsterPool>();
        manegeSpawn = manegeSp.GetComponent<ManegeSpawn>();
        manegeSpawn.RegistrAlly(this);
        //???????? ???? ????????? 1 ???????????
        Enemy.balistaAttack = attack;
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
        if (transform.position.z < -3 && transform.position.z > 0)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        if (targetEnemy != null && !targetEnemy.gameObject.activeInHierarchy)
        {
            targetEnemy.RemoveEnemy();
            targetEnemy = null;
        }
        Moving();
        if (targetEnemy == null)
        {
            Enemy nearestEnemy = (Enemy)GetNearestEnemy();
            if (nearestEnemy != null && Vector3.Distance(transform.position, nearestEnemy.transform.position) <= attackRange)
            {
                targetEnemy = nearestEnemy;

            }
        }
        if (targetEnemy != null && !isAttack)
        {
            //????? ????. ????? ???????? ????????? ???? ?? ????? ?????
            isAttack = true;
            StartCoroutine(WaidMageAtack());
        }
        if (targetEnemy == null && isAttack)
        {
            //????? ??? ??? ????? ????? ???????? ?????
            anim.SetBool("IsAttack", false);
            isAttack = false;
            speedLeft = speedLeftBase;
            speedForward = 1;
        }
    }

    private void FixedUpdate()
    {
        if (fireBall != null && targetEnemy != null)
        {
            fireBall.transform.Translate((targetEnemy.transform.position - fireBall.transform.position).normalized * Time.deltaTime * 3);
        }
    }


    //??? ?????? ????? ???? ??????? ????????? ? ????????
    public IEnumerator WaidMageAtack()
    {
        anim.SetBool("IsAttack", true);
        speedLeft = 0;
        speedForward = 0;
        yield return new WaitForSeconds(0.6f);
        while (targetEnemy != null && isAttack)
        {
            Attack();
            yield return new WaitForSeconds(waitTime);
        }
    }

    public void Attack()
    {
        audioSource.PlayOneShot(clip);
            GameObject fBall = Instantiate(fireBall,new Vector3(transform.position.x, transform.position.y+0.5f, transform.position.z), fireBall.transform.rotation);
    }

    public void Moving()
    {
        //???????? ????
        transform.Translate(Vector3.forward * speedForward * Time.deltaTime);


        //???? ???? ?? ?? ?????? ? ?????? ??????????? 
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
