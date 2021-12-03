using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossWeakSpot : MonoBehaviour
{
    bossController bC;
    public bool isWeaken;
    public GameObject confirmHit1;
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
                if(bC.confirmHit != null)
                    bC.confirmHit.SetActive(true);
                
                if(confirmHit1 != null)
                    confirmHit1.SetActive(true);
            }
        } else 
        {
            bC.hit -= 1;
            if(confirmHit1 != null)
                confirmHit1.SetActive(true);
        }
    }
}
