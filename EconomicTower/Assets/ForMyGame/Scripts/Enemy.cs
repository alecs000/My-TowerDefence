using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject manegeSp;
    private void Start()
    {
         
        manegeSp.GetComponent<ManegeSpawn>().RegistrEnemy(this);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
