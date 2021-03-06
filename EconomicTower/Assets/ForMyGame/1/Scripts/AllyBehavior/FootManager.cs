using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootManager : IAlly
{
    [SerializeField] float lives = 100;
    [SerializeField] float attack = 10;
    [SerializeField] float waitTime = 5f;
    GameObject manegeSp;
    [SerializeField] float speedForward = 3;
    [SerializeField] float attackRang;
    [SerializeField] float speed;
    [SerializeField] float speedL = 1;
    [SerializeField] AudioClip clip;
    AudioSource audioSource;
    Animator anim;
    //??? ???? ????? speedLeft ????? ???? ???????????? ? ???? ? ??? ????????????? ?? ?? ????? ????????
    float speedLeftBase;
    float speedLeft;
    public Enemy targetEnemy;
    bool isAttack = false;
    ManegeSpawn manegeSpawn;
    float navigatorTime;
    MonsterPool monsterPool;
    public static bool upSpeedAfterKill;
    public static bool upSpeed;
    public static bool smallCopy;
    public static bool isHpBoost;
    public static bool isBerserk;
    public static bool isAppear;
    public static bool isBersrkActive;
    public override LivesManagement livesAlly { get; protected set; }
    void Start()
    {
        lives = lives * UpgrateMemory.increaseHP;
        attack = attack * UpgrateMemory.increaseAttack;
        audioSource = GetComponent<AudioSource>();
        speedLeftBase = Random.Range(-speedL, speedL);
        manegeSp = GameObject.FindWithTag("GameManager");
        //????????? MonsterPool
        monsterPool = manegeSp.GetComponent<MonsterPool>();
        manegeSpawn = manegeSp.GetComponent<ManegeSpawn>();
        //???????? ????? ???? 
        speedLeft = speedLeftBase;
        anim = GetComponent<Animator>();
        manegeSpawn.RegistrAlly(this);
        if (isHpBoost)
        {
            lives = lives / 2 + lives;
        }
        if (isAppear)
        {
            StartCoroutine(Appear());
        }
        //?????
        livesAlly = new LivesManagement(lives);
    }
    void Update()
    {
        if (!isBersrkActive&& livesAlly.lives<lives&& isBerserk)
        {
            isBersrkActive = true;
            attack *= 2;
        }
        if (transform.position.z < -3 && transform.position.z > 0)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        if (targetEnemy != null && !targetEnemy.gameObject.activeInHierarchy)
        {
            targetEnemy = null;
        }
        if (targetEnemy != null && !isAttack)
        {
            //????? ?????. ????? ???????? ????????? ????? ?? ????? ?????
            isAttack = true;
            StartCoroutine(WaidKnightAtack());
        }
        if (targetEnemy == null && !isAttack)
        {
            anim.SetBool("IsAttack", false);
            speedForward = 1;
            speedLeft = speedLeftBase;
        }
        Moving();
    }
    public override float GetAttack()
    {
        return attack;
    }
    public override float GetWaidTimeAttack()
    {
        return waitTime;
    }
    public override float GetLives()
    {
        return lives;
    }
    public IEnumerator Appear()
    {
        yield return new WaitForSeconds(10);
        anim.SetBool("IsUseUlt", true);
        livesAlly.AddLives(livesAlly.lives);
        attack *= 2;
        waitTime /= 2;
        anim.SetFloat("SpeedAttack", 2);
        float att = attack;
        attack = 0;
        yield return new WaitForSeconds(3);
        attack = att;
        anim.SetBool("IsUseUlt", false);
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
    //??? ?????? ????? ?????? ??????? ????????? ? ????????
    public IEnumerator WaidKnightAtack()
    {
        Attack();
        yield return new WaitForSeconds(0.3f);
        while (targetEnemy != null)
        {
            if (targetEnemy.livesEnemy.lives > 0)
            {
                audioSource.PlayOneShot(clip);
                targetEnemy.livesEnemy.RemoveLives(attack);
                Debug.Log(targetEnemy.livesEnemy.lives);
            }
            if (targetEnemy != null && targetEnemy.livesEnemy.lives <= 0)
            {
                targetEnemy.RemoveEnemy();
                targetEnemy = null;
                break;
            }
            yield return new WaitForSeconds(waitTime);
        }
        isAttack = false;
    }

    public void Attack()
    {
        anim.SetBool("IsAttack", true);
        speedForward = 0;
        speedLeft = 0;
    }

    public void Moving()
    {
        //???????? ??????
        transform.Translate(Vector3.forward * speedForward * Time.deltaTime);


        //?????? ???? ?? ?? ?????? ? ?????? ??????????? 
        if (transform.position.z < -1 && transform.position.z > -2)
        {

            transform.Rotate(Vector3.up, speedLeft * Time.deltaTime);
        }
        else
        {
            transform.Rotate(Vector3.down, speedLeft * Time.deltaTime);
        }

        if (targetEnemy == null)
        {
            Enemy nearestEnemy = GetNearestEnemy() as Enemy;
            if (nearestEnemy != null)
            {
                if (Vector3.Distance(transform.position, nearestEnemy.transform.position) <= attackRang)
                {
                    targetEnemy = nearestEnemy;
                }
                else
                {
                    navigatorTime += Time.deltaTime * speed;
                    transform.position = Vector3.MoveTowards(transform.position, nearestEnemy.transform.position, navigatorTime);
                    transform.LookAt(nearestEnemy.transform);
                }

            }

        }
    }
}
