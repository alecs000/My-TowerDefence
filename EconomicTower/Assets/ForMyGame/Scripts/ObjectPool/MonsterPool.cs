using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPool : MonoBehaviour
{
    public List<IEnemy> poolM;
    [SerializeField] int poolCountShell = 10;
    [SerializeField] bool autoExpandShell = true;
    [SerializeField] IEnemy enemyPrefabShell;
    public PoolMono<IEnemy> poolMShell;
    [SerializeField] int poolCountSlime = 10;
    [SerializeField] bool autoExpandSlime = true;
    [SerializeField] IEnemy enemyPrefabSlime;
    public PoolMono<IEnemy> poolMSlime;
    private void Start()
    {
        poolM = new List<IEnemy>();
        poolMShell = new PoolMono<IEnemy>(enemyPrefabShell, poolCountShell, this.transform);
        poolMShell.autoExpand = autoExpandShell;
        poolMSlime = new PoolMono<IEnemy>(enemyPrefabSlime, poolCountSlime, this.transform);
        poolMSlime.autoExpand = autoExpandSlime;

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
       IEnemy shell = poolMShell.GetFreeElement();
        shell.transform.position = new Vector3(-15, 0, -1.2f);

    }
}
