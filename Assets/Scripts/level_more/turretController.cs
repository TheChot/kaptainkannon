using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turretController : MonoBehaviour
{
    public int noBullets;
    public float timer;
    public float shootDelayTime;
    float timerReset;
    bool isShooting;
    // int bulletCounter;
    public Transform[] shootPoints;
    public GameObject bullet;
    

    // Start is called before the first frame update
    void Start()
    {
        timerReset = timer;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer -= Time.deltaTime;

        if(timer < 0 && !isShooting)
        {
            isShooting = true;
            StartCoroutine(shootBulletDelay());
        }

        // if(bulletCounter >= noBullets)
        // {
            
        // }
    }

    void fireBullets()
    {
        for (int i = 0; i < shootPoints.Length; i++)
        {
            GameObject bulletClone = (GameObject)Instantiate(bullet, shootPoints[i].position, shootPoints[i].rotation);
        }
        
    }

    IEnumerator shootBulletDelay()
    {
        
        for (int i = 0; i < noBullets; i++)
        {
            yield return new WaitForSeconds(shootDelayTime);
            // soundManager.instance.playEnemyBullet();
            // add bullet animation and fire function here
            for (int j = 0; j < shootPoints.Length; j++)
            {
                GameObject bulletClone = (GameObject)Instantiate(bullet, shootPoints[j].position, shootPoints[j].rotation);
            }
            // GameObject bsulletClone = (GameObject)Instantiate(bullet, shootPoint.position, shootPoint.rotation);
            // bulletCounter++;
        }
        isShooting = false;
        timer = timerReset;
    }
}
