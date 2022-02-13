using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilsManagemant : MonoBehaviour
{
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
        if (!isParticlActiv&& gm!=null)
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
                boomDes.transform.position = new Vector3(hit.point.x, hit.point.y+0.1f, hit.point.z);
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
        if (!isParticlActiv && gm!=null)
        {
            gm.transform.position = Input.mousePosition;
        }
    }
}
