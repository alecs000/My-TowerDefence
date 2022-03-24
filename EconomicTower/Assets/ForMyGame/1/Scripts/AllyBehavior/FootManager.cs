using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootManager : IAlly
{
    [SerializeField] int lives = 100;
    [SerializeField] short attack = 10;
    [SerializeField] float waitTime = 5f;
    GameObject manegeSp;
    [SerializeField] float speedForward = 3;
    [SerializeField] float attackRang;
    [SerializeField] float speed;
    [SerializeField] float speedL = 1;
    [SerializeField] AudioClip clip;
    AudioSource audioSource;
    Animator anim;
    //Для того чтобы speedLeft можно было приравнивать к нулю и при востановлении он не менял значения
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
    public static bool isHpBoost;
    public static bool isBerserk;
    public static bool isAppear;
    public static bool isBersrkActive;
    public override LivesManagement livesAlly { get; protected set; }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        speedLeftBase = Random.Range(-speedL, speedL);
        manegeSp = GameObject.FindWithTag("GameManager");
        
        //Компонент MonsterPool
        monsterPool = manegeSp.GetComponent<MonsterPool>();
        manegeSpawn = manegeSp.GetComponent<ManegeSpawn>();
        //Скорость вверз вниз 
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
        //жизни
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
        
        if (targetEnemy == null && isAttack)
        {
            anim.SetBool("IsAttack", false);
            isAttack = false;
            speedForward = 1;
            speedLeft = speedLeftBase;
        }
        if (targetEnemy != null && !isAttack)
        {
            //Атака врага. Вызов корутины остановки врага на время атаки
            isAttack = true;
            StartCoroutine(WaidKnightAtack());
        }
        Moving();
    }

    public IEnumerator Appear()
    {
        yield return new WaitForSeconds(10);
        anim.SetBool("IsUseUlt", true);
        livesAlly.AddLives(livesAlly.lives);
        attack *= 2;
        waitTime /= 2;
        anim.SetFloat("SpeedAttack", 2);
        short att = attack;
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
    //Вся логика атаки рыцаря включая остановку и анимацию
    public IEnumerator WaidKnightAtack()
    {
        Attack();

        while (targetEnemy != null)
        {
            if (targetEnemy.livesEnemy.lives > 0)
            {
                audioSource.PlayOneShot(clip);
                targetEnemy.livesEnemy.RemoveLives(attack);
                Debug.Log(targetEnemy.livesEnemy.lives);
                yield return new WaitForSeconds(waitTime);
            }
            if (targetEnemy != null && targetEnemy.livesEnemy.lives <= 0)
            {
                MainManager.AddDimond(targetEnemy.dimonds);
                CoinsMangement.AddCoins(targetEnemy.coins);
                EnergyMenegment.AddEnergy(targetEnemy.energy);
                monsterPool.poolM.Remove(targetEnemy);
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
