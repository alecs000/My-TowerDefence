using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManage : MonoBehaviour
{
    public Image towerPanel;
    bool isActiv = true;
    public Button but;
    [SerializeField] private Vector2 butPosDown = new Vector2(-354,-223);
    [SerializeField] private Vector2 butPosUp = new Vector2(-354,-95.9f);
    private RectTransform rTrans;
    private void Start()
    {
        rTrans = but.GetComponent<RectTransform>();
    }
    public void ActivDezactivTowerMenue()
    {
            if (isActiv)
        {
            towerPanel.gameObject.SetActive(false);
            rTrans.anchoredPosition = butPosDown;
        }
        else
        {
            towerPanel.gameObject.SetActive(true);
            rTrans.anchoredPosition = butPosUp;
        }
        isActiv = !isActiv;
    }
}
