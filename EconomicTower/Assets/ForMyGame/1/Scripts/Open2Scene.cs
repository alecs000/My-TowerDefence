using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Open2Scene : MonoBehaviour
{
    [SerializeField] MainManager mainManager;
    public void LoudScene()
    {
        mainManager
        SceneManager.LoadScene("SampleScene");
    }
}
