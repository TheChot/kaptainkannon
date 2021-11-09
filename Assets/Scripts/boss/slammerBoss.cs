using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slammerBoss : MonoBehaviour
{
    bool slam;
    public float slamSpeed;
    public float stayTime;
    bool disappear;
    public int damageGive;
    public boss2Controller myBoss;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(slam)
        {
            transform.position -= (new Vector3(0, slamSpeed, 0) * Time.fixedDeltaTime);
        }

        if(disappear)
        {
            stayTime -= Time.deltaTime;

            if(stayTime < 0)
            {
                if(myBoss != null)
                {
                    myBoss.canReset = true;
                }
                Destroy(gameObject);
            }
        }
    }

    public void letslam()
    {
        slam = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            other.gameObject.GetComponent<playerController>().takeDamage(damageGive);
            // gameObject.SetActive(false);
            
        }

        if(other.gameObject.layer == LayerMask.NameToLayer("ground"))
        { 
            disappear = true;
            slam = false;
            
        }
    }
}
