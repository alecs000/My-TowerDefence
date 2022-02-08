using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : IEnemy
{
    [SerializeField] GameObject manegeSp;
    
    ManegeSpawn manegeSpawn;
    public override LivesManagement livesEnemy { get; protected set; }
    [SerializeField] IAlly targetAlly;
    [SerializeField] float speedRotate;
    bool isAttack;
    [SerializeField] float speedForward;
    Animator anim;
    [SerializeField] float waitTime;
    //����� �������� ����� ��� ��������
    [SerializeField] float attackRang;
    float navigatorTime;
    [SerializeField] float speed;
    [SerializeField] short attack = 5;

    private void Start()
    {
        manegeSp = GameObject.FindWithTag("GameManager");
        anim = GetComponent<Animator>();
        manegeSpawn = manegeSp.GetComponent<ManegeSpawn>();
    }
    private void OnEnable()
    {
        livesEnemy = new LivesManagement(100);
    }
    private void Update()
    {
        //Debug.Log(livesEnemy.lives);
        Moving();
        if (targetAlly != null && !isAttack)
        {
            //����� �����. ����� �������� ��������� ����� �� ����� �����
            isAttack = true;
            StartCoroutine(WaidEnemyAtack());
        }
        if (targetAlly == null && isAttack)
        {
            anim.SetBool("IsAttack", false);
            isAttack = false;
            speedForward = 3f;
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
        //������������ � �������� ����� ������ ��� ��������� �����
        if (other.CompareTag("FireBall"))
        {
            Destroy(other.gameObject);
            livesEnemy.RemoveLives(15);
            Debug.Log(livesEnemy.lives);
            
        }
        if (livesEnemy.lives<= 0)
        {

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
        //�������� �������
        transform.Translate(Vector3.forward * speedForward * Time.deltaTime);
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
                    transform.LookAt(nearestAlly.transform);//]][][pl[]pjic knight
                }

            }

        }
    }
}
