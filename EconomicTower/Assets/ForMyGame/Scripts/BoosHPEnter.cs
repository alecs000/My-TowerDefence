using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoosHPEnter : MonoBehaviour
{
    Image bar;
    Image Mainbar;
    LivesManagement livesEnemy;
    [SerializeField] float HP;
    void Start()
    {
        var manegerSpawn = GameObject.FindGameObjectWithTag("GameManager").GetComponent<ManegeSpawn>();
        bar = manegerSpawn.bar;
        Mainbar = manegerSpawn.Mainbar;
        livesEnemy = GetComponent<Enemy>().livesEnemy;
        Mainbar.gameObject.SetActive(true);
    }
    private void Update()
    {
        bar.fillAmount = (float)livesEnemy.lives / 150f;   
    }
    private void OnDisable()
    {
        bar.fillAmount = 0;
    }
}
