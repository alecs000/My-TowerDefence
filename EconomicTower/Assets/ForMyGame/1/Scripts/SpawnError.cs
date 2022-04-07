using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnError : MonoBehaviour
{
    [Header("Error")]
    [SerializeField] GameObject text1;
    [SerializeField] GameObject text2;
    [SerializeField] GameObject text3;
    [SerializeField] GameObject text4;
    [SerializeField] GameObject textRandom;
    [SerializeField] Vector3 end;
    [SerializeField] Vector3 start;
    [SerializeField] float errorWait;
    [SerializeField] Transform parent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ErrorSpawnF()
    {
        StartCoroutine(ErrorSpawn());
    }
    IEnumerator ErrorSpawn()
    {
        for (int i = 0; i < 20; i++)
        {
            int random = Random.Range(-5, 5);
            if (random>-1)
            {
                for (int j = 0; j < random; j++)
                {
                    Instantiate(text1, parent);
                    if (Random.Range(1, 10) > 8)
                    {
                        Instantiate(textRandom, parent);
                    }
                    yield return new WaitForSeconds(errorWait);
                }
            }
            int random2 = Random.Range(0, 5);
            if (random > -1)
            {
                for (int j = 0; j < random2; j++)
                {
                    Instantiate(text2, parent);
                    if (Random.Range(1, 10) > 8)
                    {
                        Instantiate(textRandom, parent);
                    }
                    yield return new WaitForSeconds(errorWait);
                }
            }
            int random3 = Random.Range(0, 5);
            if (random > -1)
            {
                for (int j = 0; j < random3; j++)
                {
                    Instantiate(text3, parent);
                    if (Random.Range(1, 10) > 8)
                    {
                        Instantiate(textRandom, parent);
                    }
                    yield return new WaitForSeconds(errorWait);
                }
            }
            int random4 = Random.Range(0, 5);
            if (random > -1)
            {
                for (int j = 0; j < random4; j++)
                {
                    Instantiate(text4, parent);
                    if (Random.Range(1, 10) > 8)
                    {
                        Instantiate(textRandom, parent);
                    }
                    yield return new WaitForSeconds(errorWait);
                }
            }
        }
    }
}
