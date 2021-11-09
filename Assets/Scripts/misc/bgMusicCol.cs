using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgMusicCol : MonoBehaviour
{
    public bgMusicSearch bgM;
    public int indexToSet;
    bool hasSet;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            if(!hasSet)
            {
                bgM.songToSet(indexToSet);
                hasSet = true;
            }
        }
    }
}
