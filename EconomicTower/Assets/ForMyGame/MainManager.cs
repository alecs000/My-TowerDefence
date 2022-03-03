using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class MainManager : MonoBehaviour
{
    GameObject emeraldsGm;
    public static MainManager instance { get; private set; }
    public static int emeralds { get; private set; }
    Text text;
    private void Awake()
    {
        emeralds = 999;
        Debug.Log(11111111111111);
        emeraldsGm = GameObject.FindGameObjectWithTag("Dimond");
        text = emeraldsGm.GetComponent<Text>();
        if (instance==null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }
        Destroy(this.gameObject);
    }
    public static bool RemoveEmeralds(int num)
    {
        if (num<=emeralds)
        {
            emeralds -= num;
            return true;
        }
        return false;
    }
    private void Update()
    {
        if (emeraldsGm==null)
        {
            emeraldsGm = GameObject.FindGameObjectWithTag("Dimond");
            text = emeraldsGm.GetComponent<Text>();
        }
        text.text = Convert.ToString(emeralds);
    }
}
