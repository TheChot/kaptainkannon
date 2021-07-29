using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flyEnemyShooting : MonoBehaviour
{
    // Create a circle around 
    // the char that detects
    // the player and then attacks him

    Rigidbody2D rb;
    enemyFlying eF;
    public LayerMask whatIsPlayer;
    public float detectRange;
    public Transform detector;

    bool hasAttacked;
    bool canAttack = true;
    public float delayTime;
    float delayReset;
    public float nextAttackCount;
    float nextReset;
    bool startNextCount;

    public Transform shootPoint;
    public GameObject bullet;
    public int bulletsToFire;
    public float bulletDelayTime;
    public float shootBulletDelay;
    // public float addedDeg = 180f;
    // public float rotateSpeed;
    // Transform target;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        eF = GetComponent<enemyFlying>();
        delayReset = delayTime;
        nextReset = nextAttackCount;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(canAttack)
        {
            Collider2D[] playerToDamage = Physics2D.OverlapCircleAll(detector.position, detectRange, whatIsPlayer);

            if(playerToDamage.Length > 0)
            {
                eF.isStopped = true;
                canAttack = false;
                StartCoroutine(attackPlayer());  
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

    IEnumerator attackPlayer()
    {
        yield return new WaitForSeconds(shootBulletDelay);
        for (int i = 0; i < bulletsToFire; i++)
        {
            Quaternion rotation = transform.rotation;
            // rotation.eulerAngles = new Vector3(0,0, 0);
            if(transform.localScale.x > 0)
            {
                rotation.eulerAngles = new Vector3(0,0, -45);
            } else 
            {
                rotation.eulerAngles = new Vector3(0,0, -135);
            }
            
            yield return new WaitForSeconds(bulletDelayTime);
            GameObject bulletClone = (GameObject)Instantiate(bullet, shootPoint.position, rotation);
        }
        hasAttacked = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;        
        Gizmos.DrawWireSphere(detector.position, detectRange);
    }
}
