using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AllUpgradeManager : MonoBehaviour
{
    static int priceAttack = 50;
    static int priceHP = 50;
    static int priceMoney = 50;
    public void AttackUpgrate(Text text)
    {
        if (MainManager.RemoveEmeralds(priceAttack))
        {
            UpgrateMemory.increaseAttack += 0.3f;
            priceAttack += 50;
            text.text = Convert.ToString(priceAttack);
        }
    }
    public void HPUpgrate(Text text)
    {
            if (MainManager.RemoveEmeralds(priceHP))
            {
                UpgrateMemory.increaseHP += 0.5f;
                priceHP += 50;
                text.text = Convert.ToString(priceHP);
            }
    }
    public void MoneyUpgrate(Text text)
    {
        if (MainManager.RemoveEmeralds(priceMoney))
        {
            UpgrateMemory.increaseMoney += 10;
            priceMoney += 50;
            text.text = Convert.ToString(priceMoney);
        }
    }
}
