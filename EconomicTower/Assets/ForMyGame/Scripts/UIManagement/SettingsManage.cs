using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SettingsManage : MonoBehaviour
{
   public void Close(Image img)
    {
        img.gameObject.SetActive(false);
    }
    public void Open(Image img)
    {
        img.gameObject.SetActive(true);
    }
}
