using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{

    enemyManager em;
    public int health = 1;
    public SpriteRenderer sr;
    public int blinkCount = 5;
    public float blinkTime = 0.15f;
    
    // Start is called before the first frame update
    void Start()
    {
        em = enemyManager.instance;
    }    

    public void takeDamage()
    {
        health -= 1;
        if(health > 0 && sr != null)
        {
            StartCoroutine(hurtEnemy());
        }
        if(health <= 0)
        {
            Instantiate(em.explosion, transform.position, transform.rotation);
            scoreManager.instance.enemiesKilled++;            
            Destroy(gameObject);
        }
        
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
