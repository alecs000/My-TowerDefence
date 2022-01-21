using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wixard_move : MonoBehaviour
{
    private Animator anim;
    public float speed;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("move_forward");
            speed = 0;
            Debug.Log(1111);
        }
        else
        {
            speed = 3;
            Debug.Log(22222);
        }
    }
}
