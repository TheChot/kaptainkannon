using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeController : MonoBehaviour
{
    Transform thePlayer;

    public Transform leftPoint;

    public int damage;

    

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            thePlayer = collision.gameObject.transform;

            if(thePlayer.position.x > transform.position.x)
            {
                thePlayer.position = leftPoint.position;
            } 
            else if(thePlayer.position.x < transform.position.x)
            {
                thePlayer.position = leftPoint.position;
            }

            collision.gameObject.GetComponent<playerController>().takeDamage(damage);
        }
    }
}
