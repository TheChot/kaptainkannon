using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragon : MonoBehaviour
{
    public bool moveLeft; 
    public float speed;
    public boss3Controller bC3;
    public float leftPoint, rightPoint;
    public bool isWeak;
    bossController bC;
    Animator anim;
    bossManager bM;
    
    // Start is called before the first frame update
    void Start() 
    {
        bM = bossManager.instance;
        anim = GetComponent<Animator>(); 
        bC = bM.bC;
        if(!moveLeft)    
            transform.localScale = new Vector3( -1, 1, 1);
    }
    
    void FixedUpdate()
    {
        if(bC.bossHealth != 0)
        {
            if(!moveLeft)
            {
                transform.position += (transform.right * Time.fixedDeltaTime * speed);
                
                if(transform.position.x > rightPoint)
                {
                    if(isWeak)
                    {
                        bC3.advanceDragon();
                    } else 
                    {
                        bC3.spawnNextDragon();
                    }
                    
                    Destroy(gameObject);
                }
            } else 
            {
                transform.position -= (transform.right * Time.fixedDeltaTime * speed);
                if(transform.position.x < leftPoint)
                {
                    if(isWeak)
                    {
                        bC3.advanceDragon();
                    } else 
                    {
                        bC3.spawnNextDragon();
                    }
                    Destroy(gameObject);
                }
            }
        } else 
        {
            anim.SetBool("boss dead", true);
        }
        
    }

    public void deadBoss()
    {
        bM.deadBoss();
    }
}
