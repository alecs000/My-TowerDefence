using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManegeSpawn : MonoBehaviour
{
    public GameObject mage;
    private Vector3 spawnPositionMage;
    public List<IEnemy> EnemyList = new List<IEnemy>();
    public List<IAlly> AllyList = new List<IAlly>();

    // Start is called before the first frame update
    void Start()
    {
        spawnPositionMage = new Vector3(7, 0, -1.3f);
    }
    public void SpawnMage()
    {
        if (CoinsMangement.RemoveCoins(20))
        {
            Instantiate(mage, spawnPositionMage, mage.transform.rotation);
        }
    }
    public void SpawnKnight()
    {
        //if (CoinsMangement.RemoveCoins(20))
        //{
        //    Instantiate(mage, spawnPositionMage, mage.transform.rotation);
        //}
    }
    public  void RegistrEnemy(Enemy enemy)
    {
        EnemyList.Add(enemy);
    }
     public void RemoveEnemy(Enemy enemy)
    {
        EnemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }
    public void RegistrAlly(IAlly ally)
    {
        AllyList.Add(ally);
    }
    public void RemoveAlly(IAlly ally)
    {
        AllyList.Remove(ally);
    }

}
