using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IEnemy : MonoBehaviour
{
    
    public virtual LivesManagement livesEnemy { get; protected set; }
    void Attack() { }
    void Moving() { }
}
