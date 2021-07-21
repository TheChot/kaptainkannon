using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformDrop : MonoBehaviour
{
    bool playerOn;
    bool platformOff;
    public bool isActive;
    public float holdTime;
    float holdTimeReset;
    public float reappearTime;
    float reappearTimeReset;

    Collider2D col;

    GameObject openPlat;
    GameObject closedPlat;

    // Start is called before the first frame update
    void Start()
    {
        reappearTimeReset = reappearTime;
        holdTimeReset = holdTime;

        col = GetComponent<Collider2D>();

        openPlat = transform.GetChild(0).gameObject;
        closedPlat = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {    
        
        if(playerOn)
        {
            holdTime -= Time.deltaTime;
        }

        if(holdTime <= 0)
        {
            playerOn = false;
            col.enabled = false;
            reappearTime -= Time.deltaTime;
            openPlat.SetActive(false);
            closedPlat.SetActive(true);
        }

        if(reappearTime <= 0)
        {
            col.enabled = true;
            holdTime = holdTimeReset;
            reappearTime = reappearTimeReset;
            openPlat.SetActive(true);
            closedPlat.SetActive(false);
        }      

        
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            if(collision.gameObject.GetComponent<playerMovement>().isGrounded)
            {
                playerOn = true;
            }
            
        }
    }


}
