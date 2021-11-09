using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMelee : MonoBehaviour
{
    // detect player
    // attack gap time 
    // when the enemy attacks 
    // delay 
    Rigidbody2D rb;
    enemyMovement eM;
    public LayerMask whatIsPlayer;
    public float detectRange;
    RaycastHit2D hit;
    Animator anim;
    bool hasAttacked;

    public int damageGive;
    public Transform fist;
    public float fistRange;
    bool hurtPlayer;
    bool canAttack = true;
    public float delayTime;
    float delayReset;
    public AudioSource warningSound;
    public AudioSource punchSound;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        eM = GetComponent<enemyMovement>();
        delayReset = delayTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(canAttack)
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
                warningSound.Play();
                eM.isStopped = true;
                eM.canControl = false; 
                anim.SetTrigger("warn");
                canAttack = false;
                rb.velocity = new Vector2(0, rb.velocity.y);
                anim.SetFloat("move", 0);
            }
            
        }

        if(!canAttack && hasAttacked)
        {
            delayTime -= Time.deltaTime; 
        }

        if(delayTime < 0)
        {            
            // eM.moveLeft = !eM.moveLeft;
            canAttack = true;
            hasAttacked = false;
            eM.isStopped = false;
            eM.canControl = true;
            delayTime = delayReset;
        }

        // if(hurtPlayer)
        // {
            
        // }
        

    }

    public void attackPlayer()
    {
        Collider2D[] playerToDamage = Physics2D.OverlapCircleAll(fist.position, fistRange, whatIsPlayer);
        if(playerToDamage.Length > 0)
        {
            punchSound.Play();
        }                    
        for (int i = 0; i < playerToDamage.Length; i++)
        {
            playerToDamage[i].gameObject.GetComponent<playerController>().takeDamage(damageGive);
        } 
    }

    public void ceaseAttack()
    {
        // hurtPlayer = false;        
        hasAttacked = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 endPos = transform.position;
        endPos.x = transform.position.x + detectRange;
        Gizmos.DrawLine(transform.position, endPos);
        Gizmos.DrawWireSphere(fist.position, fistRange);
    }
}
