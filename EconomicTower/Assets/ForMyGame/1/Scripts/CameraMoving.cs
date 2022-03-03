using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    private Vector3 startPosition;
    public Camera cam;
    ManegeSpawn skils;
    [SerializeField] ManegeSpawn manegeSpawn;
    private void Start()
    {
        cam = GetComponent<Camera>();
        skils = GameObject.FindWithTag("GameManager").GetComponent<ManegeSpawn>();
    }

    private void Update()
    {
        if (!skils.IsDrag&& !manegeSpawn.openSittings)
        {
            //получаем позицию при старте клика
            if (Input.GetMouseButtonDown(0))
            {
                startPosition = cam.ScreenToViewportPoint(Input.mousePosition);
            }

            //пока держат левую кнопку мыши
            if (Input.GetMouseButton(0))
            {
                //вычисляем дельту по х
                float pos = cam.ScreenToViewportPoint(Input.mousePosition).x - startPosition.x;
                //отнимаем дельту, для инвертированного движения
                transform.position = new Vector3(Mathf.Clamp(transform.position.x - pos, -8f, 0.5f), transform.position.y, transform.position.z);
            }
        }
    }
}
