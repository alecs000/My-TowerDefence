using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAlly :MonoBehaviour
{
    public virtual LivesManagement livesAlly { get; protected set; }
    void Attack() { }
    void Moving() { }

}
