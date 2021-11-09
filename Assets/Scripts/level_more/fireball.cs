using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour
{
    public float speed;
    public int damageGive;
    public GameObject explosion;
    
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += (transform.right * Time.fixedDeltaTime * speed);        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            other.gameObject.GetComponent<playerController>().takeDamage(damageGive);
            // gameObject.SetActive(false);
            // impactSound.Play();
            GameObject _explosion = (GameObject)Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        if(other.gameObject.layer == LayerMask.NameToLayer("ground"))
        {            
            // gameObject.SetActive(false);
            GameObject _explosion = (GameObject)Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
