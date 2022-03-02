using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallMoving : MonoBehaviour
{
    [SerializeField] float speed;
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (enemy.activeInHierarchy)
        {
            transform.Translate((enemy.transform.position - transform.position).normalized * Time.deltaTime * speed);
        }
        if(!enemy.activeInHierarchy)
        {
            Destroy(this.gameObject);
        }
    }
}
