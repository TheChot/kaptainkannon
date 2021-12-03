using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bouncer : MonoBehaviour
{
    public float bounceHeight;
    Rigidbody2D rb;  
    public Animator anim; 

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            rb = collision.gameObject.GetComponent<Rigidbody2D>();
            anim.SetTrigger("bounce");
            rb.velocity = new Vector2(rb.velocity.x, bounceHeight);
        }
        
    }
}
