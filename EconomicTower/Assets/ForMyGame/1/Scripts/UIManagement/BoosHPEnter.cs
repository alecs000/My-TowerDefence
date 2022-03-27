using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BoosHPEnter : MonoBehaviour
{
    Image bar;
    Image Mainbar;
    LivesManagement livesEnemy;
    Text barText;
    float HP = 300;
    void Start()
    {
        var manegerSpawn = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ManegeSpawn>();
        bar = manegerSpawn.bar;
        Mainbar = manegerSpawn.Mainbar;
        barText = manegerSpawn.barText;
        livesEnemy = GetComponent<Enemy>().livesEnemy;
        Mainbar.gameObject.SetActive(true);
        HP = livesEnemy.lives;
    }
    private void OnEnable()
    {
        
    }
    private void Update()
    {
        bar.fillAmount = (float)livesEnemy.lives / HP;
        barText.text = Convert.ToString((int)(livesEnemy.lives / HP * 100))+" %";
    }
    private void OnDisable()
    {
        if (bar!=null)
        {
            bar.fillAmount = 0;
        }
    }
}
