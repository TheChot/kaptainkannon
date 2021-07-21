// YOU NO LONGER NEED THIS SCRIPT
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class playerMovement1 : MonoBehaviour
{
    public bool isMobile;
    Rigidbody2D rb;
    public float moveSpeed = 10f;
    float moveInput;
    Quaternion gunPointer;
    // float moveInputMobile;

    public float jumpForce = 5f;
    public float rotateSpeed = 5f;

    public bool isGrounded;
    public float groundCheckRadius = 0.2f;
    public LayerMask whatIsGround;
    public Transform groundCheck;

    
    public KeyCode jump;

    Vector3 worldPosition;

    bool holdingMouse;
    Vector3 currentMousePosition;
    Vector3 heldMousePosition;
    Vector3 heldMouseReset;

    public Transform ropeAim;
    public Transform gunPosition;
    public Camera cam;

    public Vector2 gunRecoil;

    public Transform gfxTransform;
    public Transform gunGfxTransform;

    public float reloadTime;
    float reloadTimeReset;
    bool isReloading;
    public GameObject muzzleFlash;
    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemy;
    public Joystick joystick;
    // float horizontalAim;
    // float ho


    bool isAiming;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        reloadTimeReset = reloadTime;

    }

    // Update is called once per frame
    void Update()
    {
        // Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), 0);
        // moveVelocity = moveInput.normalized * moveSpeed;
        

        
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        // checkGround();
        controlChar();

        if(!isMobile)
        {
            targetAim();
        } else 
        {
            shootMobile();
        }
        

        if (isGrounded)
        {
            charJump();
        }

        if(isReloading)
        {
            reloadTime -= Time.deltaTime;
        }

        if(reloadTime < 0)
        {
            isReloading = false;
            reloadTime = reloadTimeReset;
            muzzleFlash.SetActive(false);
        }




    }

    void controlChar()
    {
        if(!isMobile)
        {
            moveInput = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        } else 
        {
            moveInput = CrossPlatformInputManager.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
            // Debug.Log(moveInput);
        }
        
        
        
        if (rb.velocity.x < 0)
        {
            gfxTransform.localScale = new Vector3(-1, 1, 1);
            gunGfxTransform.localScale = new Vector3(1, -1, 1);
        }
        else if (rb.velocity.x > 0)
        {
            gfxTransform.localScale = new Vector3(1, 1, 1);
            gunGfxTransform.localScale = new Vector3(1, 1, 1);
        }
    }

    void charJump()
    {
        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void targetAim()
    {
        currentMousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = currentMousePosition - gunPosition.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 0f;
        
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        gunPosition.rotation = Quaternion.Slerp(gunPosition.rotation, rotation, rotateSpeed * Time.deltaTime);
        // Debug.Log(angle);

        // Hacky 8 way recoil
        if(Input.GetMouseButtonDown(0))
        {
            if(!isReloading)
            {
                isReloading = true;
                muzzleFlash.SetActive(true);
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);

                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    // Debug.Log("Killed Enemy");
                    enemiesToDamage[i].gameObject.GetComponent<enemyController>().takeDamage();
                }

                if(angle > -10 && angle < 10)
                {
                    rb.velocity = new Vector2(-gunRecoil.x, 0);
                }
                else if (angle > 10 && angle < 80)
                {
                    rb.velocity = new Vector2(-gunRecoil.x, -gunRecoil.y);
                }
                else if (angle > 80 && angle < 100)
                {
                    rb.velocity = new Vector2(0, -gunRecoil.y);
                }
                else if (angle > 100 && angle < 170)
                {
                    rb.velocity = new Vector2(gunRecoil.x, -gunRecoil.y);
                }
                else if (angle > 170 || angle < -170)
                {
                    rb.velocity = new Vector2(gunRecoil.x, 0);
                }
                else if (angle > -170 && angle < -100)
                {
                    rb.velocity = new Vector2(gunRecoil.x, gunRecoil.y);
                }
                else if (angle > -100 && angle < -80)
                {
                    rb.velocity = new Vector2(0, gunRecoil.y);
                }
                else if (angle > -80 && angle < -10)
                {
                    rb.velocity = new Vector2(-gunRecoil.x, gunRecoil.y);
                }
            }
            
        }

    }

    public void shootMobile()
    {
        Vector2 aimInput = new Vector2(joystick.Horizontal, joystick.Vertical);
        
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            isAiming = true;
            // Debug.Log(aimInput);
            // forward
            if(aimInput.x >= 0.8 && aimInput.y <= 0.3)
            {
                gunPointer.eulerAngles = new Vector3(0, 0, 0);                
                gunPosition.rotation = gunPointer;
                

            } //diagonal right up
            else if (aimInput.x >= 0.3 && aimInput.x <= 0.8 && aimInput.y > 0)
            {
                gunPointer.eulerAngles = new Vector3(0, 0, 45);                
                gunPosition.rotation = gunPointer;

            } //up
            else if (aimInput.x <= 0.3 && aimInput.y >= 0.8)
            {
                gunPointer.eulerAngles = new Vector3(0, 0, 90);                
                gunPosition.rotation = gunPointer;
            }//diagonal left up
            else if (aimInput.x >= -0.8 && aimInput.x <= -0.3 && aimInput.y > 0)
            {
                gunPointer.eulerAngles = new Vector3(0, 0, 135);                
                gunPosition.rotation = gunPointer;
            }//left
            else if (aimInput.x <= -0.8 && aimInput.y <= 0.3)
            {
                gunPointer.eulerAngles = new Vector3(0, 0, 180);                
                gunPosition.rotation = gunPointer;
            }//diagonal left down
            else if (aimInput.x >= -0.8 && aimInput.x <= -0.3 && aimInput.y < 0)
            {
                gunPointer.eulerAngles = new Vector3(0, 0, -135);                
                gunPosition.rotation = gunPointer;
            }//down
            else if (aimInput.x >= -0.3 && aimInput.y <= -0.8)
            {
                gunPointer.eulerAngles = new Vector3(0, 0, -90);                
                gunPosition.rotation = gunPointer;
            }//diagonal right down
            else if (aimInput.x <= 0.8 && aimInput.x >= 0.3 && aimInput.y < 0)
            {
                gunPointer.eulerAngles = new Vector3(0, 0, -45);                
                gunPosition.rotation = gunPointer;
            }
        } else 
        {
            isAiming = false;
        }

        // add isreloading bool
        if(!isReloading)
        {
            if (CrossPlatformInputManager.GetButtonUp("Fire"))
            {
                isReloading = true;
                muzzleFlash.SetActive(true);
                isAiming = false;
                // Debug.Log(gunPosition.localEulerAngles.z);
                // forward
                if(gunPosition.localEulerAngles.z == 0)
                {                
                    rb.velocity = new Vector2(-gunRecoil.x, 0);

                } //diagonal right up //why the fuck is it not picking up 45
                else if (gunPosition.localEulerAngles.z >= 40 && gunPosition.localEulerAngles.z <= 50)
                {
                    rb.velocity = new Vector2(-gunRecoil.x, -gunRecoil.y);
                    Debug.Log("45d");

                } //up
                else if (gunPosition.localEulerAngles.z == 90)
                {
                    rb.velocity = new Vector2(0, -gunRecoil.y);

                }//diagonal left up
                else if (gunPosition.localEulerAngles.z == 135)
                {
                    rb.velocity = new Vector2(gunRecoil.x, -gunRecoil.y);

                }//left
                else if (gunPosition.localEulerAngles.z == 180)
                {
                    rb.velocity = new Vector2(gunRecoil.x, 0);

                }//diagonal left down
                else if (gunPosition.localEulerAngles.z == 225)
                {
                    rb.velocity = new Vector2(gunRecoil.x, gunRecoil.y);

                }//down
                else if (gunPosition.localEulerAngles.z == 270)
                {
                    rb.velocity = new Vector2(0, gunRecoil.y);

                }//diagonal right down
                else if (gunPosition.localEulerAngles.z == 315)
                {
                    rb.velocity = new Vector2(-gunRecoil.x, gunRecoil.y);
                }

                // if (gunPosition.localEulerAngles.z == 45)
                // {
                //     rb.velocity = new Vector2(-gunRecoil.x, -gunRecoil.y);
                //     Debug.Log("45d2");

                // }

                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemy);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    // Debug.Log("Killed Enemy");
                    enemiesToDamage[i].gameObject.GetComponent<enemyController>().takeDamage();
                }
            }
        }

        if(!isAiming && !isReloading)
        {
            if (gfxTransform.localScale.x < 0)
            {
                gunPointer.eulerAngles = new Vector3(0, 0, 180);
                gunPosition.rotation = gunPointer;
            }
            else if (gfxTransform.localScale.x > 0)
            {
                gunPointer.eulerAngles = new Vector3(0, 0, 0);                
                gunPosition.rotation = gunPointer;
            }
        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    

    // public void killPlayer

}

