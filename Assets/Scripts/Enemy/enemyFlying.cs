using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyFlying : MonoBehaviour
{
    Rigidbody2D rb;
    public float moveSpeed = 2f;
    public bool changeDir; //move left or down
    public bool moveHorizontal;

    public float moveDistance;
    float maxDis, minDis;    
    // public bool isFlying;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // if(isF)
        if(moveHorizontal)
        {

        
            if(changeDir)
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
            if(changeDir)
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
                changeDir = !changeDir;
            }

            if(!changeDir)
            {
                rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            } else 
            {
                rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
            }
        } 
        else 
        {
            if(transform.position.y < minDis || transform.position.y > maxDis)
            {
                changeDir = !changeDir;
            }

            if(!changeDir)
            {
                rb.velocity = new Vector2(rb.velocity.x, moveSpeed);
            } else 
            {
                rb.velocity = new Vector2(rb.velocity.x, -moveSpeed);
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
