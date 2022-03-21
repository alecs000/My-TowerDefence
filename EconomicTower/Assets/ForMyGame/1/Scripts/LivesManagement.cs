using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesManagement 
{
    public float lives { get; private set; }
    // Start is called before the first frame update
    public LivesManagement(int l)
    {
        lives = l;
    }
    public void AddLives(float addition)
    {
        lives += addition;
    }
    public void RemoveLives(float addition)
    {
            lives -= addition;
    }
}
