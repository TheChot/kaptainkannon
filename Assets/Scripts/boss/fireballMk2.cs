using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballMk2 : MonoBehaviour
{
    bool fire;
    public float speed;
    public int damageGive;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        if(fire)
        {
            transform.position -= (transform.up * Time.fixedDeltaTime * speed);        
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            other.gameObject.GetComponent<playerController>().takeDamage(damageGive);
            // gameObject.SetActive(false);
            Destroy(gameObject);
        }

        if(other.gameObject.layer == LayerMask.NameToLayer("ground"))
        {            
            // gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    public void firBall()
    {
        fire = true;
    }
}
