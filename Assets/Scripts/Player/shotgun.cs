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

    public Transform attackPos;
    public Transform attackSecondPos;
    public float attackRange;
    public LayerMask whatIsEnemy;
    public LayerMask whatIsSwitch;

    


    void Start()
    {
        gunControl = GetComponent<gunController>();
        thePlayer = GetComponent<playerMovement>();
        rb = GetComponent<Rigidbody2D>();
        reloadTimeReset = reloadTime;
        jumpGapReset = jumpGap;
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
                reloadTime = reloadTimeReset;
                muzzleFlash.SetActive(false);
                isReloading = false;
                thePlayer.gunGfxTransform.eulerAngles = new Vector3(0, 0, 0);
            }
            

        }
    }

    void FixedUpdate()
    {
        if(gunControl.shotGunEquipped)
        {   

            if(CrossPlatformInputManager.GetButtonDown("Fire") && !isReloading)
            {
                isReloading = true;
                muzzleFlash.SetActive(true);
                destroyEnemies();
                if(thePlayer.gfxTransform.localScale.x > 0)
                {
                    thePlayer.gunRecoil = true;
                    rb.velocity = new Vector2(-gunRecoil.x, rb.velocity.y);

                    // Debug.Log(-gunRecoil.x);
                } else
                {
                    thePlayer.gunRecoil = true;                    
                    rb.velocity = new Vector2(gunRecoil.x, rb.velocity.y);
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


            if(jumpGap <= 0 && !thePlayer.isGrounded)
            {
                if(CrossPlatformInputManager.GetButtonDown("Jump") && !isReloading)
                {
                    isReloading = true;
                    muzzleFlash.SetActive(true);
                    // destroyEnemies();
                    Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackSecondPos.position, attackRange, whatIsEnemy);
                    
                    for (int i = 0; i < enemiesToDamage.Length; i++)
                    {
                        
                        enemiesToDamage[i].gameObject.GetComponent<enemyController>().takeDamage();
                    }                   

                    if(enemiesToDamage.Length > 0)
                    {
                        Debug.Log("jumped");
                        rb.velocity = new Vector2(rb.velocity.x, gunRecoil.y + enemyBounce);
                        
                    } else 
                    {
                        rb.velocity = new Vector2(rb.velocity.x, gunRecoil.y);
                    }

                    Collider2D[] switchesToEngage = Physics2D.OverlapCircleAll(attackSecondPos.position, attackRange, whatIsSwitch);
                    
                    for (int i = 0; i < switchesToEngage.Length; i++)
                    {   
                        switchesToEngage[i].gameObject.GetComponent<singleSwitch>().activateSwitch();
                    }

                    if(thePlayer.gfxTransform.localScale.x > 0)
                    {
                        thePlayer.gunGfxTransform.eulerAngles = new Vector3(0, 0, -90);                        
                    } else
                    {
                        thePlayer.gunGfxTransform.eulerAngles = new Vector3(0, 0, 90);
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
            Debug.Log("Killed Enemy");
            enemiesToDamage[i].gameObject.GetComponent<enemyController>().takeDamage();
        }

        Collider2D[] switchesToEngage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsSwitch);
                    
        for (int i = 0; i < switchesToEngage.Length; i++)
        {   
            switchesToEngage[i].gameObject.GetComponent<singleSwitch>().activateSwitch();
        }

        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
