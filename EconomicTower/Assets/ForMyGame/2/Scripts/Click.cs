using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Click : MonoBehaviour
{
    [SerializeField] GameObject upgrade;
    [SerializeField] GameObject spellsUpgrade;
    [SerializeField] GameObject allUpgrade;
    bool allWindowClose =true;
    [SerializeField] AudioSource doorEffect;

    public void Close(GameObject gm)
    {
        gm.SetActive(false);
        allWindowClose = true;
        
    }
    void Update()
    {
        if (allWindowClose)
        {
            RaycastHit hit;
            if (Input.GetMouseButton(0))
            {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
                {
                    Debug.DrawRay(transform.position, Input.mousePosition, Color.red);
                    Debug.Log(hit.collider.gameObject.name);
                    if (hit.collider.gameObject.CompareTag("Upgrade"))
                    {
                        allWindowClose = false;
                        upgrade.gameObject.SetActive(true);
                        doorEffect.Play();
                    }
                    if (hit.collider.gameObject.CompareTag("SpellsUpgrade"))
                    {
                        allWindowClose = false;
                        spellsUpgrade.gameObject.SetActive(true);
                        doorEffect.Play();
                    }
                    if (hit.collider.gameObject.CompareTag("AllUpgrade"))
                    {
                        allWindowClose = false;
                        allUpgrade.gameObject.SetActive(true);
                        doorEffect.Play();
                    }
                }
            }
        }
    }
}
