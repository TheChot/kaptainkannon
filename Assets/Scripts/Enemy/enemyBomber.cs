using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBomber : MonoBehaviour
{
    Rigidbody2D rb;
    enemyMovement eM;
    public LayerMask whatIsOther;
    public LayerMask whatIsPlayer;
    RaycastHit2D hit;
    Animator anim;
    public float detectRange;
    bool canAttack;
    public float speed;
    public float bombRange;
    bool isSomething; 
    GameObject explosion;
    public Transform killZone;
    public AudioSource warningSound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        eM = GetComponent<enemyMovement>();
        explosion = enemyManager.instance.bomberExplosion;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        

        if(transform.localScale.x > 0)
        {
            hit = Physics2D.Raycast(transform.position, transform.right, detectRange, whatIsPlayer);    
        } else 
        {
            hit = Physics2D.Raycast(transform.position, -transform.right, detectRange, whatIsPlayer);    
        }

        if(hit.collider != null && hit.collider.gameObject.layer == 10)
        {
            eM.isStopped = true;
            eM.canControl = false;                
            anim.SetTrigger("warn");
            
            // canAttack = true;
            if(!canAttack)
            {
                warningSound.Play();
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }

        if(canAttack)
        {
            if(transform.localScale.x > 0)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            } else 
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }

            isSomething = Physics2D.OverlapCircle(killZone.position, bombRange, whatIsOther);

            if(isSomething)
            {
                blowUp();
            }


        }
    }

    public void attackPlayer()
    {
        canAttack = true;
    }

    void blowUp()
    {
        Instantiate(explosion, transform.position, transform.rotation);
        gameObject.SetActive(false);
        // explosion code here
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Vector3 endPos = transform.position;
        endPos.x = transform.position.x + detectRange;
        Gizmos.DrawLine(transform.position, endPos);
        Gizmos.DrawWireSphere(killZone.position, bombRange);
    }


}
