using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bouncingSaw : MonoBehaviour
{
    public Vector2 sawMove;
    Rigidbody2D rb;
    public int damageGive = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(rb.velocity.x, sawMove.y);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position += (transform.right * Time.fixedDeltaTime * sawMove.x);        
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
            rb.velocity = new Vector2(rb.velocity.x, sawMove.y);          
            // gameObject.SetActive(false);
            // Destroy(gameObject);
        }
    }
}
