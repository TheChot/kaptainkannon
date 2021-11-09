using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autospikeAll : MonoBehaviour
{
    public autospike[] autospikeOne; //up on alt
    public autospike[] autospikeTwo; //down on alt

    bool alt;
    // bool warn;
    // bool down;

    public float uptime;
    float upTimeReset;

    // Start is called before the first frame update
    void Start()
    {
        upTimeReset = uptime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float _uptime = upTimeReset * 0.4f;

        uptime -= Time.deltaTime;
        
        if(!alt)
        {
            
            if(uptime < _uptime)
            {
                warn();
            } else 
            {
                activate();
            }

            if(uptime < 0)
            {
                down();
                alt = true;
                uptime = upTimeReset;
            }

        } else 
        {
            if(uptime < _uptime)
            {
                warn();
            } else 
            {
                activate();
            }

            if(uptime < 0)
            {
                down();
                alt = false;
                uptime = upTimeReset;
            }

        }
    }

    public void warn()
    {
        if(!alt)
        {
            for (int i = 0; i < autospikeTwo.Length; i++)
            {
                autospikeTwo[i].activate = false;
                autospikeTwo[i].warn = true;
                autospikeTwo[i].down = false;
            }
            for (int i = 0; i < autospikeOne.Length; i++)
            {
                autospikeOne[i].activate = true;
                autospikeOne[i].warn = false;
                autospikeOne[i].down = false;
            }
        } else 
        {
            for (int i = 0; i < autospikeTwo.Length; i++)
            {
                autospikeTwo[i].activate = true;
                autospikeTwo[i].warn = false;
                autospikeTwo[i].down = false;
            }
            for (int i = 0; i < autospikeOne.Length; i++)
            {
                autospikeOne[i].activate = false;
                autospikeOne[i].warn = true;
                autospikeOne[i].down = false;
            }
        }
    }

    public void down()
    {
        if(!alt)
        {
            for (int i = 0; i < autospikeTwo.Length; i++)
            {
                autospikeTwo[i].activate = false;
                autospikeTwo[i].warn = false;
                autospikeTwo[i].down = true;
            }
            for (int i = 0; i < autospikeOne.Length; i++)
            {
                autospikeOne[i].activate = false;
                autospikeOne[i].warn = false;
                autospikeOne[i].down = true;
            }
        } else 
        {
            for (int i = 0; i < autospikeTwo.Length; i++)
            {
                autospikeTwo[i].activate = false;
                autospikeTwo[i].warn = false;
                autospikeTwo[i].down = true;
            }
            for (int i = 0; i < autospikeOne.Length; i++)
            {
                autospikeOne[i].activate = true;
                autospikeOne[i].warn = false;
                autospikeOne[i].down = false;
            }
        }
    }
    
    public void activate()
    {
        if(!alt)
        {
            for (int i = 0; i < autospikeTwo.Length; i++)
            {
                autospikeTwo[i].activate = false;
                autospikeTwo[i].warn = false;
                autospikeTwo[i].down = true;
            }
            for (int i = 0; i < autospikeOne.Length; i++)
            {
                autospikeOne[i].activate = true;
                autospikeOne[i].warn = false;
                autospikeOne[i].down = false;
            }
        } else 
        {
            for (int i = 0; i < autospikeTwo.Length; i++)
            {
                autospikeTwo[i].activate = true;
                autospikeTwo[i].warn = false;
                autospikeTwo[i].down = false;
            }
            for (int i = 0; i < autospikeOne.Length; i++)
            {
                autospikeOne[i].activate = false;
                autospikeOne[i].warn = false;
                autospikeOne[i].down = true;
            }
        }
    }
}
