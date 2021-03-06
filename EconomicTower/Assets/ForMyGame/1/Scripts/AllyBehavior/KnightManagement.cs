using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightManagement : IAlly
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
    [SerializeField] GameObject smallCopyPrefab;
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
    int stopUpSpeed = 0;
    public override LivesManagement livesAlly { get; protected set; }
    void Start()
    {
        lives = lives * UpgrateMemory.increaseHP;
        attack = attack * UpgrateMemory.increaseAttack;
        audioSource = GetComponent<AudioSource>();
        speedLeftBase = Random.Range(-speedL, speedL);
        manegeSp = GameObject.FindWithTag("GameManager");
        //?????
        livesAlly = new LivesManagement(lives);
        //????????? MonsterPool
        monsterPool = manegeSp.GetComponent<MonsterPool>();
        manegeSpawn = manegeSp.GetComponent<ManegeSpawn>();
        //???????? ????? ???? 
        speedLeft = speedLeftBase;
        anim = GetComponent<Animator>();
        manegeSpawn.RegistrAlly(this);
        if (upSpeed)
        {
            anim.SetFloat("upSpeedKnight", 1.0f * 1.5f);
            waitTime *= 0.75f;
        }
    }
    void Update()
    {
        if (transform.position.z < -3 && transform.position.z > 0)
        {
            transform.rotation= Quaternion.Euler(0, -90, 0);
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
        yield return new WaitForSeconds(0.8f);
        while (targetEnemy != null)
        {
            if (targetEnemy.livesEnemy.lives > 0)
            {
                audioSource.PlayOneShot(clip);
                targetEnemy.livesEnemy.RemoveLives(attack);
            }
            if (targetEnemy != null&&targetEnemy.livesEnemy.lives <= 0)
            {
                if (upSpeedAfterKill && stopUpSpeed<4)
                {
                    anim.SetFloat("upSpeedKnight", 1.0f *2f);
                    waitTime *= 0.5f;
                    stopUpSpeed++;
                }
                if (smallCopy&& smallCopyPrefab!=null)
                {
                    Instantiate(smallCopyPrefab,new Vector3(transform.position.x,0, transform.position.z+Random.Range(-0.5f,0.5f)), smallCopyPrefab.transform.rotation);
                }
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
                    navigatorTime += Time.deltaTime* speed;
                    transform.position = Vector3.MoveTowards(transform.position, nearestEnemy.transform.position, navigatorTime);
                    transform.LookAt(nearestEnemy.transform);
                }

            }

        }
    }
}
