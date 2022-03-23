using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ManegeSpawn : MonoBehaviour
{
    [SerializeField] GameObject mage;
    [SerializeField] GameObject knight;
    [SerializeField] GameObject footman;
    [SerializeField] GameObject menuWin;
    [SerializeField] GameObject menuLose;
    [SerializeField] GameObject lockGm;
    [SerializeField] int livesTower;
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
    public bool isGameStop;
    public bool win;
    public bool lose;
    public bool isBoss;
    public GameObject boss;
    private void Awake()
    {
        spawnPositionAlly = new Vector3(8, 0, -1.3f);
        listMage = UpgrateMemory.upgratesMage;
        listKnight = UpgrateMemory.upgratesKnight;
        listFootman = UpgrateMemory.upgratesFootman;
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
        if (UpgrateMemory.levels.Count>0)
        {
            lockGm.SetActive(false);
        }
    }
    private void Update()
    {
        if (isBoss&&!boss.activeInHierarchy)
        {
            menuWin.SetActive(true);
        }
        if (isGameStop)
        {
            if (win)
            {
                menuWin.SetActive(true);
            }
            else if (lose)
            {
                menuLose.SetActive(true);
            }
        }
    }
    public void Lose(int num  = 1)
    {
        if (livesTower>0)
        {
            livesTower -= num;
        }
        else
        {

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
    IEnumerator BannerRed(GameObject red)
    {
        yield return new WaitForSeconds(0.2f);
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
    public void RegistrAlly(IAlly ally)
    {
        AllyList.Add(ally);
        Debug.Log(ally);
    }
    public void RemoveAlly(IAlly ally)
    {
        AllyList.Remove(ally);
    }
    #region [Freeze]
    public void Freeze(GameObject red)
    {
        if (!openSittings)
        {
            if (CoinsMangement.RemoveCoins(12))
            {
                isFreeze = true;
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
        yield return new WaitForSeconds(3);
        isFreeze = false;
    }
    #endregion
    #region [Bomb]
    [Header("Bomb")]
    [SerializeField] Image prefab;
    Image gm;
    [SerializeField] Button but;
    [SerializeField] ParticleSystem boom;
    [SerializeField] GameObject red;
    [SerializeField] GameObject redPanel;
    ParticleSystem boomDes;
    bool isParticlActiv;
    public bool isRedZone;
    //ПОКНОПКИ НАСТРОЙКИ
    public void NotDrag()
    {
        IsDrag = true;
    }
    public void Down()
    {
        if (!openSittings)
        {
            IsDrag = true;
            if (!isParticlActiv && CoinsMangement.coins >= 40)
            {
                gm = Object.Instantiate(prefab, but.transform);
            }
            else
            {
                red.SetActive(true);
                StartCoroutine(BannerRed(red));
            }
        }

    }
    public void Up(GameObject grey)
    {
        if (!openSittings)
        {
            if (!isRedZone)
            {
                if (!isParticlActiv && CoinsMangement.coins >= 40)
                {
                    IsDrag = false;
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
                                CoinsMangement.RemoveCoins(40);
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
}
