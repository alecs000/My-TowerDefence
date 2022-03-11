using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPool : MonoBehaviour
{
    public List<IEnemy> poolM;
    public PoolMono<IEnemy> poolMShell;
    public PoolMono<IEnemy> poolMSlime;
    [SerializeField] int poolCountShell = 10;
    [SerializeField] bool autoExpandShell = true;
    [SerializeField] IEnemy enemyPrefabShell;
    [SerializeField] int poolCountSlime = 10;
    [SerializeField] bool autoExpandSlime = true;
    [SerializeField] IEnemy enemyPrefabSlime;
    [SerializeField] GameObject boss;
    Vector3 spawnPosition;
    ManegeSpawn manegeSpawn;
    public int[] waveShell = { 2, 0 };
    public int[] waveSlime = { 1, 3 };


    private void Awake()
    {
        manegeSpawn = GetComponent<ManegeSpawn>();
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
        for (int i = 0; i < waveShell.Length; i++)
        {
            for (int j = 0; j < waveShell[i]; j++)
            {
                if (!manegeSpawn.isFreeze)
                {
                    this.CreateShell();
                }
                yield return new WaitForSeconds(1f);
            }
            yield return new WaitForSeconds(1f);
            for (int j = 0; j < waveSlime[i]; j++)
            {
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
