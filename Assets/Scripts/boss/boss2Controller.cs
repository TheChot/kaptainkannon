using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boss2Controller : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;

    public GameObject hand;
    public Transform[] firePoints;
    public float fireTimer; //gap between firepoints
    bossManager bM;
    public bossController bC;
    public int attackLoop;
    int attackLoopReset;
    public float attackGap, handGap, fireRest;
    float handGapReset, attackGapReset, fireRestReset;
    public float leftPoint, rightPoint;
    bool bossPos;
    bool startFireRest;
    bool canFire;
    public Transform firePointGroup;
    
    public Transform thePlayer;
    public Transform slamSpawn;
    public float slamGap;
    public float slamDelay;
    public int slamTimes;
    bool isSlamming;
    public bool canReset;
    public GameObject slammer1;
    public GameObject slammer2;
    
    bool grounded;
    public float fallSpeed;
    public float riseSpeed; 
    public float bossYPos;
    public float bossYPosLow;
    bool isRising;

    public GameObject giantHand;
    public int handsNo;
    public Transform handMinPoint, handMaxPoint;
    public float giantGap;
    public float giantStart;
    bool fireGiant = true;
    

    


    // Start is called before the first frame update
    void Start()
    {
        bM = bossManager.instance;
        attackGapReset = attackGap;
        handGapReset = handGap;
        fireRestReset = fireRest;
        anim = GetComponent<Animator>(); 
        canFire = true;
        attackLoopReset = attackLoop;
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(bM.bossStart)
        {
            phase1();
            grounded = transform.position.y < bossYPosLow;
            if(bC.exposeWeak)
            {
                canReset = false;
                if(!grounded)
                {
                    rb.velocity = new Vector2(rb.velocity.x, -fallSpeed);
                    
                } else
                {
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                }
            } else if (!bC.exposeWeak && transform.position.y < bossYPos)
            {
                rb.velocity = new Vector2(rb.velocity.x, riseSpeed);
                isRising = true;

                
            }

            if(transform.position.y >= bossYPos && isRising)
            {
                isRising = false;
                rb.velocity = new Vector2(rb.velocity.x, 0);
                canReset = true;
            }
            anim.SetBool("isWeak", bC.exposeWeak);
            anim.SetBool("rising", isRising);
            anim.SetBool("grounded", grounded);

        }
    }

    void phase1()
    {
        if(transform.position.x == leftPoint)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if(transform.position.x == rightPoint)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if(bossPos)
        {
            transform.position = new Vector3(rightPoint, transform.position.y, transform.position.z);
        } else
        {
            transform.position = new Vector3(leftPoint, transform.position.y, transform.position.z);
        }

        if(attackLoop > 0)
        {
            attackGap -= Time.deltaTime;

            if(attackGap <= 0)
            {
                handGap -= Time.deltaTime;

                if(handGap <= 0 && canFire)
                {
                    anim.SetTrigger("shoot");
                    canFire = false;

                }
            }
        }

        if(attackLoop <= 0 && canFire)
        {
            canFire = false;
            isSlamming = true;
            canReset = false;
            if(bC.phase2)
            {
                if(fireGiant)
                {
                    StartCoroutine(fireGiantHands());
                } else 
                {
                    StartCoroutine(startSlammin());
                }
            } else 
            {
                StartCoroutine(startSlammin());
            }
            

        }

        if(startFireRest)
        {
            fireRest -= Time.deltaTime;

            if(fireRest < 0)
            {
                startFireRest = false;
                fireRest = fireRestReset;
                bossPos = !bossPos;
                attackLoop -= 1;
                canFire = true;
                
            }
        }

        if(isSlamming && canReset)
        {
            if(!bC.exposeWeak)
            {
                attackLoop = attackLoopReset;
                attackGap = attackGapReset;
                handGap = handGapReset;
                canFire = true;
                bossPos = !bossPos;
                isSlamming = false;
                fireGiant = true;
            }

        }
    }

    public void fireHands()
    {
        StartCoroutine(handFire());
    }

    

    IEnumerator handFire()
    {
        for (int i = 0; i < firePoints.Length; i++)
        {
            if(bossPos)
            {
                Quaternion rotation = firePointGroup.transform.rotation;
                rotation.eulerAngles = new Vector3(0,0, 180);
                firePointGroup.transform.rotation = rotation;
            } else 
            {
                Quaternion rotation = firePointGroup.transform.rotation;
                rotation.eulerAngles = new Vector3(0,0, 0);
                firePointGroup.transform.rotation = rotation;
            }
            yield return new WaitForSeconds(fireTimer);
            GameObject _handClone = (GameObject)Instantiate(hand, firePoints[i].position, firePoints[i].rotation);        
            if(bossPos)
            {
                _handClone.transform.GetChild(0).localScale = new Vector3(1, -1, 1);
            }
        }
        startFireRest = true;
    }

    IEnumerator startSlammin()
    {        
        yield return new WaitForSeconds(slamDelay);
        for (int i = 0; i < slamTimes; i++)
        {
            anim.SetTrigger("slam");
            slamSpawn.position = new Vector3(thePlayer.position.x, slamSpawn.position.y, slamSpawn.position.z);
            yield return new WaitForSeconds(slamGap);
            if(i != slamTimes - 1)
            {
                GameObject _slamClone = (GameObject)Instantiate(slammer1, slamSpawn.position, slamSpawn.rotation);        
            } else 
            {
                GameObject _slamClone = (GameObject)Instantiate(slammer2, slamSpawn.position, slamSpawn.rotation);        
                _slamClone.GetComponent<slammerBoss>().myBoss = this;
            }
        }

        canReset = true;
    }

    IEnumerator fireGiantHands()
    {        
        yield return new WaitForSeconds(giantStart);
        for (int i = 0; i < handsNo; i++)
        {
            anim.SetTrigger("wave hand");
            yield return new WaitForSeconds(giantGap);
            if(i%2 == 0)
            {
                GameObject _giantHandClone = (GameObject)Instantiate(giantHand, handMinPoint.position, handMinPoint.rotation);        
            } else 
            {
                GameObject _giantHandClone = (GameObject)Instantiate(giantHand, handMaxPoint.position, handMaxPoint.rotation);        
                _giantHandClone.transform.GetChild(0).localScale = new Vector3(1, -1, 1);
            }
        }

        startFireRest = true;
        fireGiant = false;
    }

    
}
