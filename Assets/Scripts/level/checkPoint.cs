using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPoint : MonoBehaviour
{
    public int checkPointIndex;

    void OnTriggerEnter2D(Collider2D other)
    {
        checkPointManager cm = checkPointManager.instance;
        if(other.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            if(checkPointIndex > cm.checkPointIndex)
            {
                cm.lastCheckPointPos = transform.position;
                cm.checkPointUsed = true;
                cm.levelTime = scoreManager.instance.levelTimer;
                cm.checkPointIndex = checkPointIndex;
                // cm.enemiesDefeated = scoreManager.instance.enemiesKilled;
            }
            
        }
    }
}
