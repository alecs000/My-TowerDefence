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
        attack.text = Convert.ToString(ally.attackAllay);
        lives.text = Convert.ToString(ally.livesAlly);
        waidTime.text = Convert.ToString(ally.waitTimeAlly);
    }
}
