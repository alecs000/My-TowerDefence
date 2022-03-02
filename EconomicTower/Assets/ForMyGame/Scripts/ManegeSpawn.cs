using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ManegeSpawn : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] GameObject mage;
    [SerializeField] GameObject knight;
    Vector3 spawnPositionAlly;  
    public List<IAlly> AllyList = new List<IAlly>();
    public bool isFreeze;
    public Image bar;
    public Image Mainbar;
    public bool IsDrag;

    

    // Start is called before the first frame update
    void Start()
    {
        spawnPositionAlly = new Vector3(7, 0, -1.3f);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }
    public void SpawnMage(GameObject red)
    {
        if (CoinsMangement.RemoveCoins(30))
        {
            Instantiate(mage, spawnPositionAlly, mage.transform.rotation);
        }
        else
        {
            red.SetActive(true);
            StartCoroutine(BannerRed(red));
        }
    }
    IEnumerator BannerRed(GameObject red)
    {
        yield return new WaitForSeconds(0.2f);
        red.SetActive(false);
    }
    public void SpawnKnight(GameObject red)
    {
        if (CoinsMangement.RemoveCoins(20))
        {
            Instantiate(knight, spawnPositionAlly, knight.transform.rotation);
        }
        else
        {
            red.SetActive(true);
            StartCoroutine(BannerRed(red));
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
    public void Freeze(GameObject red)
    {

        if (CoinsMangement.RemoveCoins(12))
        {
            isFreeze = true;
            StartCoroutine(FreezeAll());
        }
        else
        {
            red.SetActive(true);
            StartCoroutine(BannerRed(red));
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
    //œŒ ÕŒœ » Õ¿—“–Œ… »
    public void NotDrag()
    {
        IsDrag = true;
    }
    //!!!!!!!!!!!! œŒ  –≈—“» ” Ã≈Õﬂ“‹ Œ¡–¿“ÕŒ!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    
    bool IsBomb;
    public void Down(GameObject red)
    {
        IsDrag = true;
        if (!isParticlActiv&& CoinsMangement.RemoveCoins(40))
        {
            IsBomb = true;
            gm = Object.Instantiate(prefab, but.transform);
        }
        else
        {
            red.SetActive(true);
            StartCoroutine(BannerRed(red));
        }
    }
    public void Up(GameObject grey)
    {
        IsDrag = false;
        if (!isParticlActiv && gm != null&& IsBomb)
        {
            Vector3 mouse = Input.mousePosition;
            Ray castPoint = Camera.main.ScreenPointToRay(mouse);
            RaycastHit hit;
            boomDes = Instantiate(boom, transform.position, boom.transform.rotation);
            StartCoroutine(DelitParticl(grey));
            if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
            {
                Debug.DrawLine(castPoint.origin, hit.point, Color.red, 200, false);
                Debug.Log("œÛÚ¸ Í ‚‡„Û ÔÂ„‡Ê‰‡ÂÚ Ó·˙ÂÍÚ: " + hit.collider.name);
                boomDes.transform.position = new Vector3(hit.point.x, hit.point.y + 0.1f, hit.point.z);
            }
            Debug.Log(gm);
            IsBomb = false;
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
        if (!isParticlActiv && gm != null&& IsBomb)
        {
            gm.transform.position = Input.mousePosition;
        }
    }

    
    #endregion
}
