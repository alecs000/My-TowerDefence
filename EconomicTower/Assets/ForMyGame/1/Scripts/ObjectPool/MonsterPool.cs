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
    public PoolMono<IEnemy> poolMSpider;
    public PoolMono<IEnemy> poolMBug;
    public PoolMono<IEnemy> poolMStoneMonster;
    public PoolMono<IEnemy> poolMEyes;
    [SerializeField] int poolCountShell = 10;
    [SerializeField] bool autoExpandShell = true;
    [SerializeField] int poolCountSlime = 10;
    [SerializeField] bool autoExpandSlime = true;
    [SerializeField] int poolCountOrc = 10;
    [SerializeField] bool autoExpandOrc = true;
    [SerializeField] int poolCountSpider = 10;
    [SerializeField] bool autoExpandSpider = true;
    [SerializeField] int poolCountBug = 10;
    [SerializeField] bool autoExpandBug = true;
    [SerializeField] int poolCountStoneMonster = 10;
    [SerializeField] bool autoExpandStoneMonster = true;
    [SerializeField] int poolCountEyes = 10;
    [SerializeField] bool autoExpandEyes = true;
    [SerializeField] IEnemy enemyPrefabShell;
    [SerializeField] IEnemy enemyPrefabSlime;
    [SerializeField] IEnemy enemyPrefabOrc;
    [SerializeField] IEnemy enemyPrefabSpider;
    [SerializeField] IEnemy enemyPrefabBug;
    [SerializeField] IEnemy enemyPrefabStoneMonster;
    [SerializeField] IEnemy enemyPrefabEyes;
    [SerializeField] GameObject bossSlime;
    [SerializeField] GameObject bossOrc;
    [SerializeField] GameObject bossSpider;
    [SerializeField] GameObject bossBug;
    [SerializeField] GameObject bossStoneMonster;
    [SerializeField] GameObject bossEyes;
    ManegeSpawn manegeSpawn;
    public int[] waveShell = { 0 };
    public int[] waveSlime = { 0 };
    public int[] waveOrc = {0 };
    public int[] waveSpider = {0};
    public int[] waveBug = { 0};
    public int[] waveStoneMonster = {0 };
    public int[] waveEyes = { 0};
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
        poolMSpider = new PoolMono<IEnemy>(enemyPrefabSpider, poolCountSpider, this.transform);
        poolMSpider.autoExpand = autoExpandSpider;
        poolMBug = new PoolMono<IEnemy>(enemyPrefabBug, poolCountBug, this.transform);
        poolMBug.autoExpand = autoExpandBug;
        poolMStoneMonster = new PoolMono<IEnemy>(enemyPrefabStoneMonster, poolCountStoneMonster, this.transform);
        poolMStoneMonster.autoExpand = autoExpandStoneMonster;
        poolMEyes = new PoolMono<IEnemy>(enemyPrefabEyes, poolCountEyes, this.transform);
        poolMEyes.autoExpand = autoExpandEyes;
    }
    private void Start()
    {
        int level= PlayerPrefs.GetInt("levels");
            SrartLevel(level);
            StartCoroutine(SpawnMonster());

        if (!ManegeSpawn.isPlayerPrefs)
        {
            for (int i = 0; i < level - 1; i++)
            {
                UpgrateMemory.levels.Add(3);//звезды мб надо сделать
            }
            ManegeSpawn.isPlayerPrefs = true;
        }
        Debug.Log(level + "PP GG");
    }
    public void Reset()
    {
        PlayerPrefs.DeleteAll();
    }
    void SrartLevel(int level = -1)
    {
        Debug.Log(UpgrateMemory.levels.Count);
        Debug.Log(level);
        if (PlayerPrefs.GetInt("levels", level)< level)
        {
            PlayerPrefs.SetInt("levels", level);
        }
        switch (level)
        {
            case -2:
                SrartLevel(UpgrateMemory.levels.Count-1);
                break;
            case -1:
                SrartLevel(UpgrateMemory.levels.Count);
                break;
            case 0:
                //waveShell = new int[] {0, 2};
                //waveSlime = new int[] {2, 1};
                //waveOrc = new int[] { 0, 0};
                //waveSpider = new int[] { 0, 0 };
                //waveBug = new int[] { 0, 0 };
                //waveStoneMonster = new int[] { 0, 0 };
                //waveEyes = new int[] { 0, 0 };
                //boss = bossSlime;
                break;
            case 1:
                waveShell = new int[] { 1, 2, 2, 0};
                waveSlime = new int[] { 3, 1, 2, 2};
                waveOrc = new int[] { 0, 0,0,0 };
                waveSpider = new int[] { 0, 0, 0, 0 };
                waveBug = new int[] { 0, 0, 0, 0 };
                waveStoneMonster = new int[] { 0, 0, 0, 0 };
                waveEyes = new int[] { 0, 0, 0, 0 };
                boss = bossOrc;

                break;
            case 2:
                waveShell = new int[] { 3,4, 2, 0};
                waveSlime = new int[] { 2, 0, 1, 6};
                waveOrc = new int[] { 0, 1, 2, 1 };
                waveSpider = new int[] { 0, 0, 0, 0 };
                waveBug = new int[] { 0, 0, 0, 0 };
                waveStoneMonster = new int[] { 0, 0, 0, 0 };
                waveEyes = new int[] { 0, 0, 0, 0 };
                boss = bossSpider;
                break;
            case 3:
                waveShell = new int[] { 1, 1, 5, 1 };
                waveSlime = new int[] { 2, 3, 5, 1 };
                waveOrc = new int[] { 1, 2, 3, 1 };
                waveSpider = new int[] { 1, 2, 2, 4 };
                waveBug = new int[] { 0, 0, 0, 0 };
                waveStoneMonster = new int[] { 0, 0, 0, 0 };
                waveEyes = new int[] { 0, 0, 0, 0 };
                boss = bossBug;
                break;
            case 4:
                waveShell = new int[] { 1, 1, 5, 1 };
                waveSlime = new int[] { 2, 3, 5, 1 };
                waveOrc = new int[] { 1, 2, 3, 1 };
                waveSpider = new int[] { 1, 2, 2, 4 };
                waveBug = new int[] { 0, 3, 1, 0 };
                waveStoneMonster = new int[] { 0, 0, 0, 0 };
                waveEyes = new int[] { 0, 0, 0, 0 };
                boss = bossStoneMonster;
                break;
            case 5:
                waveShell = new int[] { 0, 6, 0, 0 };
                waveSlime = new int[] { 2, 0, 0, 0 };
                waveOrc = new int[] { 1, 3, 3, 0 };
                waveSpider = new int[] { 1, 3, 0, 1 };
                waveBug = new int[] { 0, 2, 1, 0 };
                waveStoneMonster = new int[] { 2, 0, 0, 3 };
                waveEyes = new int[] { 0, 0, 0, 0 };
                boss = bossEyes;
                break;
            case 6:
                waveShell = new int[] { 0, 6, 0, 0 };
                waveSlime = new int[] { 2, 0, 0, 0 };
                waveOrc = new int[] { 1, 3, 3, 0 };
                waveSpider = new int[] { 1, 3, 0, 1 };
                waveBug = new int[] { 0, 2, 1, 0 };
                waveStoneMonster = new int[] { 2, 0, 0, 3 };
                waveEyes = new int[] { 2, 0, 0, 3 };
                boss = bossEyes;
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
            SrartLevel(PlayerPrefs.GetInt("levels") - 1);
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
            for (int j = 0; j < waveSpider[i]; j++)
            {
                if (!manegeSpawn.isFreeze)
                {
                    this.CreatSpider();
                }
                yield return new WaitForSeconds(1.5f);
            }
            for (int j = 0; j < waveBug[i]; j++)
            {
                if (!manegeSpawn.isFreeze)
                {
                    this.CreatBug();
                }
                yield return new WaitForSeconds(1.5f);
            }
            for (int j = 0; j < waveStoneMonster[i]; j++)
            {
                if (!manegeSpawn.isFreeze)
                {
                    this.CreatStoneMonster();
                }
                yield return new WaitForSeconds(1.5f);
            }
            for (int j = 0; j < waveEyes[i]; j++)
            {
                if (!manegeSpawn.isFreeze)
                {
                    this.CreatEyes();
                }
                yield return new WaitForSeconds(1.5f);
            }
            yield return new WaitForSeconds(3f);
        }
        Instantiate(boss, new Vector3(-20, 0, Random.Range(-3.0f, 0f)), boss.transform.rotation);
    }
    void CreateShell()
    {
       IEnemy shell = poolMShell.GetFreeElement();
       shell.transform.position = new Vector3(-20, 0, Random.Range(-3.0f, 0f));
    }
    void CreatSlime()
    {
        IEnemy slime = poolMSlime.GetFreeElement();
        slime.transform.position = new Vector3(-20, 0, Random.Range(-3.0f, 0f));
    }
    void CreatOrc()
    {
        IEnemy orc = poolMOrc.GetFreeElement();
        orc.transform.position = new Vector3(-20, 0, Random.Range(-3.0f, 0f));
    }
    void CreatSpider()
    {
        IEnemy spider = poolMSpider.GetFreeElement();
        spider.transform.position = new Vector3(-20, 0, Random.Range(-3.0f, 0f));
    }
    void CreatBug()
    {
        IEnemy bug = poolMBug.GetFreeElement();
        bug.transform.position = new Vector3(-20, 0, Random.Range(-3.0f, 0f));
    }
    void CreatStoneMonster()
    {
        IEnemy stoneMonster = poolMStoneMonster.GetFreeElement();
        stoneMonster.transform.position = new Vector3(-20, 0, Random.Range(-3.0f, 0f));
    }
    void CreatEyes()
    {
        IEnemy eyes = poolMEyes.GetFreeElement();
        eyes.transform.position = new Vector3(-20, 0, Random.Range(-3.0f, 0f));
    }
}
