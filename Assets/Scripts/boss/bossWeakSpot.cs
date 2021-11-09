using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossWeakSpot : MonoBehaviour
{
    bossController bC;
    public bool isWeaken;
    // Start is called before the first frame update
    public void hitWeak()
    {
        bC = bossManager.instance.bC;
        if(!isWeaken)
        {
            if(bC.exposeWeak)
            {
                bC.bossHealth -= 1;
                bC.exposeWeak = false;
            }
        } else 
        {
            bC.hit -= 1;
        }
    }
}
