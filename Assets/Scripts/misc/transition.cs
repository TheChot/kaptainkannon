using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transition : MonoBehaviour
{
    public void levelStart()
    {
        levelManager.instance.levelStart = true;
    }

    public void levelEnd()
    {
        if(!levelManager.instance.isDead && !levelManager.instance.isRestarting)
        {
            levelManager.instance.nextLevel();
        } else 
        {
            levelManager.instance.restartLevel();
        }
        
    }
}
