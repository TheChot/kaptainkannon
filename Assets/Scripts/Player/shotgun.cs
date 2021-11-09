using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class shotgun : MonoBehaviour
{
    gunController gunControl;
    playerMovement thePlayer;
    Rigidbody2D rb;

    public Vector2 gunRecoil; 
    public float enemyBounce;
    bool isReloading;
    bool enemyJump;

    public float reloadTime;
    float reloadTimeReset;

    public float jumpGap;
    float jumpGapReset;

    public GameObject muzzleFlash;
    public Transform shootPoint;

    // Tellls the circle physics object where to hit
    public Transform attackPos;
    public Transform attackSecondPos;
    public float attackRange;
    public LayerMask whatIsEnemy;
    public LayerMask whatIsSwitch;
    public LayerMask whatIsBoss;
    public LayerMask whatisWeak;
    Animator anim;
    public AudioSource gunSound;
    public AudioSource reloadSound;
    


    void Start()
    {
        gunControl = GetComponent<gunController>();
        thePlayer = GetComponent<playerMovement>();
        rb = GetComponent<Rigidbody2D>();
        reloadTimeReset = reloadTime;
        jumpGapReset = jumpGap;
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        if(gunControl.shotGunEquipped)
        {
            if(isReloading)
            {
                reloadTime -= Time.deltaTime;
            }

            if(isReloading && reloadTime <= 0)
            {
                reloadSound.Play();
                reloadTime = reloadTimeReset;
                // muzzleFlash.SetActive(false);
                anim.ResetTrigger("fire");
                anim.ResetTrigger("jumpfire");
                isReloading = false;
                // thePlayer.gunGfxTransform.eulerAngles = new Vector3(0, 0, 0);
            }
            

        }
    }

    void FixedUpdate()
    {
        if(gunControl.shotGunEquipped)
        {   
            if(!thePlayer.isPc){
                if(CrossPlatformInputManager.GetButtonDown("Fire") && !isReloading)
                {
                    isReloading = true;
                    // muzzleFlash.SetActive(true);
                    GameObject _muzzleFlash = (GameObject)Instantiate(muzzleFlash, shootPoint.position, shootPoint.rotation);
                    anim.SetTrigger("fire");
                    gunSound.Play();
                    destroyEnemies();
                    if(transform.localScale.x > 0)
                    {
                        _muzzleFlash.transform.localScale = new Vector3(1, 1, 1);
                        thePlayer.gunRecoil = true;
                        rb.velocity = new Vector2(-gunRecoil.x, rb.velocity.y);

                        // Debug.Log(-gunRecoil.x);
                    } else
                    {
                        _muzzleFlash.transform.localScale = new Vector3(-1, 1, 1);
                        thePlayer.gunRecoil = true;                    
                        rb.velocity = new Vector2(gunRecoil.x, rb.velocity.y);
                    }
                }
            } else {
                if(Input.GetButtonDown("Fire1") && !isReloading)
                {
                    isReloading = true;
                    // muzzleFlash.SetActive(true);
                    GameObject _muzzleFlash = (GameObject)Instantiate(muzzleFlash, shootPoint.position, shootPoint.rotation);
                    anim.SetTrigger("fire");
                    gunSound.Play();
                    destroyEnemies();
                    if(transform.localScale.x > 0)
                    {
                        _muzzleFlash.transform.localScale = new Vector3(1, 1, 1);
                        thePlayer.gunRecoil = true;
                        rb.velocity = new Vector2(-gunRecoil.x, rb.velocity.y);
                    } else
                    {
                        _muzzleFlash.transform.localScale = new Vector3(-1, 1, 1);
                        thePlayer.gunRecoil = true;                    
                        rb.velocity = new Vector2(gunRecoil.x, rb.velocity.y);
                    }
                }
            }
            if(!thePlayer.isGrounded)
            {
                jumpGap -= Time.deltaTime;
            }

            if(thePlayer.isGrounded) 
            {
                jumpGap = jumpGapReset;
            }

            // Jump and attack
            if(jumpGap <= 0 && !thePlayer.isGrounded)
            {
                if(CrossPlatformInputManager.GetButtonDown("Jump") && !isReloading || Input.GetButtonDown("Jump") && !isReloading)
                {
                    isReloading = true;
                    anim.SetTrigger("jumpfire");
                    // muzzleFlash.SetActive(true);
                    gunSound.Play();
                    // destroyEnemies();
                    Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackSecondPos.position, attackRange, whatIsEnemy);
                    
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {                        
                        enemiesToDamage[i].gameObject.GetComponent<enemyController>().takeDamage();
                    }                   

                    if(enemiesToDamage.Length > 0)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, gunRecoil.y + enemyBounce);
                        
                    } 

                    Collider2D[] switchesToEngage = Physics2D.OverlapCircleAll(attackSecondPos.position, attackRange, whatIsSwitch);
                    
                    for (int i = 0; i < switchesToEngage.Length; i++)
                    {   
                        switchesToEngage[i].gameObject.GetComponent<singleSwitch>().activateSwitch();
                    }

                    if(switchesToEngage.Length > 0)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, gunRecoil.y + enemyBounce);
                        
                    }

                    Collider2D[] bossToDamage = Physics2D.OverlapCircleAll(attackSecondPos.position, attackRange, whatIsBoss);
                    
                    for (int i = 0; i < bossToDamage.Length; i++)
                    {
                        if(bossToDamage[i].gameObject.GetComponent<bossController>() != null)                        
                            bossToDamage[i].gameObject.GetComponent<bossController>().hitBoss();
                    }                   
                    
                                  
                    if(bossToDamage.Length > 0)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, gunRecoil.y + enemyBounce);
                        
                    }

                    Collider2D[] weakToDamage = Physics2D.OverlapCircleAll(attackSecondPos.position, attackRange, whatisWeak);
                    
                    for (int i = 0; i < weakToDamage.Length; i++)
                    {                        
                        weakToDamage[i].gameObject.GetComponent<bossWeakSpot>().hitWeak();
                    }                   

                    if(weakToDamage.Length > 0)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, gunRecoil.y + enemyBounce);
                        
                    }


                    if(weakToDamage.Length == 0 && bossToDamage.Length == 0 && switchesToEngage.Length == 0 && enemiesToDamage.Length == 0)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, gunRecoil.y);
                    }

                }
            }
        }
    }

    void destroyEnemies()
    {
        
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            // Debug.Log("Killed Enemy");
            enemiesToDamage[i].gameObject.GetComponent<enemyController>().takeDamage();
        }

        Collider2D[] switchesToEngage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsSwitch);
                    
        for (int i = 0; i < switchesToEngage.Length; i++)
        {   
            switchesToEngage[i].gameObject.GetComponent<singleSwitch>().activateSwitch();
        }

        Collider2D[] bossesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsBoss);
                    
        for (int i = 0; i < bossesToDamage.Length; i++)
        {   
            if(bossesToDamage[i].gameObject.GetComponent<bossController>() != null)                        
                bossesToDamage[i].gameObject.GetComponent<bossController>().hitBoss();
        }

        Collider2D[] weakToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatisWeak);
                    
        for (int i = 0; i < weakToDamage.Length; i++)
        {   
            weakToDamage[i].gameObject.GetComponent<bossWeakSpot>().hitWeak();
        }

        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
        Gizmos.DrawWireSphere(attackSecondPos.position, attackRange);
        
    }

    public void muzzleFlashDown()
    {
        GameObject _muzzleFlash = (GameObject)Instantiate(muzzleFlash, shootPoint.position, shootPoint.rotation);
        if(transform.localScale.x > 0)
        {
            _muzzleFlash.transform.localScale = new Vector3(1, 1, 1);            
        } else
        {
            _muzzleFlash.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
