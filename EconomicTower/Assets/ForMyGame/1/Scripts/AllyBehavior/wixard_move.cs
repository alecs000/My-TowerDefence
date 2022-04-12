using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wixard_move : IAlly
{
    [SerializeField] short lives = 50;
    public Animator anim;
    [SerializeField] float speedForward = 1;
    //ƒл€ того чтобы speedLeft можно было приравнивать к нулю и при востановлении он не мен€л значени€
    float speedLeftBase;
    float speedLeft;
    [SerializeField] float speedL =1;
    [SerializeField] float waitTime = 5f;
    [SerializeField] float attackRange;
    [SerializeField] float attack =20;
    [SerializeField] GameObject fireBall;
    [SerializeField] AudioClip clip;
    [SerializeField] GameObject fireBallBlue;
    AudioSource audioSource;
    public Enemy targetEnemy;
    bool isAttack = false;
    GameObject manegeSp;
    MonsterPool monsterPool;
    public PoolMono<IEnemy> poolMonster;
    ManegeSpawn manegeSpawn;
    public static bool upSpeed = false;
    public static bool isBlueFireBall;
    public static bool isVampire;
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
        //ускор€ем если прокачена 1 способность
        if (upSpeed)
        {
            anim.SetFloat("speedAttack", 1.0f*1.3f);
            waitTime *= 0.7f;
        }
        Enemy.Mageattack = attack;
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
    void Update()
    {
        if (transform.position.z < -3 && transform.position.z > 0)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        if (targetEnemy!=null&&!targetEnemy.gameObject.activeInHierarchy)
        {
            targetEnemy = null;
        }
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
            //јтака мага. ¬ызов корутины остановки мага на врем€ атаки
            isAttack = true;
            StartCoroutine(WaidMageAtack()); 
        }
        if(targetEnemy == null && !isAttack)
        {
            //чтобы маг шел снова после убийства врага
            anim.SetBool("IsAttack", false);
            speedLeft = speedLeftBase;
            speedForward = 1;
        }
        Moving();
    }

    private void FixedUpdate()
    {
        if (fireBall!=null&& targetEnemy!=null)
        {
            fireBall.transform.Translate((targetEnemy.transform.position - fireBall.transform.position).normalized * Time.deltaTime * 3);
        }
    }

   
    //¬с€ логика атаки мага включа€ остановку и анимацию
    public IEnumerator WaidMageAtack()
    {
        anim.SetBool("IsAttack", true);
        speedLeft = 0;
        speedForward = 0;
        bool endAttack;
        yield return new WaitForSeconds(0.6f);
        while (targetEnemy != null&&isAttack)
        {
            Attack();
            if (isVampire)
            {
                livesAlly.AddLives(Enemy.Mageattack/9);
                Debug.Log(Enemy.Mageattack);
            }
            
            yield return new WaitForSeconds(waitTime);
        }
        isAttack = false;
    }

    public void Attack()
    {
        audioSource.PlayOneShot(clip);
        if (!isBlueFireBall)
        {
            GameObject fBall = Instantiate(fireBall, transform.position, fireBall.transform.rotation);
            fBall.GetComponent<FireBallMoving>().enemy = targetEnemy.gameObject;
        }
        else
        {
            GameObject fBallBlue = Instantiate(fireBallBlue, transform.position, fireBallBlue.transform.rotation);
            fBallBlue.GetComponent<FireBallMoving>().enemy = targetEnemy.gameObject;
        }
    }

    public void Moving()
    {
        //ƒвижение мага
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
    }
}
