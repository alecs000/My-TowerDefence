using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallMoving : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] GameObject mage;
    wixard_move wxMove;
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        wxMove = mage.GetComponent<wixard_move>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate((enemy.transform.position - transform.position).normalized * Time.deltaTime * speed);
    }
}
