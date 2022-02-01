using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightManagement : MonoBehaviour,IAlly
{
    Animator anim;
    [SerializeField] float speedForward = 3;
    float speedLeft;
    [SerializeField] float waitTime = 5f;
    [SerializeField] float attackRange;
    [SerializeField] GameObject fireBall;
    float zPosition = -1.3f;
    byte downOrFloat;
    public Enemy targetEnemy;
    bool isAttack = false;
    [SerializeField] GameObject manegeSp;
    ManegeSpawn manegeSpawn;
    public LivesManagement livesWizard = new LivesManagement(50);
    void Start()
    {
        manegeSpawn = manegeSp.GetComponent<ManegeSpawn>();
        manegeSpawn.RegistrAlly(this);
        speedLeft = Random.Range(0, 0.3f);
        downOrFloat = (byte)Random.Range(0, 2);
        anim = GetComponent<Animator>();

    }
    void Update()
    {
        Moving();

        if (targetEnemy == null)
        {
            Enemy nearestEnemy = GetNearestEnemy();
            if (nearestEnemy != null && Vector3.Distance(transform.position, nearestEnemy.transform.position) <= attackRange)
            {
                targetEnemy = nearestEnemy;

            }
        }
        if (targetEnemy != null && !isAttack)
        {
            //Атака мага. Вызов корутины остановки мага на время атаки
            isAttack = true;
            StartCoroutine(WaidMageAtack());
        }
        if (targetEnemy == null && isAttack)
        {
            anim.SetBool("IsAttack", false);
            isAttack = false;
            speedLeft = Random.Range(0, 0.3f);
            speedForward = 3;
        }
    }
    private void FixedUpdate()
    {
        if (fireBall != null && targetEnemy != null)
        {
            fireBall.transform.Translate((targetEnemy.transform.position - fireBall.transform.position).normalized * Time.deltaTime * 3);
        }
    }

    private List<Enemy> GetEnemiesInRange()
    {
        List<Enemy> enemiesInRange = new List<Enemy>();
        foreach (Enemy item in manegeSpawn.EnemyList)
        {
            if (item != null)
            {
                if (Vector3.Distance(transform.position, item.transform.position) <= attackRange)
                {
                    enemiesInRange.Add(item);
                    Debug.Log(Vector3.Distance(transform.position, item.transform.position));
                }
            }
        }
        return enemiesInRange;
    }
    Enemy GetNearestEnemy()
    {
        Enemy nearestEnemy = null;
        float smolestDistanse = float.PositiveInfinity;
        foreach (Enemy item in GetEnemiesInRange())
        {
            if (Vector3.Distance(transform.position, item.transform.position) < smolestDistanse)
            {
                smolestDistanse = Vector3.Distance(transform.position, item.transform.position);
                nearestEnemy = item;
            }
        }

        return nearestEnemy;
    }
    //Вся логика атаки мага включая остановку и анимацию
    public IEnumerator WaidMageAtack()
    {
        speedLeft = 0;
        anim.SetBool("IsAttack", true);
        speedForward = 0;
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
        if (zPosition < 0 && zPosition > -3)
        {
            if (downOrFloat == 1)
            {
                transform.Translate(Vector3.left * speedLeft * Time.deltaTime);
            }
            else if (downOrFloat == 0)
            {
                transform.Translate(Vector3.left * -speedLeft * Time.deltaTime);
            }
        }
    }
}
