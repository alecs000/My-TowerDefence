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
        if (isActive)
        {
            image = null;
            gm.gameObject.SetActive(false);
            isActive = false;
        }
        else
        {
            image = gm;
            gm.gameObject.SetActive(true);
            isActive = true;
        }
    }
}
