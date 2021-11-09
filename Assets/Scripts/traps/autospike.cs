using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autospike : MonoBehaviour
{
    Animator anim;

    public bool activate;
    public bool warn;
    public bool down;

    public float upTime;
    float upTimeReset;

    public bool controlSelf;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        upTimeReset = upTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(controlSelf)
        {
            float _uptime = upTimeReset * 0.4f;

            upTime -= Time.deltaTime;
            
            if(!activate)
            {

                if(upTime < _uptime)
                {
                    warn = true;
                    down = false;
                    activate = false;
                } else 
                {
                    down = true;
                }

                if(upTime < 0)
                {
                    activate = true;
                    upTime = upTimeReset;
                }

            } else 
            {
                if(upTime < 0)
                {
                    activate = false;
                    warn = false;
                    down = true;
                    upTime = upTimeReset;
                }
            }

            
        }

        anim.SetBool("warn", warn);
        anim.SetBool("up", activate);
        anim.SetBool("down", down);
        



    }
}
