using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activator : MonoBehaviour
{
    //Activates various platforms 
    // and objectsw once the trigger is touched
    public platformMoving[] movingPlatforms;
    public platformAuto[] platformDrops;
    public saw[] saws;
    public GameObject[] stuff;
    public bool deactivator;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            if(!deactivator)
            {
                if(movingPlatforms.Length > 0)
                {
                    for (int i = 0; i < movingPlatforms.Length; i++)
                    {
                        movingPlatforms[i].activatePlatform = true;
                    }
                }

                if(platformDrops.Length > 0)
                {
                    for (int i = 0; i < platformDrops.Length; i++)
                    {
                        platformDrops[i].activatePlatform = true;
                    }
                }

                if(saws.Length > 0)
                {
                    for (int i = 0; i < saws.Length; i++)
                    {
                        saws[i].activateSaw = true;
                    }
                }

                if(stuff.Length > 0)
                {
                    for (int i = 0; i < stuff.Length; i++)
                    {
                        stuff[i].SetActive(true);
                    }
                }
            } else 
            {
                if(stuff.Length > 0)
                {
                    for (int i = 0; i < stuff.Length; i++)
                    {
                        stuff[i].SetActive(false);
                    }
                }
            }
        }        
    }
}
