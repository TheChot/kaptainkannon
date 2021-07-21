using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformAuto : MonoBehaviour
{
    Collider2D col;

    GameObject openPlat;
    GameObject closedPlat;

    public float timingDelay;
    public bool isActive;
    public float holdTime; //time for platform to stay
    float holdTimeReset;
    public float reappearTime; //time for platform to reappear
    float reappearTimeReset;

    public bool activatePlatform;
    // Start is called before the first frame update
    void Start()
    {
        reappearTimeReset = reappearTime;
        holdTimeReset = holdTime;

        col = GetComponent<Collider2D>();

        openPlat = transform.GetChild(0).gameObject;
        closedPlat = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(activatePlatform)
        {
            timingDelay -= Time.deltaTime;

            if(timingDelay <= 0)
            {
                if(isActive)
                {
                    holdTime -= Time.deltaTime;

                    // add shake animation here

                    if(holdTime <= 0)
                    {
                        isActive = false;
                        holdTime = holdTimeReset;
                    }
                    
                } else 
                {
                    reappearTime -= Time.deltaTime;
                    // add shake animation here 
                    if(reappearTime <= 0)
                    {
                        isActive = true;
                        reappearTime = reappearTimeReset;
                    }
                }
            }
        }

        if(isActive)
        {
            col.enabled = true;
            openPlat.SetActive(true);
            closedPlat.SetActive(false);
        } else 
        {
            col.enabled = false;
            openPlat.SetActive(false);
            closedPlat.SetActive(true);
        }
    
    }
}
