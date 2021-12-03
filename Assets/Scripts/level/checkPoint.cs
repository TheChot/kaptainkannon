using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPoint : MonoBehaviour
{
    public int checkPointIndex;
    public bgMusicSearch bgM;
    public bgSwitch bgs;
    public GameObject flare;
    checkPointManager cm;

    void Start()
    {
        cm = checkPointManager.instance;
    }

    void FixedUpdate()
    {
        if(checkPointIndex <= cm.checkPointIndex)
        {
            flare.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if(other.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            if(checkPointIndex > cm.checkPointIndex)
            {
                cm.lastCheckPointPos = transform.position;
                cm.checkPointUsed = true;
                cm.levelTime = scoreManager.instance.levelTimer;
                cm.checkPointIndex = checkPointIndex;
                flare.SetActive(true);
                if(bgM != null)
                {
                    cm.songIndex = bgM.songIndex;
                    cm.songSet = true;
                }
                if(bgs != null)
                {
                    cm.bgIndex = bgs.bgIndex;
                    cm.bgSet = true;
                }
                // cm.enemiesDefeated = scoreManager.instance.enemiesKilled;
            }
            
        }
    }
}
