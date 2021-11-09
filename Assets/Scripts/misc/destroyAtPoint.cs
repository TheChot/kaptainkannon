using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyAtPoint : MonoBehaviour
{
    public bool isUp;
    public bool isNeg;
    public float thePoint;
    

    // Update is called once per frame
    void Update()
    {
        if(isUp)
        {
            if(!isNeg)
            {
                if(transform.position.y > thePoint)
                {
                    Destroy(gameObject);
                }
            } else
            {
                if(transform.position.y < thePoint)
                {
                    Destroy(gameObject);
                }
            }
        } else 
        {
            if(!isNeg)
            {
                if(transform.position.x > thePoint)
                {
                    Destroy(gameObject);
                }
            } else
            {
                if(transform.position.x < thePoint)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
