using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPool : MonoBehaviour
{
    [SerializeField] int poolCount = 10;
    [SerializeField] bool autoExpand = true;
    [SerializeField] IEnemy enemyPrefab;
    public PoolMono<IEnemy> poolM;
    public bool getNearest;
    public IEnemy enemy1;
    private void Start()
    {
        poolM = new PoolMono<IEnemy>(this.enemyPrefab, this.poolCount, this.transform);
        poolM.autoExpand = autoExpand;
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
        var enemy = this.poolM.GetFreeElement();
        enemy.transform.position = new Vector3(-5, 0, -1.1f);//
    }
}
