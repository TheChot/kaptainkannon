using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgSwitcher : MonoBehaviour
{
    // Start is called before the first frame update
    public bgSwitch bgs;
    public int indexToSet;
    bool hasSet;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            if(!hasSet)
            {
                bgs.bgToSet(indexToSet);
                hasSet = true;
            }
        }
    }
}
