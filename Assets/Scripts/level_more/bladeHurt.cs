using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bladeHurt : MonoBehaviour
{    
    public int damageGive;    
    public GameObject explosion;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            Instantiate(explosion, transform.position, transform.rotation);
            collision.gameObject.GetComponent<playerController>().takeDamage(damageGive);
            // Destroy(gameObject);
            gameObject.SetActive(false);
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("ground"))
        {
            Instantiate(explosion, transform.position, transform.rotation);
            // Destroy(gameObject);
            gameObject.SetActive(false);
        }
    }
}
