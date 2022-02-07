using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] GameObject obj;
    Class2 c2;
    void Start()
    {
        c2 = obj.GetComponent<Class2>(); 
    }
    void Update()
    {
        Debug.Log(c2);
        Debug.Log(c2.c1);//null
        Debug.Log(c2.c1.en);//null
    }
}
