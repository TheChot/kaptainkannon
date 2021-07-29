using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyFlying : MonoBehaviour
{
    Rigidbody2D rb;
    public float moveSpeed = 2f;
    public bool moveLeft; //move left or down
    public bool moveHorizontal;

    public float moveDistance;
    float maxDis, minDis;    
    
    public bool isStopped;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // if(isF)
        if(moveHorizontal)
        {

        
            if(moveLeft)
            {
                minDis = transform.position.x - moveDistance;
                maxDis = transform.position.x;
            } else 
            {
                minDis = transform.position.x;
                maxDis = transform.position.x + moveDistance;
            }
        } 
        else
        {
            if(moveLeft)
            {
                minDis = transform.position.y - moveDistance;
                maxDis = transform.position.y;
            } else 
            {
                minDis = transform.position.y;
                maxDis = transform.position.y + moveDistance;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {   
        
    }

    void FixedUpdate()
    {
        
        if(moveHorizontal)
        {
            if(transform.position.x < minDis || transform.position.x > maxDis)
            {
                moveLeft = !moveLeft;
            }
            
            if(!isStopped)
            {
                if(!moveLeft)
                {
                    rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
                } else 
                {
                    rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
                }
            } else 
            {
                rb.velocity = new Vector2(0, 0);
            }
            
        } 
        else 
        {
            if(transform.position.y < minDis || transform.position.y > maxDis)
            {
                moveLeft = !moveLeft;
            }

            if(!isStopped)
            {
                if(!moveLeft)
                {
                    rb.velocity = new Vector2(rb.velocity.x, moveSpeed);
                } else 
                {
                    rb.velocity = new Vector2(rb.velocity.x, -moveSpeed);
                }
            } else 
            {
                rb.velocity = new Vector2(0, 0);
            }
        }

        if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        
    }

}
