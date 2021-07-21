using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    // public GameObject ;
    public int health; 
    levelManager lm;
    // Start is called before the first frame update
    void Start()
    {
        lm = levelManager.instance;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            lm.isDead = true;
            gameObject.SetActive(false);
        }
    }

    public void takeDamage(int dmg)
    {
        // gameObject.SetActive(false);
        health -= dmg;
    }
}
