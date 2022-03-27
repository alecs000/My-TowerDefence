using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowManager : MonoBehaviour
{
    [SerializeField] float speed;
    public IEnemy enemy;
    GameObject manegeSp;
    MonsterPool monsterPool;
    void Start()
    {
        manegeSp = GameObject.FindWithTag("GameManager");
        //Компонент MonsterPool
        monsterPool = manegeSp.GetComponent<MonsterPool>();
        enemy = GetNearestEnemy();
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
    void LateUpdate()
    {
        float step = speed * Time.deltaTime;
        if (enemy.gameObject.activeInHierarchy)
        {
            //transform.Translate((enemy.transform.position - transform.position).normalized * Time.deltaTime * speed);
            transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position, step);
            transform.LookAt(enemy.transform);
        }
        if (!enemy.gameObject.activeInHierarchy)
        {
            Destroy(this.gameObject);
        }
    }
}
