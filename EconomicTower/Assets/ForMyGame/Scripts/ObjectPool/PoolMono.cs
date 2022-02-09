using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolMono<T> where T : MonoBehaviour
{
    public T prefab { get; }
    public bool autoExpand { get; set; }
    public Transform container { get; }
    public List<T> pool;
    public PoolMono(T prefab, int count)
    {
        this.prefab = prefab;
        this.container = null;
        this.CreatePool(count);
    }
    public PoolMono(T prefab, int count, Transform container)
    {
        this.prefab = prefab;
        this.container = container;
        this.CreatePool(count);
    }
    void CreatePool(int count)
    {
        this.pool = new List<T>();
        for (int i = 0; i < count; i++)
        {
            this.CreateObject();
        }
    }

    public T CreateObject(bool IsActiveByDefolt = false)
    {
        var createdObject = UnityEngine.Object.Instantiate(this.prefab, this.container);
        createdObject.gameObject.gameObject.SetActive(IsActiveByDefolt);
        this.pool.Add(createdObject);
        return createdObject;
    }
    public bool HasFreeElement(out T element)
    {
        foreach (var mono in pool)
        {
            if (!mono.gameObject.activeInHierarchy)
            {
                element = mono;
                mono.gameObject.SetActive(true);
                return  true;
            }
        }
        element = null;
        return false;
    }
    public T GetFreeElement()
    {
        if (this.HasFreeElement(out var element))
        {
            return element;
        }
        if (this.autoExpand)
        {
            return this.CreateObject(true);
        }
        throw new Exception("PoolNonObject");
    }


}
