using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossController : MonoBehaviour
{
    Animator anim;

    public bool startFight;
    public int bossHealth;
    int bossTotalHealth;
    public int hit;
    int hitReset;
    public bool exposeWeak;
    // bool phase1;
    public bool phase2;
    bossManager bM;
    public bool isWeaken;

    public float weakTime;
    float weakReset; 
    bool startCount;   
    public GameObject confirmHit;
    public SpriteRenderer sr;
    public int blinkCount = 5;
    public float blinkTime = 0.15f;

    void Start()
    {
        hitReset = hit; 
        anim = GetComponent<Animator>(); 
        weakReset = weakTime;
        bossTotalHealth = bossHealth;
        bM = bossManager.instance;
          
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if(hit == 0)
        {
            exposeWeak = true;
        }

        if(exposeWeak && startCount)
        {
            weakTime -= Time.deltaTime;
            if(weakTime < 0)
            {
                weakTime = weakReset;
                exposeWeak = false;
                hit = hitReset;
                startCount = false;
            }
        }

        phase2 = bossHealth <= 0.5 * bossTotalHealth;
        

        bool bossDead = bossHealth == 0;
        anim.SetBool("boss dead", bossDead);

        
    }

    public void hitBoss()
    {
        if(isWeaken)
        {
            hit -= 1;
            
        } else 
        {
            if(exposeWeak)
            {
                bossHealth -= 1;
                if(bossHealth > 0)
                {
                    exposeWeak = false;
                    startCount = false;
                    weakTime = weakReset;
                    hit = hitReset;
                }
            }
            
        } 

        if(sr != null)
        {
            StartCoroutine(hurtEnemy());
        }
    }

    public void recover()
    {
        exposeWeak = false;
        hit = hitReset;
        if(confirmHit != null)
            confirmHit.SetActive(false);
    }

    public void startTheCount()
    {
        startCount = true;
    }

    public void killBoss()
    {
        bM.deadBoss();
    }

    IEnumerator hurtEnemy()
    {
        
        for (int i = 0; i < blinkCount; i++)
        {
            yield return new WaitForSeconds(blinkTime);
            if (sr.color == Color.red)
            {
                sr.color = Color.white;
            }
            else
            {
                sr.color = Color.red;
            }    
        }

        sr.color = Color.white;
             
    }
}
