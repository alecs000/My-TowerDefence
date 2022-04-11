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
    [SerializeField] int poolCountShell = 10;
    [SerializeField] bool autoExpandShell = true;
    [SerializeField] int poolCountSlime = 10;
    [SerializeField] bool autoExpandSlime = true;
    [SerializeField] int poolCountOrc = 10;
    [SerializeField] bool autoExpandOrc = true;
    [SerializeField] int poolCountSpider = 10;
    [SerializeField] bool autoExpandSpider = true;
    [SerializeField] IEnemy enemyPrefabShell;
    [SerializeField] IEnemy enemyPrefabSlime;
    [SerializeField] IEnemy enemyPrefabOrc;
    [SerializeField] IEnemy enemyPrefabSpider;
    [SerializeField] GameObject bossSlime;
    [SerializeField] GameObject bossOrc;
    [SerializeField] GameObject bossSpider;
    ManegeSpawn manegeSpawn;
    public int[] waveShell = {  };
    public int[] waveSlime = {  };
    public int[] waveOrc = { };
    public int[] waveSpider = {};
    GameObject boss;
    static bool isWinAndRespawn;

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
        PlayerPrefs.SetInt("levels", level);
        switch (level)
        {
            case -2:
                SrartLevel(UpgrateMemory.levels.Count-1);
                break;
            case -1:
                SrartLevel(UpgrateMemory.levels.Count);
                break;
            case 0:
                waveShell = new int[] {0, 2};
                waveSlime = new int[] {2, 1};
                waveOrc = new int[] {0, 0};
                waveSpider = new int[] { 0, 0 };
                boss = bossSlime;
                break;
            case 1:
                waveShell = new int[] { 1, 2, 2, 0};
                waveSlime = new int[] { 3, 1, 2, 2};
                waveOrc = new int[] { 0, 0, 0, 0};
                waveSpider = new int[] { 0, 0,0,0 };
                boss = bossOrc;
                break;
            case 2:
                waveShell = new int[] { 3,4, 2, 0};
                waveSlime = new int[] { 2, 0, 1, 6};
                waveOrc = new int[] { 0, 1, 2, 1 };
                waveSpider = new int[] { 0, 0, 0, 0 };
                boss = bossSpider;
                break;
        } 
    }
    public void SrartNextLevel()
    {
        SrartLevel(-1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ResrartLevel(bool win)
    {
        if (win)
        {
            isWinAndRespawn = true;
        }
        else
        {
            isWinAndRespawn = false;
        }
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
            yield return new WaitForSeconds(3f);
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
    void CreatSpider()
    {
        IEnemy spider = poolMSpider.GetFreeElement();
        spider.transform.position = new Vector3(-15, 0, Random.Range(-3.0f, 0f));
    }
}
