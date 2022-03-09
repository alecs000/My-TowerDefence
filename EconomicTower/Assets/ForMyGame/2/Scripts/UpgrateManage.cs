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
    [SerializeField] List<GameObject> buttonsMage = new List<GameObject>();
    [SerializeField] List<GameObject> buttonsKnight = new List<GameObject>();
    [SerializeField] List<GameObject> lockMage = new List<GameObject>();
    [SerializeField] List<GameObject> lockKnight = new List<GameObject>();
    [SerializeField] List<GameObject> purchasedMage = new List<GameObject>();
    [SerializeField] List<GameObject> purchasedKnight = new List<GameObject>();
    private void Awake()
    {
        listMage = UpgrateMemory.upgratesMage;
        listKnight = UpgrateMemory.upgratesKnight;
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
    public void ClickOnUpgrate(Image gm)
    {
        AktiveMageUpgtate();
        AktiveKnightUpgtate();
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
}
