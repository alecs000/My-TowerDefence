using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

public class Click : MonoBehaviour, IPointerClickHandler
{
    public GameObject up;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        up.gameObject.SetActive(true);
    }
    public void Close(GameObject gm)
    {
        gm.SetActive(false);
    }
}
