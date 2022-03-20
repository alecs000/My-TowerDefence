using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterPool : MonoBehaviour
{
    public List<IEnemy> poolM;
    public PoolMono<IEnemy> poolMShell;
    public PoolMono<IEnemy> poolMSlime;
    public PoolMono<IEnemy> poolMOrc;
    [SerializeField] int poolCountShell = 10;
    [SerializeField] bool autoExpandShell = true;
    [SerializeField] int poolCountSlime = 10;
    [SerializeField] bool autoExpandSlime = true;
    [SerializeField] int poolCountOrc = 10;
    [SerializeField] bool autoExpandOrc = true;
    [SerializeField] IEnemy enemyPrefabShell;
    [SerializeField] IEnemy enemyPrefabSlime;
    [SerializeField] IEnemy enemyPrefabOrc;
    [SerializeField] GameObject bossSlime;
    [SerializeField] GameObject bossOrc;
    ManegeSpawn manegeSpawn;
    public int[] waveShell = {  };
    public int[] waveSlime = {  };
    public int[] waveOrc = { };
    GameObject boss;


    private void Awake()
    {
        manegeSpawn = GetComponent<ManegeSpawn>();
        poolM = new List<IEnemy>();
        poolMShell = new PoolMono<IEnemy>(enemyPrefabShell, poolCountShell, this.transform);
        poolMShell.autoExpand = autoExpandShell;
        poolMSlime = new PoolMono<IEnemy>(enemyPrefabSlime, poolCountSlime, this.transform);
        poolMSlime.autoExpand = autoExpandSlime;
        poolMOrc = new PoolMono<IEnemy>(enemyPrefabOrc, poolCountOrc, this.transform);
        poolMOrc.autoExpand = autoExpandOrc;
    }
    private void Start()
    {
        SrartLevel(UpgrateMemory.levels.Count);
        StartCoroutine(SpawnMonster());
    }
    void SrartLevel(int level = -1)
    {
        switch (level)
        {
            case -2:
                SrartLevel(UpgrateMemory.levels.Count-1);
                break;
            case -1:
                SrartLevel(UpgrateMemory.levels.Count);
                break;
            case 0:
                waveShell = new int[] {0, 0};
                waveSlime = new int[] {0, 0};
                waveOrc = new int[] {0, 0};
                boss = bossSlime;
                break;
            case 1:
                waveShell = new int[] { 1, 2, 2, 0};
                waveSlime = new int[] { 3, 1, 2, 2};
                waveOrc = new int[] { 0, 0, 0, 0};
                boss = bossOrc;
                break;
            case 2:
                waveShell = new int[] { 3,4, 2, 0};
                waveSlime = new int[] { 2, 0, 1, 6};
                waveOrc = new int[] { 0, 1, 2, 1 };
                break;
        } 
    }
    public void SrartNextLevel()
    {
        SrartLevel(-1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ResrartLevel()
    {
        SrartLevel(-2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
                yield return new WaitForSeconds(1f);
            }
            for (int j = 0; j < waveOrc[i]; j++)
            {
                if (!manegeSpawn.isFreeze)
                {
                    this.CreatOrc();
                }
                yield return new WaitForSeconds(1.5f);
            }
            yield return new WaitForSeconds(0f);
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
    void CreatOrc()
    {
        IEnemy orc = poolMOrc.GetFreeElement();
        orc.transform.position = new Vector3(-15, 0, Random.Range(-3.0f, 0f));
    }
}
