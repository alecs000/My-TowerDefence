using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class EnergyMenegment : MonoBehaviour
{
    public static short energy { get; private set; }
    public Text textEnergy;
    // Start is called before the first frame update
    private void Start()
    {
        energy = 10;
    }
    public static void AddEnergy(short addition)
    {
        energy += addition;
    }
    public static bool RemoveEnergy(short addition)
    {
        if (energy >= addition)
        {
            energy -= addition;
            return true;
        }
        else
        {
            return false;
        }
    }
    private void Update()
    {
        textEnergy.text = Convert.ToString(energy);
    }
}
