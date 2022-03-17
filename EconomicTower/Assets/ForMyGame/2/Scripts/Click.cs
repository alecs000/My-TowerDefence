using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Click : MonoBehaviour
{
    public GameObject up;

    public void Close(GameObject gm)
    {
        gm.SetActive(false);
    }
    void Update()
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
                    
                    up.gameObject.SetActive(true);
                }
            }
        }
    }
}
