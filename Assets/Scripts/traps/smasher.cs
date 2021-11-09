using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smasher : MonoBehaviour
{
    public bool smash;
    public float timeToSmash;
    float timeReset;
    public bool controlSelf;

    public float endY;
    public float startY;
    public float riseSpeed;
    public float fallSpeed;

    public AudioSource smashSound;
    // Start is called before the first frame update
    void Start()
    {
        timeReset = timeToSmash;

        
    }

    void FixedUpdate()
    {
        if(controlSelf)
        {
            if(transform.position.y >= startY)
            {
                timeToSmash -= Time.deltaTime;

                if(timeToSmash < 0)
                {
                    smash = true;
                    timeToSmash = timeReset;
                }
            }
        }

        if(smash)
        {
            transform.position -= (transform.up * Time.fixedDeltaTime * fallSpeed);
        }

        if(smash && transform.position.y <= endY)
        {
            smash = false;
            smashSound.Play();
        }

        if(!smash && transform.position.y < startY)
        {
            transform.position += (transform.up * Time.fixedDeltaTime * riseSpeed);
        }

        if(!smash && transform.position.y >= startY)
        {
            transform.position = new Vector3(transform.position.x, startY, transform.position.z);
        }




    }
}
