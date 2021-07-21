﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float moveSpeed = 2f;
    public bool moveLeft;

    public LayerMask whatIsGround;
    Transform floorDetector;
    Transform wallDetector;
    bool isFloor;
    bool isWall;    
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        floorDetector = transform.GetChild(0);
        wallDetector = transform.GetChild(1);        
    }

    // Update is called once per frame
    void Update()
    {
        isFloor = Physics2D.OverlapCircle(floorDetector.position, 0.2f, whatIsGround);
        isWall = Physics2D.OverlapCircle(wallDetector.position, 0.2f, whatIsGround);       
        
        
    }

    void FixedUpdate()
    {
        if(!isFloor || isWall)
        {
            moveLeft = !moveLeft;
        }
        
        if(!moveLeft)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        } else 
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
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