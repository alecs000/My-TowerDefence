using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgrateManage : MonoBehaviour
{
   Image image;
   bool isActive;
   public void ClickOnUpgrate(Image gm)
    {
        if (image!=null)
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
    public void ZeroSkillUpdate(int num)
    {
        if (MainManager.RemoveEmeralds(num))
        {
            UpgrateMemory.upgratesKnight.Add(true);
        }
    }
    public void OneSkillUpdate(int num)
    {
        if (MainManager.RemoveEmeralds(num))
        {
            UpgrateMemory.upgratesMage.Add(true);
        }
    }
}
