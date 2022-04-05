using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LownMoverManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float speedForward;
    void Start()
    {

    }
        // Update is called once per frame
        void Update()
    {
        transform.Translate(Vector3.forward * speedForward * Time.deltaTime);
    }
}
