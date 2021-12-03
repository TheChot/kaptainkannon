using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss1Controller : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;

    public float phase1Speed = 3f;
    public float phase2Speed = 3.6f;

    public bool moveLeft;
    public bool stop;
    public int walkLoop; //the number of times it walks before it starts blade throwing    
    int walkLoopReset;
    public float maxX, minX; // The max distance the character can walk

    public int attackNo;
    int attackNoReset;
    public float attackTimer;
    float attackTimerReset;
    bossManager bM;
    bossController bC;
    public Transform throwPoint;
    public GameObject saw1;
    public GameObject saw2;
    public float chillTime;

    bool justStopped;
    bool justThrown;

    bool canAttack;

    public float jumpForce, highPoint, lowPoint, sawMinSpeed, sawMaxSpeed;
    bool land, changePos;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); 
        attackTimerReset = attackTimer;
        walkLoopReset = walkLoop;
        bC = GetComponent<bossController>(); 
        bM = bossManager.instance;
        attackNoReset = attackNo;
    }

    void FixedUpdate()
    {
        if(bM.bossStart)
        {
            phase1();
        }

        anim.SetBool("weak exposed", bC.exposeWeak);

        if(bC.exposeWeak)
        {
            stop = true;
        }

        if(transform.position.y > highPoint && !changePos)
        {
            bool randomNum = Random.Range(0,10) > 5;
            changePos = true;
            if(randomNum)
            {
                transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
                transform.localScale = new Vector3(-1, 1, 1);
            } else 
            {
                transform.position = new Vector3(minX, transform.position.y, transform.position.z);
                transform.localScale = new Vector3(1, 1, 1);
            }
        }

        bool land = transform.position.y < lowPoint;
        anim.SetBool("land", land);

    }

    void phase1()
    {
        if(!stop)
        {
            float speed = bC.phase2 ? phase2Speed : phase1Speed;
            if(moveLeft)
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            } else 
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }

            anim.SetFloat("move", speed);

            if (rb.velocity.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if (rb.velocity.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }

            if(transform.position.x >= maxX)
            {
                if(!moveLeft)
                    walkLoop -= 1;

                moveLeft = true;
                
            } 
            else if(transform.position.x <= minX)
            {
                if(moveLeft)
                    walkLoop -= 1;

                moveLeft = false;
            }
        }

        if(walkLoop <= 0)
        {
            if(!justStopped)
            {
                if(transform.localScale.x > 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                } else
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }

                anim.SetTrigger("throw");
                canAttack = true;
            }
            
            rb.velocity = new Vector2(0, rb.velocity.y);
            stop = true;
            justStopped = true;           
            
        }

        if(stop)
        {
            // attackTimer -= Time.deltaTime;
            anim.SetFloat("move", 0);            
            rb.velocity = new Vector2(0, rb.velocity.y);
            // StartCoroutine(attackPlayer());
        }

        if(canAttack && transform.position.y < lowPoint)
        {
            attackTimer -= Time.deltaTime;

            if(attackNo > 0)
            {
                if(attackTimer < 0)
                {
                    if(!justThrown)
                    {
                        anim.SetTrigger("throw");
                        justThrown = true;
                    }          
                    
                }

            } else {
                if(attackTimer < 0)
                {
                    attackNo = attackNoReset;
                    stop = false;
                    justStopped = false;
                    attackTimer = attackTimerReset;
                    walkLoop = walkLoopReset;
                    canAttack = false;
                }
                
            }

        }

        
    }

    

    public void spawnSaw()
    {
        attackTimer = attackTimerReset;
        justThrown = false;
        attackNo -= 1;      
        GameObject saw = bC.phase2 ? saw2 : saw1;
        GameObject _sawClone = (GameObject)Instantiate(saw, throwPoint.position, throwPoint.rotation);        
        if(bC.phase2)
        {
            _sawClone.GetComponent<bouncingSaw>().sawMove.x = Random.Range(sawMinSpeed, sawMaxSpeed);
        }
        if(transform.localScale.x < 0)        
        {
            Quaternion rotation = _sawClone.transform.rotation;
            rotation.eulerAngles = new Vector3(0,0, 180);
            _sawClone.transform.rotation = rotation;
        }
    }

    public void jumpToTheSky()
    {
        changePos = false;
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    public void startAttack()
    {
        canAttack = true;
        attackNo = attackNoReset;
        justStopped = false;
        attackTimer = attackTimerReset;
        walkLoop = walkLoopReset;        
    }

}

