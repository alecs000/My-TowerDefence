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
    [SerializeField] List<GameObject> buttonsMage = new List<GameObject>();
    [SerializeField] List<GameObject> buttonsKnight = new List<GameObject>();
    [SerializeField] List<GameObject> buttonsFootman = new List<GameObject>();
    [SerializeField] List<GameObject> lockMage = new List<GameObject>();
    [SerializeField] List<GameObject> lockKnight = new List<GameObject>();
    [SerializeField] List<GameObject> lockFootman = new List<GameObject>();
    [SerializeField] List<GameObject> purchasedMage = new List<GameObject>();
    [SerializeField] List<GameObject> purchasedKnight = new List<GameObject>();
    [SerializeField] List<GameObject> purchasedFootman = new List<GameObject>();
    [SerializeField] GameObject lockGm;
    [SerializeField] GameObject footmenGm;
    [SerializeField] AudioSource fireEffect;
    [SerializeField] AudioSource pressEffect;
    private void Awake()
    {
        if (UpgrateMemory.levels.Count>0)
        {
            if (UpgrateMemory.levels?[0] == 1 || UpgrateMemory.levels?[0] == 2 || UpgrateMemory.levels?[0] == 3)
            {
                footmenGm.SetActive(true);
                lockGm.SetActive(false);
            }
        }
        listMage = UpgrateMemory.upgratesMage;
        listKnight = UpgrateMemory.upgratesKnight;
        listFootman = UpgrateMemory.upgratesFootman;
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
    public void ClickOnUpgrate(Image gm)
    {
        pressEffect.Play();
        AktiveMageUpgtate();
        AktiveKnightUpgtate();
        AktiveFootmanUpgtate();
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
    public void ZeroSkillUpdate(int num)
    {
        if (MainManager.RemoveEmeralds(num))
        {
            fireEffect.Play();
            UpgrateMemory.upgratesKnight.Add(true);
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
    public void OneSkillUpdate(int num)
    {
        if (MainManager.RemoveEmeralds(num))
        {
            fireEffect.Play();
            UpgrateMemory.upgratesMage.Add(true);
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
    public void TwoSkillUpdate(int num)
    {
        if (MainManager.RemoveEmeralds(num))
        {
            fireEffect.Play();
            UpgrateMemory.upgratesFootman.Add(true);
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
}
