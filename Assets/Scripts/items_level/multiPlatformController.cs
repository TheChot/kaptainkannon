using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class multiPlatformController : MonoBehaviour
{
    public float timer;
    float timerReset;
    bool altPlat;
    public platformAuto[] platforms;
    public platformAuto[] platformsAlt;
    // Start is called before the first frame update
    void Start()
    {
        timerReset = timer;
        if(altPlat)
        {
            for (int i = 0; i < platforms.Length; i++)
            {
                platforms[i].isActive = true;
            }
            for (int i = 0; i < platformsAlt.Length; i++)
            {
                platformsAlt[i].isActive = false;
            }
        } else
        {
            for (int i = 0; i < platforms.Length; i++)
            {
                platforms[i].isActive = false;
            }
            for (int i = 0; i < platformsAlt.Length; i++)
            {
                platformsAlt[i].isActive = true;
            }
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer -= Time.fixedDeltaTime;

        if(timer <= 0)
        {
            timer = timerReset;
            altPlat = !altPlat;
            if(altPlat)
            {
                for (int i = 0; i < platforms.Length; i++)
                {
                    platforms[i].isActive = true;
                }
                for (int i = 0; i < platformsAlt.Length; i++)
                {
                    platformsAlt[i].isActive = false;
                }
            } else
            {
                for (int i = 0; i < platforms.Length; i++)
                {
                    platforms[i].isActive = false;
                }
                for (int i = 0; i < platformsAlt.Length; i++)
                {
                    platformsAlt[i].isActive = true;
                }
            }
        }

    }
}
