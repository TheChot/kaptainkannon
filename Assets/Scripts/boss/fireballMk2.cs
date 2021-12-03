using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireballMk2 : MonoBehaviour
{
    bool fire;
    public float speed;
    public int damageGive;
    public GameObject explosion;
    
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
            enemyManager.instance.pc.takeDamage(damageGive);
            // gameObject.SetActive(false);
            GameObject _explosionClone = (GameObject)Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        if(other.gameObject.layer == LayerMask.NameToLayer("ground"))
        { 
            GameObject _explosionClone = (GameObject)Instantiate(explosion, transform.position, transform.rotation);           
            // gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

    public void firBall()
    {
        fire = true;
    }
}
