using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shockwave : MonoBehaviour
{
    public float speed;
    public int damageGive;
    public bool moveLeft;
    
    void Start()
    {
        if(!moveLeft)
            transform.localScale = new Vector3( -1, 1, 1);    
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(!moveLeft)
        {
            transform.position += (transform.right * Time.fixedDeltaTime * speed);
        } else 
        {
            transform.position -= (transform.right * Time.fixedDeltaTime * speed);
        }
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            enemyManager.instance.pc.takeDamage(damageGive);
            // gameObject.SetActive(false);
            Destroy(gameObject);
        }

        if(other.gameObject.layer == LayerMask.NameToLayer("ground"))
        {            
            // gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
