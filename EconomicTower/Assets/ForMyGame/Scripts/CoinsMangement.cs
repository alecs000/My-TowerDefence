using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class CoinsMangement : MonoBehaviour
{
    public static short coins { get; private set; }
    public TMP_Text textCoins;
    // Start is called before the first frame update
    private void Start()
    {
        coins = 30;
    }
    public static void AddCoins(short addition)
    {
        coins += addition;
    }
    public static bool RemoveCoins(short addition)
    {
        if (coins>= addition)
        {
            coins -= addition;
            return true;
        }
        else
        {
            return false; 
        }
    }
    private void Update()
    {
        textCoins.text = Convert.ToString(coins);
    }
}
