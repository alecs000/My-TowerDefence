using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IEnemy
{
    [SerializeField] GameObject manegeSp;
    
    ManegeSpawn manegeSpawn;
    LivesManagement livesManagement = new LivesManagement(100);
    [SerializeField] wixard_move targetWizard;
    bool isAttack;
    [SerializeField] float speedForward;
    Animator anim;
    [SerializeField] float waitTime;
    [SerializeField] float attackRang;
    float navigatorTime;
    [SerializeField] float speed;

    private void Start()
    {
        anim = GetComponent<Animator>();
        manegeSpawn = manegeSp.GetComponent<ManegeSpawn>();
        manegeSpawn.RegistrEnemy(this);
    }
    private void Update()
    {
        Moving();
        if (targetWizard != null && !isAttack)
        {
            //Атака мага. Вызов корутины остановки мага на время атаки
            isAttack = true;
            StartCoroutine(WaidMageAtack());
        }
        if (targetWizard == null && isAttack)
        {
            anim.SetBool("IsAttack", false);
            isAttack = false;
            speedForward = 3;
        }
    }
    public IEnumerator WaidMageAtack()
    {
        Attack();
        while (targetWizard != null)
        {
            if (targetWizard.livesWizard.lives>0)
            {
                targetWizard.livesWizard.RemoveLives(5);
                yield return new WaitForSeconds(waitTime);
            }
            else
            {
                Destroy(targetWizard.gameObject);
                manegeSpawn.RemoveAlly(targetWizard);
                targetWizard = null;
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FireBall"))
        {
            Destroy(other.gameObject);
            livesManagement.RemoveLives(15);
            
        }
        if (livesManagement.lives<= 0)
        {
            manegeSpawn.RemoveEnemy(this);
            CoinsMangement.AddCoins(5);
        }
    }
    wixard_move GetNearestWizard()
    {
        wixard_move nearestWizard = null;
        float smolestDistanse = float.PositiveInfinity;
        if (manegeSpawn.AllyList.Count !=0)
        {
            foreach (wixard_move item in manegeSpawn.AllyList)
            {
                if (item != null)
                {
                    if (Vector3.Distance(transform.position, item.transform.position) < smolestDistanse)
                    {
                        smolestDistanse = Vector3.Distance(transform.position, item.transform.position);
                        nearestWizard = item;
                    }
                }
            }
        }
        return nearestWizard;
    }

    public void Attack()
    {
        anim.SetBool("IsAttack", true);
        speedForward = 0;
    }

    public void Moving()
    {
        navigatorTime += Time.deltaTime * speed;
        //Движение монстра
        transform.Translate(Vector3.forward * speedForward * Time.deltaTime);
        if (targetWizard == null)
        {
            wixard_move nearestWizard = GetNearestWizard();
            if (nearestWizard != null)
            {
                if (Vector3.Distance(transform.position, nearestWizard.transform.position) <= attackRang)
                {
                    targetWizard = nearestWizard;
                }
                else
                {
                    transform.position = Vector3.MoveTowards(transform.position, nearestWizard.transform.position, navigatorTime);
                }

            }

        }
    }
}
