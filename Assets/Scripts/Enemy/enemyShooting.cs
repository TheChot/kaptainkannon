using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShooting : MonoBehaviour
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
    bool canAttack = true;
    public float delayTime;
    float delayReset;


    
    public Transform shootPoint;
    public GameObject bullet;
    public int bulletsToFire;
    public float shootDelayTime;
    public float shootBulletDelay;
    int bulletCount;
    public float nextAttackCount;
    float nextReset;
    bool startNextCount;

    public AudioSource warningSound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        eM = GetComponent<enemyMovement>();
        delayReset = delayTime;
        nextReset = nextAttackCount;
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
                eM.isStopped = true;
                eM.canControl = false; 
                anim.SetTrigger("warn");
                canAttack = false;
                rb.velocity = new Vector2(0, rb.velocity.y);
                anim.SetFloat("move", 0);
                warningSound.Play();
                // StartCoroutine(attackPlayer());                
                
            }   

                   
        }

        if(!canAttack && hasAttacked)
        {
            delayTime -= Time.deltaTime; 
        }

        if(delayTime < 0)
        {
            hasAttacked = false;
            eM.isStopped = false;
            eM.canControl = true;
            delayTime = delayReset;
            startNextCount = true;
        }

        if(startNextCount)
        {
            nextAttackCount -= Time.deltaTime;
        }

        if(nextAttackCount < 0)
        {
            canAttack = true;
            startNextCount = false;
            nextAttackCount = nextReset;
        }      

    }

    public void attackThePlayer()
    {
        GameObject bulletClone = (GameObject)Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        
        if(transform.localScale.x < 0)        
        {
            // bulletClone.transform.localScale = new Vector3(-1,1,1);
            Quaternion rotation = bulletClone.transform.rotation;
            rotation.eulerAngles = new Vector3(0,0, 180);
            bulletClone.transform.rotation = rotation;
        }
    }

    public void ceaseAttack()
    {
        // hurtPlayer = false;        
        hasAttacked = true;

    }

    // IEnumerator attackPlayer()
    // {
    //     yield return new WaitForSeconds(shootBulletDelay);
    //     for (int i = 0; i < bulletsToFire; i++)
    //     {
    //         yield return new WaitForSeconds(shootDelayTime);
    //         GameObject bulletClone = (GameObject)Instantiate(bullet, shootPoint.position, shootPoint.rotation);
            
    //     }
    //     hasAttacked = true;
    // }    

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 endPos = transform.position;
        endPos.x = transform.position.x + detectRange;
        Gizmos.DrawLine(transform.position, endPos);
    }
}
