using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    [SerializeField] GameObject emeraldsPrefab;
    public static MainManager instance { get; private set; }
    int emeralds;
    private void Awake()
    {
        Debug.Log(333);
        if (instance==null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }
        Destroy(this.gameObject);
    }
}
