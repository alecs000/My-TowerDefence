using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManegeSpawn : MonoBehaviour
{
    [SerializeField] GameObject mage;
    [SerializeField] GameObject knight;
    Vector3 spawnPositionAlly;  
    public List<IAlly> AllyList = new List<IAlly>();

    // Start is called before the first frame update
    void Start()
    {
        spawnPositionAlly = new Vector3(7, 0, -1.3f);
    }
    public void SpawnMage()
    {
        if (CoinsMangement.RemoveCoins(20))
        {
            Instantiate(mage, spawnPositionAlly, mage.transform.rotation);

        }
    }
    public void SpawnKnight()
    {
        if (CoinsMangement.RemoveCoins(30))
        {
            Instantiate(knight, spawnPositionAlly, knight.transform.rotation);
        }
    }

    public void RegistrAlly(IAlly ally)
    {
        AllyList.Add(ally);
        Debug.Log(ally);
    }
    public void RemoveAlly(IAlly ally)
    {
        AllyList.Remove(ally);
    }

}
