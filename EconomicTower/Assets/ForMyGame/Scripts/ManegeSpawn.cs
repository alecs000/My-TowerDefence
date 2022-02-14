using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManegeSpawn : MonoBehaviour
{
    [SerializeField] GameObject mage;
    [SerializeField] GameObject knight;
    Vector3 spawnPositionAlly;  
    public List<IAlly> AllyList = new List<IAlly>();
    public bool isFreeze;
    public Image bar;
    public Image Mainbar;

    // Start is called before the first frame update
    void Start()
    {
        spawnPositionAlly = new Vector3(7, 0, -1.3f);
    }
    public void SpawnMage()
    {
        if (CoinsMangement.RemoveCoins(30))
        {
            Instantiate(mage, spawnPositionAlly, mage.transform.rotation);

        }
    }
    public void SpawnKnight()
    {
        if (CoinsMangement.RemoveCoins(20))
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
    #region [Freeze]
    public void Freeze()
    {

        if (CoinsMangement.RemoveCoins(2))
        {
            isFreeze = true;
            StartCoroutine(FreezeAll());
        }
    }
    IEnumerator FreezeAll()
    {
        yield return new WaitForSeconds(3);
        isFreeze = false;
    }
    #endregion
    #region [Bomb]
    [Header("Bomb")]
    [SerializeField] Image prefab;
    Image gm;
    [SerializeField] Button but;
    [SerializeField] ParticleSystem boom;
    ParticleSystem boomDes;
    bool isParticlActiv;
    public bool IsDrag;
    public void Down()
    {
        IsDrag = true;
        if (!isParticlActiv)
        {
            gm = Object.Instantiate(prefab, but.transform);
        }
    }
    public void Up(GameObject grey)
    {
        IsDrag = false;
        if (!isParticlActiv && gm != null)
        {
            Vector3 mouse = Input.mousePosition;
            Ray castPoint = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit;
            boomDes = Instantiate(boom, transform.position, boom.transform.rotation);
            StartCoroutine(DelitParticl(grey));
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
            {
                Debug.DrawLine(castPoint.origin, hit.point, Color.red, 200, false);
                Debug.Log("Путь к врагу преграждает объект: " + hit.collider.name);
                boomDes.transform.position = new Vector3(hit.point.x, hit.point.y + 0.1f, hit.point.z);
            }
            Debug.Log(gm);
            Destroy(gm);
        }
    }
    IEnumerator DelitParticl(GameObject grey)
    {
        grey.SetActive(true);
        isParticlActiv = true;
        yield return new WaitForSeconds(1);
        grey.SetActive(false);
        isParticlActiv = false;
        Destroy(boomDes.gameObject);
    }
    public void Drag()
    {
        IsDrag = true;
        if (!isParticlActiv && gm != null)
        {
            gm.transform.position = Input.mousePosition;
        }
    }
    #endregion
}
