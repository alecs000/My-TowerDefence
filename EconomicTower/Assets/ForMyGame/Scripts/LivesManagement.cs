using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesManagement 
{
    public short lives { get; private set; }
    // Start is called before the first frame update
    public LivesManagement(short l)
    {
        lives = l;
    }
    public void AddLives(short addition)
    {
        lives += addition;
    }
    public void RemoveLives(short addition)
    {
            lives -= addition;
    }
}
