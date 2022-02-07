using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Class1<T> where T : MonoBehaviour
{
    public T en;
    public T prefab { get; }
    public Transform container { get; }
    public Class1(T prefab, Transform container)
    {
        this.prefab = prefab;
        this.container = container;
    }
    public T CreateElement() {
        en = UnityEngine.Object.Instantiate(this.prefab, this.container);
        return en;
    }
}
