using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoosHPEnter : MonoBehaviour
{
    Image bar;
    Image Mainbar;
    LivesManagement livesEnemy;
    float HP = 300;
    void Start()
    {
        var manegerSpawn = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ManegeSpawn>();
        bar = manegerSpawn.bar;
        Mainbar = manegerSpawn.Mainbar;
        livesEnemy = GetComponent<Enemy>().livesEnemy;
        Mainbar.gameObject.SetActive(true);
        HP = livesEnemy.lives;
    }
    private void Update()
    {
        bar.fillAmount = (float)livesEnemy.lives / HP;
    }
    private void OnDisable()
    {
        if (bar!=null)
        {
            bar.fillAmount = 0;
        }
    }
}
