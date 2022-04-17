using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Audio;
public class ManegeSpawn : MonoBehaviour
{
    [SerializeField] GameObject mage;
    [SerializeField] GameObject knight;
    [SerializeField] GameObject footman;
    [SerializeField] GameObject barbarian;
    [SerializeField] GameObject menuWin;
    [SerializeField] GameObject menuLose;
    [SerializeField] GameObject lockGmKnight;
    [SerializeField] GameObject lockGmArrow;
    [SerializeField] GameObject lockGmBarbarian;
    [SerializeField] GameObject lockGmFire;
    [SerializeField] GameObject GmFire;
    [SerializeField] GameObject GmKnight;
    [SerializeField] GameObject GmArrow;
    [SerializeField] GameObject GmBarbarian;
    [SerializeField] int livesTower;
    [SerializeField] AudioSource boosMusic;
    [SerializeField] AudioSource idleMusic;
    [SerializeField] AudioSource winMusic;
    [SerializeField] AudioSource loseMusic;
    [SerializeField] GameObject grey;
    [SerializeField] GameObject arrow;
    [SerializeField] float frezzeTime;
    Vector3 spawnPositionAlly;  
    public List<IAlly> AllyList = new List<IAlly>();
    public bool isFreeze;
    public Image bar;
    public Image Mainbar;
    public Text barText;
    public bool IsDrag;
    public bool openSittings = false;//запрещать покупки при настройках
    List<bool> listMage;
    List<bool> listKnight;
    List<bool> listFootman;
    List<bool> listIce;
    List<bool> listFireBall;
    List<bool> listArrow;
    List<bool> listBarbarian;
    public bool isGameStop;
    public bool win;
    public bool lose;
    public bool isBoss;
    public GameObject boss;
    bool isMusicBoss;
    bool isMenuActive;
    short bombPrice = 40;
    [Header("Bomb")]
    [SerializeField] Image prefab;
    Image gm;
    [SerializeField] Button but;
    [SerializeField] ParticleSystem boom;
    [SerializeField] ParticleSystem bigBoom;
    [SerializeField] GameObject red;
    [SerializeField] GameObject redPanel;
    ParticleSystem boomDes;
    bool isParticlActiv;
    public bool isRedZone;
    [SerializeField] GameObject Text40;
    [SerializeField] GameObject Text30;
    [SerializeField] GameObject balista;
    [Header("Info")]
    [SerializeField] GameObject infoKnight;
    [SerializeField] GameObject infoMage;
    [SerializeField] GameObject infoFootman;
    [SerializeField] GameObject infoBarbarian;
    [SerializeField] GameObject infoIce;
    [SerializeField] GameObject infoFireBall;
    [SerializeField] GameObject infoArrow;
    [SerializeField] GameObject infoGod;
    [Header("Lawn Mover")]
    [SerializeField] GameObject lawnMoverPrefab;
    [SerializeField] AudioSource error;
    [SerializeField] AudioMixerGroup Mixer;
    public static bool isPlayerPrefs;
    [SerializeField] List<GameObject> buttonSpeed = new List<GameObject>();
    int allSpeed = 0;
    private void Awake()
    {
        spawnPositionAlly = new Vector3(8, 0, -1.3f);
        if (!isPlayerPrefs)
        {
            for (int i = 0; i < PlayerPrefs.GetInt("levels"); i++)
            {
                UpgrateMemory.levels.Add(3);//звезды мб надо сделать
            }
            for (int i = 0; i < PlayerPrefs.GetInt("zero"); i++)
            {
                UpgrateMemory.upgratesKnight.Add(true);
            }
            for (int i = 0; i < PlayerPrefs.GetInt("one"); i++)
            {
                UpgrateMemory.upgratesMage.Add(true);
            }
            for (int i = 0; i < PlayerPrefs.GetInt("two"); i++)
            {
                UpgrateMemory.upgratesFootman.Add(true);
            }
            for (int i = 0; i < PlayerPrefs.GetInt("three"); i++)
            {
                UpgrateMemory.upgratesBarbarian.Add(true);
            }
            for (int i = 0; i < PlayerPrefs.GetInt("ice"); i++)
            {
                UpgrateMemory.upgratesIce.Add(true);
            }
            for (int i = 0; i < PlayerPrefs.GetInt("fire"); i++)
            {
                UpgrateMemory.upgratesFireBall.Add(true);
            }
            for (int i = 0; i < PlayerPrefs.GetInt("arrow"); i++)
            {
                UpgrateMemory.upgratesArrow.Add(true);
            }
            isPlayerPrefs = true;
        }
        listMage = UpgrateMemory.upgratesMage;
        listKnight = UpgrateMemory.upgratesKnight;
        listFootman = UpgrateMemory.upgratesFootman;
        listIce = UpgrateMemory.upgratesIce;
        listFireBall = UpgrateMemory.upgratesFireBall;
        listArrow = UpgrateMemory.upgratesArrow;
        listBarbarian = UpgrateMemory.upgratesBarbarian;
        for (int i = 0; i < listMage.Count; i++)
        {
            if (listMage[i])
            {
                if (i == 0)
                {
                    wixard_move.upSpeed = true;
                    Enemy.Mageattack *= 1.1f;
                }
                if (i == 1)
                {
                    Enemy.Mageattack *= 2;
                    wixard_move.isBlueFireBall = true;
                }
                if (i == 2)
                {
                    wixard_move.isVampire = true;
                }
            }
        }
        for (int i = 0; i < listKnight.Count; i++)
        {
            if (listKnight[i])
            {
                if (i == 0)
                {
                    KnightManagement.upSpeed = true;
                }
                if (i == 1)
                {
                    KnightManagement.upSpeedAfterKill = true;
                }
                if (i == 2)
                {
                    KnightManagement.smallCopy = true;
                }
            }
        }
        for (int i = 0; i < listFootman.Count; i++)
        {
            if (listFootman[i])
            {
                if (i == 0)
                {
                    FootManager.isHpBoost = true;
                }
                if (i == 1)
                {
                    FootManager.isBerserk = true;
                }
                if (i == 2)
                {
                    FootManager.isAppear = true;
                }
            }
        }
        for (int i = 0; i < listIce.Count; i++)
        {
            if (listIce[i])
            {
                if (i == 0)
                {
                    frezzeTime *= 2;
                }
                if (i == 1)
                {
                    Enemy.frezzeDie = true;
                }
            }
        }
        for (int i = 0; i < listFireBall.Count; i++)
        {
            if (listFireBall[i])
            {
                if (i == 0)
                {
                    bombPrice -= 10;
                    Text30.SetActive(true);
                    Text40.SetActive(false);
                }
                if (i == 1)
                {
                    boom = bigBoom;
                }
            }
        }
        for (int i = 0; i < listArrow.Count; i++)
        {
            if (listArrow[i])
            {
                if (i == 0)
                {
                    Enemy.isPoisonActive = true;
                }
                if (i == 1)
                {

                }
            }
        }
        for (int i = 0; i < listBarbarian.Count; i++)
        {
            if (listBarbarian[i])
            {
                if (i == 0)
                {
                    BarbarianManager.UpAttack5Sec = true;
                }
                if (i == 1)
                {
                    BarbarianManager.UpAttackWhenAtack = true;
                }
                if (i == 2)
                {
                    BarbarianManager.UpAttack50 = true;
                }
            }
        }

        if (UpgrateMemory.levels.Count>0)
        {
            lockGmFire.SetActive(false);
            GmFire.SetActive(true);
        }
        if (UpgrateMemory.levels.Count > 1)
        {
            lockGmKnight.SetActive(false);
            GmKnight.SetActive(true);
        }
        if (UpgrateMemory.levels.Count > 3)
        {
            lockGmArrow.SetActive(false);
            GmArrow.SetActive(true);
        }
        if (UpgrateMemory.levels.Count > 5)
        {
            lockGmBarbarian.SetActive(false);
            GmBarbarian.SetActive(true);
        }
    }
    private void Start()
    {
        Mixer.audioMixer.SetFloat("EffectsVolume", Mathf.Lerp(-80, 0, PlayerPrefs.GetFloat("Effect")));
        Mixer.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, PlayerPrefs.GetFloat("Music")));
        if (listArrow.Count>0&& !openSittings && listArrow[1])
        {
            Instantiate(balista, spawnPositionAlly, balista.transform.rotation);
        }
    }

    private void Update()
    {
        if (isBoss && boss.activeInHierarchy && !isMusicBoss)
        {
            boosMusic.Play();
            idleMusic.Stop();
            isMusicBoss = true;
        }
        if (isBoss&&!boss.activeInHierarchy && !isMenuActive)
        {
            idleMusic.Stop();
            boosMusic.Stop();
            winMusic.Play();
            menuWin.SetActive(true);
            Debug.Log(PlayerPrefs.GetInt("levels") + 1 + "wtf");
            isMenuActive = true;
            PlayerPrefs.SetInt("levels", PlayerPrefs.GetInt("levels") + 1);
        }
        if (isGameStop&& !isMenuActive)
        {
            if (win)
            {
                idleMusic.Stop();
                boosMusic.Stop();
                winMusic.Play();
                menuWin.SetActive(true);
                Debug.Log(PlayerPrefs.GetInt("levels") + 1);
                isMenuActive = true;
                PlayerPrefs.SetInt("levels", PlayerPrefs.GetInt("levels")+1);
            }
            else if (lose)
            {
                idleMusic.Stop();
                boosMusic.Stop();
                loseMusic.Play();
                Debug.Log(2);
                menuLose.SetActive(true);
                isMenuActive = true;
            }
        }
    }
    public void ButtonSpeedClick()
    {
        for (int i = 0; i < buttonSpeed.Count; i++)
        {
            if (i!= buttonSpeed.Count-1&&buttonSpeed[i].activeInHierarchy)
            {
                switch (i)
                {
                    case 0:
                        Time.timeScale = 1;
                        break;
                    case 1:
                        Time.timeScale = 1.5f;
                        break;
                    case 2:
                        Time.timeScale = 2;
                        break;
                    case 3:
                        Time.timeScale = 3;
                        break;
                }
                buttonSpeed[i].SetActive(false);
                buttonSpeed[i+1].SetActive(true);
                break;
            }
            if (i == buttonSpeed.Count - 1)
            {
                Time.timeScale = 0.5f;
                buttonSpeed[i].SetActive(false);
                buttonSpeed[0].SetActive(true);
                break;
            }
        }
    }
    public void Lose(int num  = 1)
    {
        if (livesTower>0)
        {
            livesTower -= num;
        }
    }

    public void SpawnMage(GameObject red)
    {
        if (!openSittings)
        {
            if (CoinsMangement.RemoveCoins(30))
            {
                Instantiate(mage, spawnPositionAlly, mage.transform.rotation);
            }
            else
            {
                red.SetActive(true);
                StartCoroutine(BannerRed(red));
            }
        }
    }
    public void SpawBarbarian(GameObject red)
    {
        if (!openSittings)
        {
            if (CoinsMangement.RemoveCoins(100))
            {
                Instantiate(barbarian, spawnPositionAlly, barbarian.transform.rotation);
            }
            else
            {
                red.SetActive(true);
                StartCoroutine(BannerRed(red));
            }
        }
    }
    IEnumerator BannerRed(GameObject red,float waitTime = 0.2f)
    {
        yield return new WaitForSeconds(waitTime);
        red.SetActive(false);
    }
    public void SpawnKnight(GameObject red)
    {
        if (!openSittings)
        {
            if (CoinsMangement.RemoveCoins(20))
            {
                Instantiate(knight, spawnPositionAlly, knight.transform.rotation);
            }
            else
            {
                red.SetActive(true);
                StartCoroutine(BannerRed(red));
            }
        }
    }
    public void SpawnFootman(GameObject red)
    {
        if (!openSittings)
        {
            if (CoinsMangement.RemoveCoins(60))
            {
                Instantiate(footman, spawnPositionAlly, knight.transform.rotation);
            }
            else
            {
                red.SetActive(true);
                StartCoroutine(BannerRed(red));
            }
        }
    }
    public void SpawnArrow(GameObject red)
    {
        if (!openSittings)
        {
            if (EnergyMenegment.RemoveEnergy(90))
            {
                StartCoroutine(ArrowAtack());
            }
            else
            {
                red.SetActive(true);
                StartCoroutine(BannerRed(red));
            }
        }
    }
    IEnumerator ArrowAtack()
    {
        int i = 0;
        while (i<10)
        {
            i++;
            Instantiate(arrow, arrow.transform.position, arrow.transform.rotation);
            StartCoroutine(BannerRed(grey, 10));
            yield return new WaitForSeconds(2f);
        }

    }
    public void RegistrAlly(IAlly ally)
    {
        AllyList.Add(ally);
        Debug.Log(ally);
    }
    public void RemoveAlly(IAlly ally)
    {
        AllyList.Remove(ally);
    }
    public void OpenInfo()
    {
        if (infoKnight.activeInHierarchy)
        {
            infoKnight.SetActive(false);
            infoMage.SetActive(false);
            infoFootman.SetActive(false);
            infoBarbarian.SetActive(false);
        }
        else
        { 
            infoKnight.SetActive(true);
            infoMage.SetActive(true);
            infoFootman.SetActive(true);
            infoBarbarian.SetActive(true);
        }
    }
    public void SpawnLowerMoverF()
    {
        StartCoroutine(SpawnLowerMover());
        error.Play();
    }
    IEnumerator SpawnLowerMover()
    {
        yield return new WaitForSeconds(3);
             Instantiate(lawnMoverPrefab, lawnMoverPrefab.transform.position, lawnMoverPrefab.transform.rotation);
        error.Pause();
    }
    public void OpenDiscription()
    {
        if (infoIce.activeInHierarchy)
        {
            infoIce.SetActive(false);
            infoFireBall.SetActive(false);
            infoArrow.SetActive(false);
            infoGod.SetActive(false);
        }
        else
        {
            infoIce.SetActive(true);
            infoFireBall.SetActive(true);
            infoArrow.SetActive(true);
            infoGod.SetActive(true);
        }
    }
    #region [Freeze]
    [Header("Freeze")]
    [SerializeField] GameObject frost;
    public void Freeze(GameObject red)
    {
        if (!openSittings)
        {
            if (EnergyMenegment.RemoveEnergy(17))
            {
                isFreeze = true;
                frost.SetActive(true);
                StartCoroutine(FreezeAll());
            }
            else
            {
                red.SetActive(true);
                StartCoroutine(BannerRed(red));
            }
        }
    }
    IEnumerator FreezeAll()
    {
        yield return new WaitForSeconds(frezzeTime);
        isFreeze = false;
        frost.SetActive(false);
    }
    #endregion
    #region [Bomb]
    public void Down()
    {
        if (!openSittings)
        {
            if (!isParticlActiv && EnergyMenegment.energy >= bombPrice)
            {
                gm = Object.Instantiate(prefab, but.transform);
                IsDrag = true;
            }
            else
            {
                IsDrag = false;
                red.SetActive(true);
                StartCoroutine(BannerRed(red));
            }
        }

    }
    public void Up(GameObject grey)
    {
        IsDrag = false;
        if (!openSittings)
        {
            if (!isRedZone)
            {
                if (!isParticlActiv && EnergyMenegment.energy >= bombPrice)
                {
                    if (!isParticlActiv && gm != null)
                    {
                        Vector3 mouse = Input.mousePosition;
                        Ray castPoint = Camera.main.ScreenPointToRay(mouse);
                        RaycastHit hit;

                        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
                        {
                            Debug.DrawLine(castPoint.origin, hit.point, Color.red, 200, false);
                            Debug.Log("Путь к врагу преграждает объект: " + hit.collider.name);
                            if (hit.collider.name == "ground"|| hit.collider.tag == "enemy")
                            {
                                EnergyMenegment.RemoveEnergy(bombPrice);
                                boomDes = Instantiate(boom, transform.position, boom.transform.rotation);
                                StartCoroutine(DelitParticl(grey));
                                boomDes.transform.position = new Vector3(hit.point.x, hit.point.y + 0.1f, hit.point.z);
                            }
                            else
                            {
                                redPanel.SetActive(true);
                                StartCoroutine(redDisactive());
                            }
                        }
                        Debug.Log(gm);
                        Destroy(gm);
                    }
                }
            }
        }
    }
    IEnumerator redDisactive()
    {
        yield return new WaitForSeconds(1);
        redPanel.SetActive(false);
    }
    IEnumerator DelitParticl(GameObject grey)
    {
        grey.SetActive(true);
        isParticlActiv = true;
        yield return new WaitForSeconds(1);
        grey.SetActive(false);
        isParticlActiv = false;
        Destroy(boomDes.gameObject);
    }
    public void Drag()
    {
        if (!openSittings)
        {
            IsDrag = true;
            if (!isParticlActiv && gm != null)
            {
                gm.transform.position = Input.mousePosition;
            }
        }
    }


    #endregion
    #region [Settings]
    public void Close(Image img)
    {
        openSittings = false;
        img.gameObject.SetActive(false);
    }
    public void Open(Image img)
    {
        openSittings = true;
        img.gameObject.SetActive(true);
    }
    #endregion
    #region [Error]
    #endregion
}
