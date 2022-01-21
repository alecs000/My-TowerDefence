using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManegeSpawn : MonoBehaviour
{
    public GameObject mage;
    private Vector3 spawnPositionMage;
    // Start is called before the first frame update
    void Start()
    {
        spawnPositionMage = new Vector3(7, 0, -1.3f);
    }
    public void SpawnMage()
    {
        Instantiate(mage, spawnPositionMage, mage.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
