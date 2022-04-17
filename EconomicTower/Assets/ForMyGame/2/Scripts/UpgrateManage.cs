using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgrateManage : MonoBehaviour
{
   Image image;
   bool isActive;
    List<bool> listMage;
    List<bool> listKnight;
    List<bool> listFootman;
    List<bool> listIce;
    List<bool> listFireBall;
    List<bool> listArrow;
    List<bool> listBarbarian;
    [SerializeField] List<GameObject> buttonsMage = new List<GameObject>();
    [SerializeField] List<GameObject> buttonsKnight = new List<GameObject>();
    [SerializeField] List<GameObject> buttonsFootman = new List<GameObject>();
    [SerializeField] List<GameObject> buttonsBarbarian = new List<GameObject>();
    [SerializeField] List<GameObject> buttonsIce = new List<GameObject>();
    [SerializeField] List<GameObject> buttonsFireBall = new List<GameObject>();
    [SerializeField] List<GameObject> buttonsArrow = new List<GameObject>();
    [SerializeField] List<GameObject> lockMage = new List<GameObject>();
    [SerializeField] List<GameObject> lockKnight = new List<GameObject>();
    [SerializeField] List<GameObject> lockFootman = new List<GameObject>();
    [SerializeField] List<GameObject> lockBarbarian = new List<GameObject>();
    [SerializeField] List<GameObject> lockIce = new List<GameObject>();
    [SerializeField] List<GameObject> lockFireBall = new List<GameObject>();
    [SerializeField] List<GameObject> lockArrow = new List<GameObject>();
    [SerializeField] List<GameObject> purchasedMage = new List<GameObject>();
    [SerializeField] List<GameObject> purchasedKnight = new List<GameObject>();
    [SerializeField] List<GameObject> purchasedFootman = new List<GameObject>();
    [SerializeField] List<GameObject> purchasedBarbarian = new List<GameObject>();
    [SerializeField] List<GameObject> purchasedIce = new List<GameObject>();
    [SerializeField] List<GameObject> purchasedFireBall = new List<GameObject>();
    [SerializeField] List<GameObject> purchasedArrow = new List<GameObject>();
    [SerializeField] GameObject lockGmFire;
    [SerializeField] GameObject fireGm;
    [SerializeField] GameObject lockGmFootman;
    [SerializeField] GameObject footmenGm;
    [SerializeField] GameObject lockGmArrow;
    [SerializeField] GameObject ArrowGm;
    [SerializeField] GameObject lockGmBarbarian;
    [SerializeField] GameObject BarbarianGm;
    [SerializeField] AudioSource pressEffect;
    [SerializeField] AudioSource dogEffect;
    [SerializeField] AudioSource mageEffect;
    [SerializeField] AudioSource knightEffect;
    [SerializeField] AudioSource barbarianEffect;
    [SerializeField] AudioSource freezeEffect;
    [SerializeField] AudioSource fireEffect;
    [SerializeField] AudioSource arrowEffect;
    [SerializeField] GameObject spells;
    [SerializeField] GameObject towers;
    bool notActiveInHierarchySpells;
    bool notActiveInHierarchyTowers;
    private void Awake()
    {
        if (UpgrateMemory.levels.Count > 0)
        {
            if (UpgrateMemory.levels[0] == 1 || UpgrateMemory.levels?[0] == 2 || UpgrateMemory.levels?[0] == 3)
            {
                fireGm.SetActive(true);
                lockGmFire.SetActive(false);
            }
        }
        if (UpgrateMemory.levels.Count>1)
        {
            if (UpgrateMemory.levels[1] == 1 || UpgrateMemory.levels?[1] == 2 || UpgrateMemory.levels?[1] == 3)
            {
                footmenGm.SetActive(true);
                lockGmFootman.SetActive(false);
            }
        }
        if (UpgrateMemory.levels.Count > 3)
        {
            if (UpgrateMemory.levels[3] == 1 || UpgrateMemory.levels?[3] == 2 || UpgrateMemory.levels?[3] == 3)
            {
                ArrowGm.SetActive(true);
                lockGmArrow.SetActive(false);
            }
        }
        if (UpgrateMemory.levels.Count >3)
        {
            if (UpgrateMemory.levels[5] == 1 || UpgrateMemory.levels[5] == 2 || UpgrateMemory.levels[5] == 3)
            {
                BarbarianGm.SetActive(true);
                lockGmBarbarian.SetActive(false);
                Debug.Log(23435453);
            }
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
                buttonsMage[i].SetActive(false);
            }
        }
        for (int i = 0; i < listKnight.Count; i++)
        {
            if (listKnight[i])
            {
                buttonsKnight[i].SetActive(false);
            }
        }
        for (int i = 0; i < listFootman.Count; i++)
        {
            if (listFootman[i])
            {
                buttonsFootman[i].SetActive(false);
            }
        }
        for (int i = 0; i < listBarbarian.Count; i++)
        {
            if (listBarbarian[i])
            {
                buttonsBarbarian[i].SetActive(false);
            }
        }
    }
    private void Update()
    {
        if (!towers.activeInHierarchy)
        {
            notActiveInHierarchyTowers = true;
        }
        if (!spells.activeInHierarchy)
        {
            notActiveInHierarchySpells = true;
        }
            if (notActiveInHierarchySpells && spells.activeInHierarchy)
            {
                notActiveInHierarchySpells = false;
                image?.gameObject.SetActive(false);
            }
            if (notActiveInHierarchyTowers && towers.activeInHierarchy)
            {
                notActiveInHierarchyTowers = false;
                image?.gameObject.SetActive(false);
            }
    }
    void AktiveKnightUpgtate()
    {
        for (int i = 0; i < buttonsKnight.Count; i++)
        {
            if (i > listKnight.Count)
            {
                buttonsKnight[i].SetActive(false);
                lockKnight[i].SetActive(true);
            }
            else if (i == listKnight.Count)
            {
                buttonsKnight[i].SetActive(true);
                lockKnight[i].SetActive(false);
            }
            else
            {
                buttonsKnight[i].SetActive(false);
                purchasedKnight[i].SetActive(true);
            }
        }
    }
    void AktiveMageUpgtate()
    {
        for (int i = 0; i < buttonsMage.Count; i++)
        {
            if (i > listMage.Count)
            {
                buttonsMage[i].SetActive(false);
                lockMage[i].SetActive(true);
            }
            else if (i == listMage.Count)
            {
                buttonsMage[i].SetActive(true);
                lockMage[i].SetActive(false);
            }
            else
            {
                buttonsMage[i].SetActive(false);
                purchasedMage[i].SetActive(true);
            }
        }
    }
    void AktiveFootmanUpgtate()
    {
        for (int i = 0; i < buttonsFootman.Count; i++)
        {
            if (i > listFootman.Count)
            {
                buttonsFootman[i].SetActive(false);
                lockFootman[i].SetActive(true);
            }
            else if (i == listFootman.Count)
            {
                buttonsFootman[i].SetActive(true);
                lockFootman[i].SetActive(false);
            }
            else
            {
                buttonsFootman[i].SetActive(false);
                purchasedFootman[i].SetActive(true);
            }
        }
    }
    void AktiveIceUpgtate()
    {
        for (int i = 0; i < buttonsIce.Count; i++)
        {
            if (i > listIce.Count)
            {
                buttonsIce[i].SetActive(false);
                lockIce[i].SetActive(true);
            }
            else if (i == listIce.Count)
            {
                buttonsIce[i].SetActive(true);
                lockIce[i].SetActive(false);
            }
            else
            {
                buttonsIce[i].SetActive(false);
                purchasedIce[i].SetActive(true);
            }
        }
    }
    void AktiveFireBallUpgtate()
    {
        for (int i = 0; i < buttonsFireBall.Count; i++)
        {
            if (i > listFireBall.Count)
            {
                buttonsFireBall[i].SetActive(false);
                lockFireBall[i].SetActive(true);
            }
            else if (i == listFireBall.Count)
            {
                buttonsFireBall[i].SetActive(true);
                lockFireBall[i].SetActive(false);
            }
            else
            {
                buttonsFireBall[i].SetActive(false);
                purchasedFireBall[i].SetActive(true);
            }
        }
    }
    void AktiveArrowUpgtate()
    {
        for (int i = 0; i < buttonsArrow.Count; i++)
        {
            if (i > listArrow.Count)
            {
                buttonsArrow[i].SetActive(false);
                lockArrow[i].SetActive(true);
            }
            else if (i == listArrow.Count)
            {
                buttonsArrow[i].SetActive(true);
                lockArrow[i].SetActive(false);
            }
            else
            {
                buttonsArrow[i].SetActive(false);
                purchasedArrow[i].SetActive(true);
            }
        }
    }
    void AktiveBarbarianUpgtate()
    {
        for (int i = 0; i < buttonsBarbarian.Count; i++)
        {
            if (i > listBarbarian.Count)
            {
                buttonsBarbarian[i].SetActive(false);
                lockBarbarian[i].SetActive(true);
            }
            else if (i == listBarbarian.Count)
            {
                buttonsBarbarian[i].SetActive(true);
                lockBarbarian[i].SetActive(false);
            }
            else
            {
                buttonsBarbarian[i].SetActive(false);
                purchasedBarbarian[i].SetActive(true);
            }
        }
    }
    public void ClickOnUpgrate(Image gm)
    {
        pressEffect.Play();
        AktiveBarbarianUpgtate();
        AktiveMageUpgtate();
        AktiveKnightUpgtate();
        AktiveFootmanUpgtate();
        AktiveIceUpgtate();
        AktiveFireBallUpgtate();
        AktiveArrowUpgtate();
        Debug.Log(UpgrateMemory.upgratesIce);
        if (image != null&&image != gm)
        {
            image.gameObject.SetActive(false);
            image = null;
            isActive = false;
            image = gm;
            gm.gameObject.SetActive(true);
            isActive = true;
        }
        else if (image!=null)
        {
            image.gameObject.SetActive(false);
            image = null;
            isActive = false;
        }
        else if (!isActive)
        {
            image = gm;
            gm.gameObject.SetActive(true);
            isActive = true;
        }
    }
    public void CloseDiscription()
    {
        if (image!=null)
        {
            image.gameObject.SetActive(false);
        }
    }
    int zero = 1;
    public void ZeroSkillUpdate(int num)
    {
        if (MainManager.RemoveEmeralds(num))
        {
            dogEffect.Play();
            UpgrateMemory.upgratesKnight.Add(true);
            PlayerPrefs.SetInt("zero", zero);
            zero++;
        }
        for (int i = 0; i < listKnight.Count; i++)
        {
            if (listKnight[i])
            {
                buttonsKnight[i].SetActive(false);
                purchasedKnight[i].SetActive(true);
            }
        }
    }
    int one = 1;
    public void OneSkillUpdate(int num)
    {
        if (MainManager.RemoveEmeralds(num))
        {
            mageEffect.Play();
            UpgrateMemory.upgratesMage.Add(true);
            PlayerPrefs.SetInt("one", one);
            one++;
        }
        for (int i = 0; i < listMage.Count; i++)
        {
            if (listMage[i])
            {
                buttonsMage[i].SetActive(false);
                purchasedMage[i].SetActive(true);
            }
        }
    }
    int two = 1;
    public void TwoSkillUpdate(int num)
    {
        if (MainManager.RemoveEmeralds(num))
        {
            knightEffect.Play();
            UpgrateMemory.upgratesFootman.Add(true);
            PlayerPrefs.SetInt("two", two);
            two++;
        }
        for (int i = 0; i < listFootman.Count; i++)
        {
            if (listFootman[i])
            {
                buttonsFootman[i].SetActive(false);
                purchasedFootman[i].SetActive(true);
            }
        }
    }
    int three = 1;
    public void ThreeSkillUpdate(int num)
    {
        if (MainManager.RemoveEmeralds(num))
        {
            barbarianEffect.Play();//��� ���������� ���� ����� ������ �����
            UpgrateMemory.upgratesBarbarian.Add(true);
            PlayerPrefs.SetInt("three", three);
            three++;
        }
        for (int i = 0; i < listBarbarian.Count; i++)
        {
            if (listBarbarian[i])
            {
                buttonsBarbarian[i].SetActive(false);
                purchasedBarbarian[i].SetActive(true);
            }
        }
    }
    int ice = 1;
    public void IceSkillUpdate(int num)
    {
        if (MainManager.RemoveEmeralds(num))
        {
            freezeEffect.Play();//��� ���������� ���� ����� ������ �����
            UpgrateMemory.upgratesIce.Add(true);
            PlayerPrefs.SetInt("ice", ice);
            ice++;
            Debug.Log(true);
        }
        for (int i = 0; i < listIce.Count; i++)
        {
            if (listIce[i])
            {
                buttonsIce[i].SetActive(false);
                purchasedIce[i].SetActive(true);
            }
        }
    }
    int fire = 1;
    public void FireBallSkillUpdate(int num)
    {
        if (MainManager.RemoveEmeralds(num))
        {
            fireEffect.Play();//��� ���������� ���� ����� ������ �����
            UpgrateMemory.upgratesFireBall.Add(true);
            PlayerPrefs.SetInt("fire", fire);
            fire++;
        }
        for (int i = 0; i < listFireBall.Count; i++)
        {
            if (listFireBall[i])
            {
                buttonsFireBall[i].SetActive(false);
                purchasedFireBall[i].SetActive(true);
            }
        }
    }
    int arrow = 1;
    public void ArrowSkillUpdate(int num)
    {
        if (MainManager.RemoveEmeralds(num))
        {
            arrowEffect.Play();//��� ���������� ���� ����� ������ �����
            UpgrateMemory.upgratesArrow.Add(true);
            PlayerPrefs.SetInt("arrow", arrow);
            arrow++;
        }
        for (int i = 0; i < listArrow.Count; i++)
        {
            if (listArrow[i])
            {
                buttonsArrow[i].SetActive(false);
                purchasedArrow[i].SetActive(true);
            }
        }
    }
}
