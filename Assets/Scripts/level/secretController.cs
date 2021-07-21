using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class secretController : MonoBehaviour
{   
    void Start()
    {
        if(checkPointManager.instance.secretCollected)
        {
            scoreManager.instance.secretsCollected++;
            Destroy(gameObject);
        }
             
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            scoreManager.instance.secretsCollected++;
            checkPointManager.instance.secretCollected = true;
            // gameObject.SetActive(false);
            Destroy(gameObject);
        }
    } 
    
}
