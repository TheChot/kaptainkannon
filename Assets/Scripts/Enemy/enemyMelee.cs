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

            
            if(hit.collider != null)
            {                
                eM.isStopped = true;
                anim.SetTrigger("attack");
                canAttack = false;
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
            delayTime = delayReset;
        }

        if(hurtPlayer)
        {
            Collider2D[] playerToDamage = Physics2D.OverlapCircleAll(fist.position, fistRange, whatIsPlayer);
                    
            for (int i = 0; i < playerToDamage.Length; i++)
            {
                playerToDamage[i].gameObject.GetComponent<playerController>().takeDamage(damageGive);
            } 
        }
        

    }

    public void attackPlayer()
    {
        hurtPlayer = true;
    }

    public void ceaseAttack()
    {
        hurtPlayer = false;        
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
