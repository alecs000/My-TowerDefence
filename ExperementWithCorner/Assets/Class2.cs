using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Class2 : MonoBehaviour
{
    public Class1<Rotate> c1;
    [SerializeField] Rotate prefab;
    void Start()
    {
        c1 = new Class1<Rotate>(prefab,this.transform);
        c1.CreateElement();
        Debug.Log(c1+" ---");//Class1`1[Rotate] ---
    }


}
