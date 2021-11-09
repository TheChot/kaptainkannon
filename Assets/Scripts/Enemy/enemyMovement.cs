using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float moveSpeed = 2f;
    public bool moveLeft;

    public LayerMask whatIsGround;
    public Transform floorDetector;
    public Transform wallDetector;
    bool isFloor;
    bool isWall;   
    public bool isStopped; 
    Animator anim;
    public float floorRange;
    public float wallRange;
    public float stopTime;
    float stopTimeReset;
    public bool canControl = true;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // floorDetector = transform.GetChild(0);
        // wallDetector = transform.GetChild(1);    
        anim = GetComponent<Animator>();    
        stopTimeReset = stopTime;
    }

    // Update is called once per frame
    void Update()
    {
            
        
        
    }

    void FixedUpdate()
    {
        anim.SetBool("stopped", isStopped);
        if(canControl)
        {
            if(isStopped)
            {
                stopTime -= Time.deltaTime;

                if(stopTime < 0)
                {
                    stopTime = stopTimeReset;
                    isStopped = false;
                    moveLeft = !moveLeft;
                }
            }        
            
            if(!isStopped)
            {
                // anim.SetBool("isMoving", true);
                anim.SetFloat("move", moveSpeed);
                if(!moveLeft)
                {
                    rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
                    
                } else 
                {
                    rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
                }
                
                
            } else
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                anim.SetFloat("move", 0);
                // anim.SetBool("isMoving", false);
            }
            
            

            if (rb.velocity.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (rb.velocity.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            isFloor = Physics2D.OverlapCircle(floorDetector.position, floorRange, whatIsGround);
            isWall = Physics2D.OverlapCircle(wallDetector.position, wallRange, whatIsGround);
            if(!isFloor || isWall)
            {
                isStopped = true;
            }

        }
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(floorDetector.position, floorRange);
        Gizmos.DrawWireSphere(wallDetector.position, wallRange);
    }

    public void turnAround()
    {
        moveLeft = !moveLeft;
        isStopped = false;        
    }
    
}
