using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPool : MonoBehaviour
{
    [SerializeField] int poolCount = 10;
    [SerializeField] bool autoExpand = true;
    [SerializeField] IEnemy enemyPrefab;
    PoolMono<IEnemy> pool;
    private void Start()
    {
        this.pool = new PoolMono<IEnemy>(this.enemyPrefab, this.poolCount, this.transform);
        this.pool.autoExpand = autoExpand;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            this.CreateEnemy();
        }
    }
    void CreateEnemy()
    {
        var enemy = this.pool.GetFreeElement();
        enemy.transform.position = new Vector3(-15, 0, -1.1f);
    }
}
