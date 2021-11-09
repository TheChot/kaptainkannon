using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saw : MonoBehaviour
{
    public bool activateSaw;
    public bool isMoving;
    public Animator anim;
    bool canMove;
    Collider2D col;

    public float moveSpeed;
    public int damageGive;
    public bool moveLeft; //move left or down
    public bool moveHorizontal;

    public float moveDistance;
    float maxDis, minDis;    
    // public bool isFlying;
    public AudioSource sawSound;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();
        col.enabled = false;
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
    void FixedUpdate()
    {
        if(activateSaw)
        {
            anim.SetBool("saw_start", true);
            col.enabled = true;
            if(canMove)
            {
                if(moveHorizontal)
                {
                    if(transform.position.x < minDis || transform.position.x > maxDis)
                    {
                        moveLeft = !moveLeft;
                        sawSound.Play();
                    }

                    if(!moveLeft)
                    {
                        // rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
                        transform.position += (new Vector3(moveSpeed, 0, 0) * Time.fixedDeltaTime);
                    } else 
                    {
                        // rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
                        transform.position += (new Vector3(-moveSpeed, 0, 0) * Time.fixedDeltaTime);
                    }
                } 
                else 
                {
                    if(transform.position.y < minDis || transform.position.y > maxDis)
                    {
                        moveLeft = !moveLeft;
                        sawSound.Play();
                    }

                    if(!moveLeft)
                    {
                        transform.position += (new Vector3(0, moveSpeed, 0) * Time.fixedDeltaTime);
                    } else 
                    {
                        transform.position += (new Vector3(0, -moveSpeed, 0) * Time.fixedDeltaTime);
                    }
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            other.gameObject.GetComponent<playerController>().takeDamage(damageGive);            
        }        
    }

    public void startMoving()
    {
        if(isMoving)
        {
            canMove = true;
            sawSound.Play();
        }
    }

    
}
