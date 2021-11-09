using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyingEnemy : MonoBehaviour
{

    public Transform[] shootPoints;
    public GameObject bullet;

    Rigidbody2D rb;
    enemyFlying eF;

    bool hasAttacked;
    bool canAttack = true;
    public float delayTime;
    float delayReset;
    Animator anim;

    public float attackTimer;
    float attackTimerReset;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        eF = GetComponent<enemyFlying>();
        delayReset = delayTime;
        anim = GetComponent<Animator>();
        attackTimerReset = attackTimer;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(canAttack)
        {
            attackTimer -= Time.deltaTime;

            if(attackTimer < 0)
            {                
                anim.SetTrigger("attack");
                eF.isStopped = true;
                canAttack = false;
                // StartCoroutine(attackPlayer());  
                // target = playerToDamage[0].transform;
            }
        }

        if(!canAttack && hasAttacked)
        {
            delayTime -= Time.deltaTime; 
        }

        if(delayTime < 0)
        {
            
            hasAttacked = false;
            eF.isStopped = false;
            delayTime = delayReset;
            canAttack = true;
            attackTimer = attackTimerReset;
        }
        
    }

    public void fireBullets()
    {
        for (int i = 0; i < shootPoints.Length; i++)
        {
            GameObject bulletClone = (GameObject)Instantiate(bullet, shootPoints[i].position, shootPoints[i].rotation);
        }
    }

    public void ceaseAttack()
    {
        hasAttacked = true;
    }
}
