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
    Vector3 spawnPosition;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(SpawnMonster());
        }
    }
    public IEnumerator SpawnMonster()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                this.CreateShell();
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(1f);
            for (int j = 0; j < 3; j++)
            {
                this.CreatSlime();
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(5f);
        }
       
    }
    void CreateShell()
    {
       IEnemy shell = poolMShell.GetFreeElement();
       shell.transform.position = new Vector3(-15, 0, Random.Range(-3.0f, 0f)); ;
    }
    void CreatSlime()
    {
        IEnemy slime = poolMSlime.GetFreeElement();
        slime.transform.position = new Vector3(-15, 0, Random.Range(-3.0f, 0f)); ;
    }
}
