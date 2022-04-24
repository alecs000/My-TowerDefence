using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoudScene1 : MonoBehaviour
{
    [SerializeField] GameObject blackScrin;
    public void LoudScene11()
    {
        blackScrin.SetActive(true);
        SceneManager.LoadScene("MainScence");
    }
}
