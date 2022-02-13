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
    ManegeSpawn manegeSpawn;
    [SerializeField] GameObject boss;

    private void Awake()
    {
        manegeSpawn= GetComponent<ManegeSpawn>();
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
        for (int i = 0; i < 1; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                if (!manegeSpawn.isFreeze)
                {
                    this.CreateShell();
                }
                yield return new WaitForSeconds(1f);
            }
            yield return new WaitForSeconds(1f);
            for (int j = 0; j < 1; j++)
            {
                if (manegeSpawn.isFreeze)
                {
                    yield return new WaitForSeconds(3f);
                }
                if (!manegeSpawn.isFreeze)
                {
                    this.CreatSlime();
                }
                yield return new WaitForSeconds(1.5f);
            }
            yield return new WaitForSeconds(7f);
        }
        Instantiate(boss, new Vector3(-15, 0, Random.Range(-3.0f, 0f)), boss.transform.rotation);


    }
    void CreateShell()
    {
       IEnemy shell = poolMShell.GetFreeElement();
       shell.transform.position = new Vector3(-15, 0, Random.Range(-3.0f, 0f));
    }
    void CreatSlime()
    {
        IEnemy slime = poolMSlime.GetFreeElement();
        slime.transform.position = new Vector3(-15, 0, Random.Range(-3.0f, 0f));
    }
}
