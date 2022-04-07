using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorManager : MonoBehaviour
{
    [SerializeField] int speed;
    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
