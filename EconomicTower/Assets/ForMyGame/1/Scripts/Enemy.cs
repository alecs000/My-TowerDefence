using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : IEnemy
{
    public override LivesManagement livesEnemy { get; protected set; }
    public static float Mageattack = 15;
    public static float balistaAttack = 100;
    public short coins;
    public short dimonds;
    public short energy;
    public static bool frezzeDie;
    public IAlly targetAlly;
    public static bool isPoisonActive;
    [SerializeField] float speed;
    [SerializeField] short lives;
    [SerializeField] short attack = 5;
    [SerializeField] float speedForward;
    [SerializeField] float speedL = 1;
    [SerializeField] float waitTime;
    //Чтобы оставить место для анимации
    [SerializeField] float attackRang;
    [SerializeField] AudioClip clip;
    [SerializeField] bool isBoss;
    AudioSource audioSource;
    GameObject manegeSp;
    ManegeSpawn manegeSpawn;
    bool isAttack;
    Animator anim;
    float navigatorTime;
    //Для того чтобы speedLeft можно было приравнивать к нулю и при востановлении он не менял значения
    float speedLeftBase;
    float speedLeft;
    MonsterPool monsterPool;
    GameObject target;
    bool animStop;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        manegeSp = GameObject.FindWithTag("GameManager");
        anim = GetComponent<Animator>();
        manegeSpawn = manegeSp.GetComponent<ManegeSpawn>();
        monsterPool = manegeSp.GetComponent<MonsterPool>();
        target = GameObject.FindWithTag("Finish");

    }
    private void OnEnable()
    {
        if (this.gameObject.activeInHierarchy)
        {
            monsterPool.poolM.Add(this);
        }
        speedLeftBase = Random.Range(-speedL, speedL);
        speedLeft = speedLeftBase;
        livesEnemy = new LivesManagement(lives);
        speedForward = 1f;
        targetAlly = null;
    }
    private void Update()
    {
        if (frezzeDie && manegeSpawn.isFreeze&& livesEnemy.lives<100)
        {
            RemoveEnemy();
        }
        if (!manegeSpawn.isBoss&&isBoss)
        {
            manegeSpawn.isBoss = isBoss;
            manegeSpawn.boss = this.gameObject;
        }
        if (!manegeSpawn.isFreeze)
        {
            if (animStop)
            {
                anim.SetBool("StopAll", false);
                animStop = false;
            }
            //Debug.Log(livesEnemy.lives);
            
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
                speedForward = 1f;
                speedLeft = speedLeftBase;
            }
        }
        else
        {
            anim.SetBool("StopAll", true);
            animStop = true;
        }
        if (isBoss && (!this.gameObject.activeInHierarchy || livesEnemy.lives <= 0) && !manegeSpawn.isGameStop)
        {//ОТКЛЮЧАТЬ ПРИ РЕСТАРТЕ
            manegeSpawn.win = true;
            manegeSpawn.isGameStop = true;
            Debug.Log(5555);
            UpgrateMemory.levels.Add(3);

        }
        if (!manegeSpawn.isFreeze)
        {
            Moving();
        }
    }
    public IEnumerator WaidEnemyAtack()
    {
        if (!manegeSpawn.isFreeze)
        {
            Attack();
            while (targetAlly != null)
            {
                if (targetAlly.livesAlly.lives > 0)
                {
                    //Debug.Log("Вражина" + targetAlly.livesAlly.lives);
                    targetAlly.livesAlly.RemoveLives(attack);
                    audioSource.PlayOneShot(clip);
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
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.CompareTag("Finish"))
        {//ОТКЛЮЧАТЬ ПРИ РЕСТАРТЕ
            manegeSpawn.lose = true;
            manegeSpawn.isGameStop = true;
        }
        //Столкновение с огненным шаром смерть или получение урона
        if (other.CompareTag("FireBall"))
        {
            Destroy(other.gameObject);
            livesEnemy.RemoveLives(Mageattack);
        }
        if (other.CompareTag("Boomb"))
        {
            livesEnemy.RemoveLives(100);
        }
        if (other.CompareTag("Arrow"))
        {
            livesEnemy.RemoveLives(30);
            Destroy(other.gameObject);
            if (isPoisonActive)
            {
                StartCoroutine(Poisoned());
            }
        }
        if (other.CompareTag("ArrowBalist"))
        {
            livesEnemy.RemoveLives(30);
            Destroy(other.gameObject);
        }
        if (livesEnemy.lives<= 0)
        {
            if (isBoss && !manegeSpawn.isGameStop)
            {//ОТКЛЮЧАТЬ ПРИ РЕСТАРТЕ
                manegeSpawn.win = true;
                manegeSpawn.isGameStop = true;
                Debug.Log(5555);
                UpgrateMemory.levels.Add(3);

            }
            RemoveEnemy();
        }
    }
    IEnumerator Poisoned()
    {
        int i = 0;
        while (i<=5)
        {
            yield return new WaitForSeconds(1);
            livesEnemy.RemoveLives(10);
        }
    }
    public void RemoveEnemy()
    {
        MainManager.AddDimond(dimonds);
        CoinsMangement.AddCoins(coins);
        EnergyMenegment.AddEnergy(energy);
        targetAlly = null;
        monsterPool.poolM.Remove(this);
        this.gameObject.SetActive(false);
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
        //Движение монстра
        transform.Translate(Vector3.forward * speedForward * Time.deltaTime);
        //Враги идут не по прямой а чучуть сворачивают мб ненужно
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
            if (nearestAlly == null)
            {
                transform.LookAt(target.transform);
            }
        }
    }
}
