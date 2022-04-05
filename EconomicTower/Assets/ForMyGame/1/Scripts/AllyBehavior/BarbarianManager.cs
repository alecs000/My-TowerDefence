using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarbarianManager : IAlly
{
    [SerializeField] short lives = 100;
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
    //Для того чтобы speedLeft можно было приравнивать к нулю и при востановлении он не менял значения
    float speedLeftBase;
    float speedLeft;
    public Enemy targetEnemy;
    bool isAttack = false;
    ManegeSpawn manegeSpawn;
    float navigatorTime;
    MonsterPool monsterPool;
    public static bool UpAttack5Sec;
    public static bool UpAttackWhenAtack;
    public static bool UpAttack50;
    public override LivesManagement livesAlly { get; protected set; }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        speedLeftBase = Random.Range(-speedL, speedL);
        manegeSp = GameObject.FindWithTag("GameManager");
        //жизни
        livesAlly = new LivesManagement(lives);
        //Компонент MonsterPool
        monsterPool = manegeSp.GetComponent<MonsterPool>();
        manegeSpawn = manegeSp.GetComponent<ManegeSpawn>();
        //Скорость вверз вниз 
        speedLeft = speedLeftBase;
        anim = GetComponent<Animator>();
        manegeSpawn.RegistrAlly(this);
        if (UpAttack5Sec)
        {
            StartCoroutine(GetAttackEveryFiveSecund());
        }
        if (UpAttack50)
        {
            attack += attack * 0.5f;
        }
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
        if (targetEnemy != null && !isAttack)
        {
            //Атака врага. Вызов корутины остановки врага на время атаки
            isAttack = true;
            StartCoroutine(WaidKnightAtack());
            Debug.Log("AAAAAAAAAAAAAAAAAAAAAA");
        }
        if (targetEnemy == null && !isAttack)
        {
            anim.SetBool("IsAttack", false);
            speedForward = 1;
            speedLeft = speedLeftBase;
        }
        Moving();
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

    IEnumerator GetAttackEveryFiveSecund()
    {
        yield return new WaitForSeconds(5);
        attack += attack * 0.1f;
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
    private List<Enemy> GetEnemiesInRange()
    {
        List<Enemy> enemiesInRange = new List<Enemy>();
        foreach (IEnemy item in monsterPool.poolM)
        {
            if (item.gameObject.activeInHierarchy)
            {
                if (Vector3.Distance(transform.position, item.transform.position) <= attackRang)
                {
                    enemiesInRange.Add(item as Enemy);
                }
            }
        }
        return enemiesInRange;
    }
    //Вся логика атаки рыцаря включая остановку и анимацию
    public IEnumerator WaidKnightAtack()
    {
        Attack();
        yield return new WaitForSeconds(2);
        //yield return new WaitForSeconds(1);
        while (targetEnemy != null)
        {
            if (targetEnemy.livesEnemy.lives > 0)
            {
                audioSource.PlayOneShot(clip);
                targetEnemy.livesEnemy.RemoveLives(attack);
                Debug.Log(waitTime);
                if (UpAttackWhenAtack)
                {
                    attack += attack * 0.2f;
                }
                if (UpAttack50)
                {
                    foreach (var item in GetEnemiesInRange())
                    {
                        if (item != targetEnemy)
                        {
                            item.livesEnemy.RemoveLives(attack);
                        }
                        if (item != null && item.livesEnemy.lives <= 0)
                        {
                            item.RemoveEnemy();
                        }
                    }
                }
            }
            if (targetEnemy != null && targetEnemy.livesEnemy.lives <= 0)
            {
                targetEnemy.RemoveEnemy();
                targetEnemy = null;
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
    }
}

