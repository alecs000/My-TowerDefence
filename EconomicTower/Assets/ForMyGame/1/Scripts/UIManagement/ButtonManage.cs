using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManage : MonoBehaviour
{
    [SerializeField] Image towerPanel;
    [SerializeField] Image skillsPanel;
    bool isActivTower;
    bool isActivSkills;
    [SerializeField] Button butTower;
    [SerializeField] Button butSkils;
    [SerializeField] private Vector2 butPosDownTower;
    [SerializeField] private Vector2 butPosUpTower;
    [SerializeField] private Vector2 butPosDownbutSkils;
    [SerializeField] private Vector2 butPosUpbutSkils;
    RectTransform rTransTower;
    RectTransform rTransSkils;
    [SerializeField] ManegeSpawn manegeSpawn;
    private void Start()
    {
        rTransTower = butTower.GetComponent<RectTransform>();
        rTransSkils = butSkils.GetComponent<RectTransform>();
        butPosDownTower = rTransTower.anchoredPosition;
        butPosDownbutSkils = rTransSkils.anchoredPosition;
        butPosUpTower = new Vector2(rTransTower.rect.x+ rTransTower.rect.width/2 , rTransTower.rect.y + towerPanel.rectTransform.rect.height+ rTransTower.rect.height/2);
        butPosUpbutSkils = new Vector2(rTransSkils.rect.x + rTransTower.rect.width / 2, rTransSkils.rect.y + skillsPanel.rectTransform.rect.height + rTransSkils.rect.height / 2);
    }
    public void ActivDezactivTowerMenue()
    {
        if (!manegeSpawn.openSittings)
        {
            if (isActivTower)
            {
                towerPanel.gameObject.SetActive(false);
                DoDownPosition();
            }
            else
            {
                towerPanel.gameObject.SetActive(true);
                DoUpPosition();
            }
            isActivTower = !isActivTower;
            isActivSkills = false;
            skillsPanel.gameObject.SetActive(false);
        }
    }
    public void ActivDezactivSpelsMenue()
    {
        if (!manegeSpawn.openSittings)
        {
            if (isActivSkills)
            {
                skillsPanel.gameObject.SetActive(false);
                DoDownPosition();
            }
            else
            {
                skillsPanel.gameObject.SetActive(true);
                DoUpPosition();
            }
            isActivSkills = !isActivSkills;
            isActivTower = false;
            towerPanel.gameObject.SetActive(false);
        }
    }
    void DoUpPosition()
    {
        rTransSkils.anchoredPosition = butPosUpbutSkils;
        rTransTower.anchoredPosition = butPosUpTower;
    }
    void DoDownPosition()
    {
        rTransTower.anchoredPosition = butPosDownTower;
        rTransSkils.anchoredPosition = butPosDownbutSkils;
    }
}
