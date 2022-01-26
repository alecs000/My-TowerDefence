using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManegeSpawn : MonoBehaviour
{
    public GameObject mage;
    private Vector3 spawnPositionMage;
    public List<Enemy> EnemyList = new List<Enemy>();
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
    public  void RegistrEnemy(Enemy enemy)
    {
        EnemyList.Add(enemy);
        Debug.Log(22);
    }
     public void RemoveEnemy(Enemy enemy)
    {
        EnemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }

}
