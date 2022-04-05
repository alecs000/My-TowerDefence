using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InfoManager : MonoBehaviour
{
    [SerializeField] IAlly ally;
    [SerializeField] Text attack;
    [SerializeField] Text lives;
    [SerializeField] Text waidTime;
    private void OnEnable()
    {
        attack.text = Convert.ToString(ally.GetAttack());
        Debug.Log(ally.GetAttack());
        lives.text = Convert.ToString(ally.GetLives());
        Debug.Log(ally.livesAlly);
        waidTime.text = Convert.ToString(ally.GetWaidTimeAttack());
        Debug.Log(ally.GetAttack());
    }
}
