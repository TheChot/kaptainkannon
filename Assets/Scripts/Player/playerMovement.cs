using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class playerMovement : MonoBehaviour
{
    // For movement
    Rigidbody2D rb;
    public float moveSpeed = 10f;
    float moveInput;

    // For jumping 
    public float jumpForce = 5f;
    public bool isGrounded;
    public float groundCheckRadius = 0.2f;
    public LayerMask whatIsGround;
    public Transform groundCheck;

    // Graphics
    public Transform gfxTransform;
    public Transform gunGfxTransform;

    // gun recoil
    public bool gunRecoil;
    public float gunRecTime;
    float gunRecTimeReset;
    public bool isJumping; //gun jump recoil
    public int jumpCounter = 0;
    public Transform shootPoint;

    public bool isPc;

    Animator anim;
    public AudioSource jumpSound;
    playerController pC;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gunRecTimeReset = gunRecTime;
        anim = GetComponent<Animator>();
        pC = GetComponent<playerController>();
    }

    void Update()
    {
        moveInput = CrossPlatformInputManager.GetAxisRaw("Horizontal");

        if(isPc){
            moveInput = Input.GetAxisRaw("Horizontal");
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        anim.SetBool("grounded", isGrounded);
        if (isGrounded)
        {
            isJumping = false;
            if(!isPc){
                if (CrossPlatformInputManager.GetButtonDown("Jump"))
                {
                    jumpSound.Play();
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    isJumping = true;
                    // jumpCounter += 1;
                }
            }else{
                if (Input.GetButtonDown("Jump"))
                {
                    jumpSound.Play();
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    isJumping = true;
                    // jumpCounter += 1;
                }
            }
        } 
    }

    void FixedUpdate()
    {
        anim.SetBool("isDead", pC.health < 0);
        if(pC.health < 0)
            return;
        // gun recoil variable
        // hacky way of slightly disabling the 
        // moveinput so that the player can be pushed back by recoil
        if(!gunRecoil)
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
            anim.SetFloat("run", Mathf.Abs(moveInput));

        } else 
        {
            anim.SetFloat("run", 0);
        }
        if(gunRecoil)
        {
            gunRecTime -= Time.deltaTime;
        }

        if(gunRecTime <= 0 && gunRecoil)
        {
            gunRecTime = gunRecTimeReset;
            gunRecoil = false;
        }

        // flips char
        if (moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            // shootPoint.localScale = new Vector3(-1, 1, 1);
            // gfxTransform.localScale = new Vector3(-1, 1, 1);
            // gunGfxTransform.localScale = new Vector3(-1, 1, 1);
            // gunGfxTransform.GetChild(0).GetChild(1).GetChild(0).localScale = new Vector3(-1, 1, 1);
            // gunGfxTransform.GetChild(0).GetChild(1).GetChild(1).localScale = new Vector3(-1, 1, 1);
        }
        else if (moveInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            // shootPoint.localScale = new Vector3(-1, 1, 1);
            // gfxTransform.localScale = new Vector3(1, 1, 1);
            // gunGfxTransform.localScale = new Vector3(1, 1, 1);
            // gunGfxTransform.GetChild(0).GetChild(1).GetChild(0).localScale = new Vector3(1, 1, 1);
            // gunGfxTransform.GetChild(0).GetChild(1).GetChild(1).localScale = new Vector3(1, 1, 1);
        }
        
    }

    // public void gunRecoil(float recoil)
    // {
    //     rb.velocity = new Vector2(recoil, rb.velocity.y);
    // }

    public void hurtRecoil(float bounce)
    {
        rb.velocity = new Vector2(0, bounce);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

}

