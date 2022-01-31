using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManegeSpawn : MonoBehaviour
{
    public GameObject mage;
    private Vector3 spawnPositionMage;
    public List<Enemy> EnemyList = new List<Enemy>();
    public List<wixard_move> AllyList = new List<wixard_move>();

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
    public  void RegistrEnemy(Enemy enemy)
    {
        EnemyList.Add(enemy);
    }
     public void RemoveEnemy(Enemy enemy)
    {
        EnemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }
    public void RegistrAlly(wixard_move ally)
    {
        AllyList.Add(ally);
    }
    public void RemoveAlly(wixard_move ally)
    {
        AllyList.Remove(ally);
        Destroy(ally.gameObject);
    }

}
