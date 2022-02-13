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
    [SerializeField] private Vector2 butPosDownTower = new Vector2(-354,-223);
    [SerializeField] private Vector2 butPosUpTower = new Vector2(-354,-95.9f);
    [SerializeField] private Vector2 butPosDownbutSkils = new Vector2(-258, -223);
    [SerializeField] private Vector2 butPosUpbutSkils = new Vector2(-258, -95.9f);
    RectTransform rTransTower;
    RectTransform rTransSkils;
    private void Start()
    {
        rTransTower = butTower.GetComponent<RectTransform>();
        rTransSkils = butSkils.GetComponent<RectTransform>();
    }
    public void ActivDezactivTowerMenue()
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
    public void ActivDezactivSpelsMenue()
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
