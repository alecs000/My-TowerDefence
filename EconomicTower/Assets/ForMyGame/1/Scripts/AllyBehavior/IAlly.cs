using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAlly :MonoBehaviour
{
    public virtual LivesManagement livesAlly { get; protected set; }
    public virtual float GetAttack()
    {
        return 990;
    }
    public virtual float GetWaidTimeAttack()
    {
        return 991;
    }
    public virtual float GetLives()
    {
        return 992;
    }
    void Attack() { }
    void Moving() { }

}
